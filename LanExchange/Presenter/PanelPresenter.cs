using System;
using System.Windows.Forms;
using LanExchange.Model;
using LanExchange.Properties;
using LanExchange.SDK;
using LanExchange.UI;

namespace LanExchange.Presenter
{
    public class PanelPresenter : IPanelPresenter
    {
        private IPanelModel m_Objects;
        private readonly IPanelView m_View;

        public event EventHandler CurrentPathChanged;

        public PanelPresenter(IPanelView view)
        {
            m_View = view;
        }

        public void SetupColumns()
        {
            if (m_Objects.DataType == null)
                return;
            m_View.ColumnsClear();
            var columns = AppPresenter.PanelColumns.GetColumns(m_Objects.DataType);
            if (columns == null) return;
            int j = 0;
            for (int i = 0; i < columns.Count; i++ )
                if (columns[i].Visible)
                {
                    m_View.AddColumn(columns[i]);
                    if (i == m_Objects.Comparer.ColumnIndex)
                        m_View.SetColumnMarker(j, m_Objects.Comparer.SortOrder);
                    ++j;
                }
        }

        public void ResetSortOrder()
        {
            m_Objects.Comparer.ColumnIndex = 0;
            m_Objects.Comparer.SortOrder = SortOrder.Ascending;
        }

        public void UpdateItemsAndStatus()
        {
            if (m_Objects == null) return;
            // refresh only for current page
            var model = AppPresenter.MainPages.GetModel();
            var currentItemList = model.GetItem(model.SelectedIndex);
            if (currentItemList == null || !m_Objects.Equals(currentItemList)) 
                return;
            // get number of visible items (filtered) and number of total items
            var showCount = m_Objects.FilterCount;
            var totalCount = m_Objects.Count;
            if (m_Objects.HasBackItem)
            {
                showCount--;
                totalCount--;
            }
            if (showCount != totalCount)
                MainForm.Instance.ShowStatusText(Resources.PanelPresenter_Items2, showCount, totalCount);
            else
                MainForm.Instance.ShowStatusText(Resources.PanelPresenter_Items1, showCount);
            SetupColumns();
            m_View.SetVirtualListSize(m_Objects.FilterCount);
            if (m_Objects.FilterCount > 0)
            {
                var index = Objects.IndexOf(currentItemList.FocusedItem);
                m_View.FocusedItemIndex = index;
            }
            m_View.Filter.UpdateFromModel(Objects);
        }

        private void CurrentPath_Changed(object sender, EventArgs e)
        {
            if (m_Objects != null && CurrentPathChanged != null)
                CurrentPathChanged(sender, e);
        }

        public IPanelModel Objects
        {
            get { return m_Objects; }
            set
            {
                if (m_Objects != null)
                    m_Objects.CurrentPath.Changed -= CurrentPath_Changed;
                m_Objects = value;
                if (m_Objects != null)
                    m_Objects.CurrentPath.Changed += CurrentPath_Changed;
                m_View.Filter.Presenter.SetModel(value);
                //m_View.SetVirtualListSize(m_Objects.Count);
            }
        }

