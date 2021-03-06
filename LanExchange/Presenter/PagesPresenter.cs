﻿using System;
using LanExchange.Ioc;
using LanExchange.SDK;

namespace LanExchange.Presenter
{
    public class PagesPresenter : PresenterBase<IPagesView>, IPagesPresenter, IDisposable
    {
        private readonly IPagesModel m_Model;

        public event EventHandler PanelViewFocusedItemChanged;
        public event EventHandler PanelViewFilterTextChanged;

        public PagesPresenter(IPagesModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");
            m_Model = model;
            App.Resolve<IDisposableManager>().RegisterInstance(this);
            m_Model.AfterAppendTab += Model_AfterAppendTab;
            m_Model.AfterRemove += Model_AfterRemove;
            m_Model.IndexChanged += Model_IndexChanged;
        }

        public void Dispose()
        {
            m_Model.Dispose();
        }

        public bool CanSendToNewTab()
        {
            var sourcePV = View.ActivePanelView;
            if (sourcePV == null) 
                return false;
            var indexes = sourcePV.SelectedIndexes.GetEnumerator();
            if (!indexes.MoveNext()) 
                return false;
            return sourcePV.Presenter.Objects.Count > 1;
        }

        public void CommandSendToNewTab()
        {
            //if (!CanSendToNewTab()) return;
            //var newTabName = m_Model.GenerateTabName();
            //var sourcePV = View.ActivePanelView;
            //var sourceObjects = sourcePV.Presenter.Objects;
            //var destObjects = App.Resolve<IPanelModel>();
            //destObjects.TabName = newTabName;
            //destObjects.DataType = sourceObjects.DataType;
            ////destObjects.CurrentPath.Push(PanelItemRoot.ROOT_OF_USERITEMS);

            //foreach (int index in sourcePV.SelectedIndexes)
            //{
            //    var panelItem = sourceObjects.GetItemAt(index);
            //    if (panelItem.GetType().Name.Equals(destObjects.DataType))
            //    {
            //        // add item to new panel
            //        var newItem = (PanelItemBase) panelItem.Clone();
            //        newItem.Parent = null;
            //        destObjects.Items.Add(newItem);
            //    }
            //}
            ////destObjects.SyncRetrieveData(true);
            //// add tab
            //m_Model.AddTab(destObjects);
            ////m_View.SelectedIndex = m_Model.Count - 1;
            //View.ActivePanelView.Presenter.UpdateItemsAndStatus();
        }

        public bool CanPasteItems()
        {
            if (View.ActivePanelView == null)
                return false;
            var clipboard = App.Resolve<IClipboardService>();
            var obj = clipboard.GetDataObject();
            if (obj == null)
                return false;
            if (!obj.GetDataPresent(typeof(PanelItemBaseHolder)))
                return false;
            var items = (PanelItemBaseHolder)obj.GetData(typeof(PanelItemBaseHolder));
            if (items == null)
                return false;
            return !View.ActivePanelView.Presenter.Objects.TabName.Equals(items.Context);
        }

        public void CommandPasteItems()
        {
            if (!CanPasteItems()) return;
            var clipboard = App.Resolve<IClipboardService>();
            var obj = clipboard.GetDataObject();
            if (obj == null) return;
            var items = (PanelItemBaseHolder)obj.GetData(typeof(PanelItemBaseHolder));
            var destObjects = m_Model.GetItem(m_Model.SelectedIndex);
            destObjects.DataType = items.DataType;
            //destObjects.CurrentPath.Push(PanelItemRoot.ROOT_OF_USERITEMS);
            foreach (var panelItem in items)
                if (panelItem.GetType().Name.Equals(destObjects.DataType))
                {
                    if (destObjects.Contains(panelItem))
                        continue;
                    // add item to new panel
                    var newItem = (PanelItemBase) panelItem.Clone();
                    //newItem.Parent = PanelItemRootBase.ROOT_OF_USERITEMS;
                    destObjects.Items.Add(newItem);
                }
            destObjects.AsyncRetrieveData(true);
            //m_View.ActivePanelView.Presenter.UpdateItemsAndStatus();
        }

        public void CommandDeleteItems()
        {
            var panelView = View.ActivePanelView;
            if (panelView == null) return;
            var indexes = panelView.SelectedIndexes.GetEnumerator();
            if (!indexes.MoveNext()) return;
            var modified = false;
            var firstIndex = -1;
            foreach (int index in panelView.SelectedIndexes)
            {
                var comp = panelView.Presenter.Objects.GetItemAt(index);
                if (comp.ImageName.Contains(PanelImageNames.GREEN_POSTFIX) || comp.ImageName.Contains(PanelImageNames.HIDDEN_POSTFIX))
                {
                    if (firstIndex == -1)
                        firstIndex = index-1;
                    panelView.Presenter.Objects.Items.Remove(comp);
                    modified = true;
                }
            }
            panelView.ClearSelected();
            if (modified)
            {
                if (firstIndex < 0 || firstIndex > panelView.Presenter.Objects.FilterCount - 1)
                    firstIndex = panelView.Presenter.Objects.FilterCount - 1;
                if (firstIndex >= 0)
                    panelView.Presenter.Objects.FocusedItem = panelView.Presenter.Objects.GetItemAt(firstIndex);
                panelView.Presenter.Objects.AsyncRetrieveData(false);
            }
        }

