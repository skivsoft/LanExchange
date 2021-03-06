﻿using System;
using LanExchange.Ioc;
using LanExchange.Properties;
using LanExchange.SDK;

namespace LanExchange.Presenter
{
    public class PanelPresenter : PresenterBase<IPanelView>, IPanelPresenter, IDisposable
    {
        private IPanelModel m_Objects;

        public event EventHandler CurrentPathChanged;

        public void Dispose()
        {
            
        }

        public void SetupColumns()
        {
            if (m_Objects.DataType == null)
                return;
            View.ColumnsClear();
            var columns = App.PanelColumns.GetColumns(m_Objects.DataType);
            if (columns == null) return;
            int j = 0;
            for (int i = 0; i < columns.Count; i++ )
                if (columns[i].Visible)
                {
                    View.AddColumn(columns[i]);
                    if (i == m_Objects.Comparer.ColumnIndex)
                        View.SetColumnMarker(j, m_Objects.Comparer.SortOrder);
                    ++j;
                }
        }

        public void ResetSortOrder()
        {
            m_Objects.Comparer.ColumnIndex = 0;
            m_Objects.Comparer.SortOrder = PanelSortOrder.Ascending;
        }

        public void UpdateItemsAndStatus()
        {
            if (m_Objects == null) return;
            // refresh only for current page
            var presenter = App.MainPages;
            var panelModel = presenter.GetItem(presenter.SelectedIndex);
            if (panelModel == null || !m_Objects.Equals(panelModel)) 
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
                App.MainView.ShowStatusText(App.TR.PluralForm(Resources.PanelPresenter_Items2, totalCount), showCount, totalCount);
            else
                App.MainView.ShowStatusText(Resources.PanelPresenter_Items1, showCount);
            SetupColumns();
            View.SetVirtualListSize(m_Objects.FilterCount);
            if (m_Objects.FilterCount > 0)
            {
                var index = Objects.IndexOf(panelModel.FocusedItem);
                View.FocusedItemIndex = index;
            }
            if (View.Filter != null)
                View.Filter.UpdateFromModel(Objects);
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
            if (!App.PanelFillers.FillerExists(panelItem)) return false;
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
            if (Objects == null || Objects.CurrentPath.IsEmptyOrRoot) 
                return false;
            var panelItem = Objects.CurrentPath.Peek();
            if (panelItem == null || panelItem is PanelItemRootBase) 
                return false;
            if (!App.PanelFillers.FillerExists(panelItem)) return false;
            Objects.FocusedItem = panelItem;
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
            var columns = App.PanelColumns.GetColumns(m_Objects.DataType);
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
            if (m_Objects.DataType != null)
            {
                var columns = App.PanelColumns.GetColumns(m_Objects.DataType);
                View.ShowHeaderMenu(columns);
            }
        }

        public void ShowHideColumnClick(int columnIndex)
        {
            if (columnIndex == 0) return;
            var columns = App.PanelColumns.GetColumns(m_Objects.DataType);
            columns[columnIndex].Visible = !columns[columnIndex].Visible;
            SetupColumns();
            //AppPresenter.MainPages.PV_FocusedItemChanged(m_View, EventArgs.Empty);
        }
    }
}