        public PanelItemBase GetFocusedPanelItem(bool pingAndAsk, bool canReturnParent)
        {
            var panelItem = m_View.FocusedItem;
            if (panelItem != null && pingAndAsk)
            {
                var bReachable = PingThread.FastPing(panelItem.Name);
                if (panelItem.IsReachable != bReachable)
                {
                    panelItem.IsReachable = bReachable;
                    m_View.RedrawFocusedItem();
                }
                if (!bReachable)
                {
                    var result = MessageBox.Show(
                        String.Format(Resources.PanelPresenter_UnreachableMsg, panelItem.Name), Resources.PanelPresenter_QueryCaption,
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (result != DialogResult.Yes)
                        panelItem = null;
                }
            }
            if (canReturnParent && panelItem is PanelItemDoubleDot)
                panelItem = panelItem.Parent;
            return panelItem;
        }

        /// <summary>
        /// Returns computer either focused item is computer or focused item is subitem of computer.
        /// </summary>
        /// <returns>a ComputerPanelItem or null</returns>
        public PanelItemBase GetFocusedComputer(bool pingAndAsk)
        {
            var panelItem = GetFocusedPanelItem(pingAndAsk, false);
            if (panelItem == null)
                return null;
            while (!(panelItem is IWmiComputer) && (panelItem.Parent != null))
                panelItem = panelItem.Parent;
            return panelItem;
        }

        public bool CommandLevelDown()
        {
            var panelItem = GetFocusedPanelItem(false, false);
            if (panelItem == null || Objects == null) 
                return false;
            if (panelItem is PanelItemDoubleDot)
                return CommandLevelUp();
            var result = AppPresenter.PanelFillers.FillerExists(panelItem);
            if (result)
            {
                Objects.FocusedItem = new PanelItemDoubleDot(panelItem);
                Objects.CurrentPath.Push(panelItem);
                ResetSortOrder();
                Objects.SyncRetrieveData(true);
                m_View.Filter.SetFilterText(string.Empty);
            }
            return result;
        }

        public bool CommandLevelUp()
        {
            if (Objects == null || Objects.CurrentPath.IsEmptyOrRoot) 
                return false;
            var panelItem = Objects.CurrentPath.Peek();
            if (panelItem == null || panelItem is PanelItemRoot) 
                return false;
            var result = AppPresenter.PanelFillers.FillerExists(panelItem);
            if (result)
            {
                Objects.FocusedItem = panelItem;
                Objects.CurrentPath.Pop();
                ResetSortOrder();
                Objects.SyncRetrieveData(true);
                m_View.Filter.SetFilterText(string.Empty);
            }
            return result;
        }

        internal void ColumnClick(int index)
        {
            // TODO Need sort lazy column
            var columns = AppPresenter.PanelColumns.GetColumns(m_Objects.DataType);
            if (columns[index].Callback != null)
                return;

            var order = SortOrder.None;
            if (index == Objects.Comparer.ColumnIndex)
                switch (Objects.Comparer.SortOrder)
                {
                    case SortOrder.None:
                    case SortOrder.Descending:
                        order = SortOrder.Ascending;
                        break;
                    case SortOrder.Ascending:
                        order = SortOrder.Descending;
                        break;
                }
            else
                order = SortOrder.Ascending;
            Objects.Comparer.ColumnIndex = index;
            Objects.Comparer.SortOrder = order;
            Objects.Sort(Objects.Comparer);
        }

        // TODO Column reorder
        internal bool ReorderColumns(int oldIndex, int newIndex)
        {
            //var result = AppPresenter.PanelColumns.ReorderColumns(m_Objects.DataType, oldIndex, newIndex);
            //if (result)
            //{
            //    if (m_Objects.Comparer.ColumnIndex == oldIndex)
            //        m_Objects.Comparer.ColumnIndex = newIndex;
            //    else if (m_Objects.Comparer.ColumnIndex == newIndex)
            //        m_Objects.Comparer.ColumnIndex = newIndex + 1;
            //    m_View.Refresh();
            //}
            //return result;
            return false;
        }

        internal void ColumnWidthChanged(int index, int newWidth)
        {
            // TODO need change internal column width but not always
            //var columns = AppPresenter.PanelColumns.GetColumns(m_Objects.DataType);
            //columns[index].Width = newWidth;
        }

        internal void ColumnRightClick(int columnIndex)
        {
            if (m_Objects.DataType != null)
            {
                var columns = AppPresenter.PanelColumns.GetColumns(m_Objects.DataType);
                m_View.ShowHeaderMenu(columns);
            }
        }

        internal void ShowHideColumnClick(int columnIndex)
        {
            if (columnIndex == 0) return;
            var columns = AppPresenter.PanelColumns.GetColumns(m_Objects.DataType);
            columns[columnIndex].Visible = !columns[columnIndex].Visible;
            SetupColumns();
            //AppPresenter.MainPages.PV_FocusedItemChanged(m_View, EventArgs.Empty);
        }
    }
}