        public void CommandCloseTab()
        {
            var index = View.PopupSelectedIndex;
            m_Model.DelTab(index);
        }

        public void CommanCloseOtherTabs()
        {
            var popupIndex = View.PopupSelectedIndex;
            for (int index = m_Model.Count - 1; index >= 0; index--)
                if (index != popupIndex)
                    m_Model.DelTab(index);
            m_Model.SelectedIndex = 0;
        }

        public int IndexOf(IPanelModel model)
        {
            if (model == null)
                return -1;
            for (int index = 0; index < m_Model.Count; index++)
                if (m_Model.GetItem(index) == model)
                    return index;
            return -1;
        }

        public void Model_AfterAppendTab(object sender, PanelModelEventArgs e)
        {
            // create panel
            var panelView = View.CreatePanelView(e.Info);
            // set update event
            IPanelPresenter presenter = panelView.Presenter;
            presenter.Objects = e.Info;
            //m_View.SelectedIndex = m_View.TabPagesCount - 1;
            e.Info.Changed += (o, args) => presenter.UpdateItemsAndStatus();
            e.Info.TabNameUpdated += InfoOnTabNameUpdated;
            e.Info.OnTabNameUpdated();
            //e.Info.SubscriptionChanged += Item_SubscriptionChanged;
            // update items
            //e.Info.DataChanged(null, ConcreteSubject.s_UserItems);
            panelView.Presenter.ResetSortOrder();
            e.Info.AsyncRetrieveData(false);
        }

        private void InfoOnTabNameUpdated(object sender, EventArgs eventArgs)
        {
            var model = sender as IPanelModel;
            if (model == null) return;
            var index = IndexOf(model);
            if (index != -1)
            {
                View.SetTabText(index, model.TabName);
                View.SetTabImage(index, App.Images.IndexOf(model.ImageName));
            }
        }

        public void Model_AfterRemove(object sender, PanelIndexEventArgs e)
        {
            View.RemoveTabAt(e.Index);
        }

        public void Model_AfterRename(object sender, PanelIndexEventArgs e)
        {
            var model = m_Model.GetItem(e.Index);
            View.SetTabText(e.Index, model.TabName);
        }

        public void Model_IndexChanged(object sender, PanelIndexEventArgs e)
        {
            View.SelectedIndex = e.Index;
            View.FocusPanelView();
        }

        public void DoPanelViewFocusedItemChanged(object sender, EventArgs e)
        {
            if (PanelViewFocusedItemChanged != null)
                PanelViewFocusedItemChanged(sender, e);
        }

        public void DoPanelViewFilterTextChanged(object sender, EventArgs e)
        {
            if (PanelViewFilterTextChanged != null)
                PanelViewFilterTextChanged(sender, e);
        }

        public bool SelectTabByName(string tabName)
        {
            for (int index = 0; index < m_Model.Count; index++ )
                if (m_Model.GetTabName(index).Equals(tabName))
                {
                    SelectedIndex = index;
                    return true;
                }
            return false;
        }

        public int Count
        {
            get { return m_Model.Count; }
        }

        public int SelectedIndex
        {
            get { return m_Model.SelectedIndex; }
            set { m_Model.SelectedIndex = value; }
        }

        public void SaveInstant()
        {
            m_Model.SaveSettings();
        }

        public string GetTabName(int index)
        {
            return m_Model.GetTabName(index);
        }

        public void SetupPanelViewEvents(IPanelView panelView)
        {
            panelView.FocusedItemChanged += DoPanelViewFocusedItemChanged;
            panelView.FilterTextChanged += DoPanelViewFilterTextChanged;
        }

        public IPanelModel GetItem(int index)
        {
            return m_Model.GetItem(index);
        }

        public void LoadSettings()
        {
            IPagesModel model;
            m_Model.LoadSettings(out model);
            m_Model.SetLoadedModel(model);
        }

        public bool AddTab(IPanelModel info)
        {
            return m_Model.AddTab(info);
        }

        public void UpdateTabName(int index)
        {
            var model = m_Model.GetItem(index);
            if (model != null)
                View.SetTabText(index, model.TabName);
        }
    }
}