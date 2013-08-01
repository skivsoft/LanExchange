using System;
using LanExchange.Model;
using LanExchange.Model.Panel;
using LanExchange.SDK;

namespace LanExchange.Presenter
{
    public class PagesPresenter
    {
        private readonly IPagesView m_View;
        private readonly PagesModel m_Model;

        public event EventHandler PanelViewFocusedItemChanged;
        public event EventHandler PanelViewFilterTextChanged;

        public PagesPresenter(IPagesView pages)
        {
            m_View = pages;
            m_Model = new PagesModel();
            m_Model.AfterAppendTab += Model_AfterAppendTab;
            m_Model.AfterRemove += Model_AfterRemove;
            m_Model.AfterRename += Model_AfterRename;
            m_Model.IndexChanged += Model_IndexChanged;
        }

        public PagesModel GetModel()
        {
            return m_Model;
        }

        // TODO: Need check duplicates on new tab
        public void CommandNewTab()
        {
            var newTabName = InputBoxPresenter.Ask("Новая вкладка", "Введите имя", "", false);
            if (newTabName != null)
            {
                m_Model.AddTab(new PanelItemList(newTabName));
                m_Model.SaveSettings();
            }
        }

        public void CommandSendToNewTab()
        {
            // TODO UNCOMMENT THIS!
            //var newTabName = InputBoxPresenter.Ask("Новая вкладка", "Введите имя", "", false);
            //if (newTabName == null) return;
            //var sourcePV = m_View.ActivePanelView;
            //if (sourcePV == null) return;
            //var sourceObjects = sourcePV.Presenter.Objects;
            //var destObjects = new PanelItemList(newTabName);
            //foreach (int index in sourcePV.SelectedIndexes)
            //{
            //    var PItem = sourceObjects.GetItemAt(index);
            //    if (PItem == null || PItem.Name == PanelItemBase.s_DoubleDot) 
            //        continue;
            //    if (!(PItem is ComputerPanelItem))
            //        continue;
            //    // copy computer and set partent to null
            //    var newItem = new ComputerPanelItem(null, (PItem as ComputerPanelItem).SI);
            //    newItem.ParentSubject = ConcreteSubject.s_UserItems;
            //    // add item to new panel
            //    destObjects.Items.Add(newItem);
            //}
            //// add tab
            //m_Model.AddTab(destObjects);
            //m_View.SelectedIndex = m_Model.Count-1;
            //m_Model.SaveSettings();
        }

        public void CommandCloseTab()
        {
            int Index = m_View.PopupSelectedIndex;
            if (CanCloseTab())
            {
                m_Model.DelTab(Index);
                m_Model.SaveSettings();
            }
        }

        // TODO: Need check duplicates on rename tab
        public void CommandRenameTab()
        {
            int Index = m_View.PopupSelectedIndex;
            PanelItemList ItemList = GetModel().GetItem(Index);
            if (ItemList == null) return;
            string NewTabName = InputBoxPresenter.Ask("Переименование", "Введите имя", ItemList.TabName, false);
            if (NewTabName != null)
            {
                m_Model.RenameTab(Index, NewTabName);
                m_Model.SaveSettings();
            }
        }

        public bool CanCloseTab()
        {
            return m_View.TabPagesCount > 1;
        }

        public void Model_AfterAppendTab(object sender, PanelItemListEventArgs e)
        {
            // create panel
            var PV = m_View.CreatePanelView(e.Info);
            // set update event
            IPanelPresenter presenter = PV.Presenter;
            presenter.Objects = e.Info;
            e.Info.Changed += (o, args) => presenter.UpdateItemsAndStatus();
            //e.Info.SubscriptionChanged += Item_SubscriptionChanged;
            // update items
            //e.Info.DataChanged(null, ConcreteSubject.s_UserItems);
            e.Info.SyncRetrieveData();
        }

        public void Model_AfterRemove(object sender, PanelIndexEventArgs e)
        {
            m_View.RemoveTabAt(e.Index);
        }

        public void Model_AfterRename(object sender, PanelItemListEventArgs e)
        {
            m_View.SelectedTabText = e.Info.TabName;
        }

        public void Model_IndexChanged(object sender, PanelIndexEventArgs e)
        {
            m_View.SelectedIndex = e.Index;
            m_View.FocusPanelView();
        }

        public void PV_FocusedItemChanged(object sender, EventArgs e)
        {
            if (PanelViewFocusedItemChanged != null)
                PanelViewFocusedItemChanged(sender, e);
        }

        public void PV_FilterTextChanged(object sender, EventArgs e)
        {
            if (PanelViewFilterTextChanged != null)
                PanelViewFilterTextChanged(sender, e);
        }

        public void Item_SubscriptionChanged(object sender, EventArgs e)
        {
            PanelItemList Item = sender as PanelItemList;
            if (Item == null) return;
            int Index = m_Model.GetItemIndex(Item);
            if (Index != -1)
                m_View.SetTabToolTip(Index, Item.ToolTipText);
        }
    }

}
