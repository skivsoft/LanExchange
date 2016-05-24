using System;
using LanExchange.Properties;
using LanExchange.SDK;
using System.Diagnostics.Contracts;
using System.Linq;

namespace LanExchange.Presenter
{
    public class PanelPresenter : PresenterBase<IPanelView>, IPanelPresenter, IDisposable
    {
        private readonly IPanelFillerManager panelFillers;
        private readonly IPanelColumnManager panelColumns;
        private readonly IPagesPresenter pagesPresenter;
        private IPanelModel objects;

        public event EventHandler CurrentPathChanged;

        public PanelPresenter(
            IPanelFillerManager panelFillers,
            IPanelColumnManager panelColumns,
            IPagesPresenter pagesPresenter)
        {
            Contract.Requires<ArgumentNullException>(panelFillers != null);
            Contract.Requires<ArgumentNullException>(panelColumns != null);
            Contract.Requires<ArgumentNullException>(pagesPresenter != null);

            this.panelFillers = panelFillers;
            this.panelColumns = panelColumns;
            this.pagesPresenter = pagesPresenter;
        }

        public void Dispose()
        {
            
        }

        public void SetupColumns()
        {
            if (objects.DataType == null)
                return;

            View.ColumnsClear();
            int index = 0;
            int visibleIndex = 0;
            foreach (var column in panelColumns.GetColumns(objects.DataType))
            {
                if (column.Visible)
                {
                    View.AddColumn(column);
                    if (index == objects.Comparer.ColumnIndex)
                        View.SetColumnMarker(visibleIndex, objects.Comparer.SortOrder);
                    ++visibleIndex;
                }
                ++index;
            }
        }

        public void ResetSortOrder()
        {
            objects.Comparer.ColumnIndex = 0;
            objects.Comparer.SortOrder = PanelSortOrder.Ascending;
        }

        public void UpdateItemsAndStatus()
        {
            if (objects == null) return;
            // refresh only for current page
            var panelModel = pagesPresenter.GetItem(pagesPresenter.SelectedIndex);
            if (panelModel == null || !objects.Equals(panelModel)) 
                return;
            // get number of visible items (filtered) and number of total items
            var showCount = objects.FilterCount;
            var totalCount = objects.Count;
            if (objects.HasBackItem)
            {
                showCount--;
                totalCount--;
            }
            if (showCount != totalCount)
                App.MainView.ShowStatusText(App.TR.PluralForm(Resources.PanelPresenter_Items2, totalCount), showCount, totalCount);
            else
                App.MainView.ShowStatusText(Resources.PanelPresenter_Items1, showCount);
            SetupColumns();
            View.SetVirtualListSize(objects.FilterCount);
            if (objects.FilterCount > 0)
            {
                var index = Objects.IndexOf(panelModel.FocusedItem);
                View.FocusedItemIndex = index;
            }
            if (View.Filter != null)
                View.Filter.UpdateFromModel(Objects);
        }

        private void CurrentPath_Changed(object sender, EventArgs e)
        {
            if (objects != null && CurrentPathChanged != null)
                CurrentPathChanged(sender, e);
        }

        public IPanelModel Objects
        {
            get { return objects; }
            set
            {
                if (objects != null)
                    objects.CurrentPath.Changed -= CurrentPath_Changed;
                objects = value;
                if (objects != null)
                    objects.CurrentPath.Changed += CurrentPath_Changed;
                if (View.Filter != null)
                    View.Filter.Presenter.SetModel(value);
            }
        }

        public PanelItemBase GetFocusedPanelItem(bool canReturnParent)
        {
            var panelItem = View.FocusedItem;
            if (canReturnParent && panelItem is PanelItemDoubleDot)
                panelItem = panelItem.Parent;
            return panelItem;
        }

        public bool CommandLevelDown()
        {
            var panelItem = GetFocusedPanelItem(false);
            if (panelItem == null || Objects == null) 
                return false;
            if (panelItem is PanelItemDoubleDot)
                return CommandLevelUp();
            if (!panelFillers.FillerExists(panelItem)) return false;
            Objects.FocusedItem = new PanelItemDoubleDot(panelItem);
            Objects.CurrentPath.Push(panelItem);
            Objects.OnTabNameUpdated();
            ResetSortOrder();
            var syncResult = Objects.RetrieveData(RetrieveMode.Sync, true);
            Objects.SetFillerResult(syncResult, true);
            View.Filter.SetFilterText(string.Empty);
            Objects.AsyncRetrieveData(true);
            return true;
        }

        public bool CommandLevelUp()
        {
            if (Objects == null || !Objects.CurrentPath.Any()) 
                return false;
            var panelItem = Objects.CurrentPath.Peek();
            if (!panelItem.Any() || panelItem.Single() is PanelItemRootBase) 
                return false;
            if (!panelFillers.FillerExists(panelItem.Single())) return false;
            Objects.FocusedItem = panelItem.Single();
            Objects.CurrentPath.Pop();
            Objects.OnTabNameUpdated();
            ResetSortOrder();
            var syncResult = Objects.RetrieveData(RetrieveMode.Sync, true);
            Objects.SetFillerResult(syncResult, true);
            View.Filter.SetFilterText(string.Empty);
            Objects.AsyncRetrieveData(true);
            return true;
        }

        public void ColumnClick(int index)
        {
            // TODO Need sort lazy column
            var columns = panelColumns.GetColumns(objects.DataType).ToList();
            if (columns[index].Callback != null)
                return;

            var order = PanelSortOrder.None;
            if (index == Objects.Comparer.ColumnIndex)
                switch (Objects.Comparer.SortOrder)
                {
                    case PanelSortOrder.None:
                    case PanelSortOrder.Descending:
                        order = PanelSortOrder.Ascending;
                        break;
                    case PanelSortOrder.Ascending:
                        order = PanelSortOrder.Descending;
                        break;
                }
            else
                order = PanelSortOrder.Ascending;
            Objects.Comparer.ColumnIndex = index;
            Objects.Comparer.SortOrder = order;
            Objects.Sort(Objects.Comparer);
        }

        // TODO Column reorder
        public bool ReorderColumns(int oldIndex, int newIndex)
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

        public void ColumnWidthChanged(int index, int newWidth)
        {
            // TODO need change internal column width but not always
            //var columns = AppPresenter.PanelColumns.GetColumns(m_Objects.DataType);
            //columns[index].Width = newWidth;
        }

        public void ColumnRightClick(int columnIndex)
        {
            if (objects.DataType != null)
            {
                var columns = panelColumns.GetColumns(objects.DataType);
                View.ShowHeaderMenu(columns);
            }
        }

        public void ShowHideColumnClick(int columnIndex)
        {
            if (columnIndex == 0) return;
            var columns = panelColumns.GetColumns(objects.DataType).ToList();
            columns[columnIndex].Visible = !columns[columnIndex].Visible;
            SetupColumns();
        }
    }
}