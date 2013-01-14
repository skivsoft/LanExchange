using System;
using System.Windows.Forms;
using LanExchange.Model;
using LanExchange.View;
using LanExchange.UI;
using NLog;

namespace LanExchange.Presenter
{
    public class PagesPresenter
    {
        // logger object 
        private readonly static Logger logger = LogManager.GetCurrentClassLogger();

        private readonly IPagesView m_View;
        private readonly PagesModel m_Model;

        public event EventHandler PanelViewFocusedItemChanged;

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
        public void NewTab()
        {
            string NewTabName = InputBoxForm.Ask("Новая вкладка", "Введите имя", "", false);
            if (!String.IsNullOrEmpty(NewTabName))
            {
                m_Model.AddTab(new PanelItemList(NewTabName));
                m_Model.SaveSettings();
            }
        }

        public void CloseTab()
        {
            int Index = m_View.PopupSelectedIndex;
            if (CanCloseTab())
            {
                m_Model.DelTab(Index);
                m_Model.SaveSettings();
            }
        }

        // TODO: Need check duplicates on rename tab
        public void RenameTab()
        {
            int Index = m_View.PopupSelectedIndex;
            PanelItemList ItemList = GetModel().GetItem(Index);
            if (ItemList == null) return;
            string NewTabName = InputBoxForm.Ask("Переименование", "Введите имя", ItemList.TabName, false);
            if (NewTabName != null)
            {
                m_Model.RenameTab(Index, NewTabName);
                m_Model.SaveSettings();
            }
        }


        public void AddTabsToMenuItem(ToolStripMenuItem menuitem, EventHandler handler, bool bHideActive)
        {
            for (int i = 0; i < m_Model.Count; i++)
            {
                if (bHideActive && (!CanCloseTab() || (i == m_View.GetSelectedIndex())))
                    continue;
                string S = m_Model.GetTabName(i);
                var Item = new ToolStripMenuItem
                {
                    Checked = (i == m_View.GetSelectedIndex()),
                    Text = m_View.Ellipsis(S, 20),
                    ToolTipText = S,
                    Tag = i
                };
                Item.Click += handler;
                menuitem.DropDownItems.Add(Item);
            }
        }

        public void mSelectTab_Click(object sender, EventArgs e)
        {
            var menu = sender as ToolStripMenuItem;
            if (menu != null && m_View != null)
            {
                int Index = (int)menu.Tag;
                if (!Equals(m_View.GetSelectedIndex(), Index))
                    m_View.SetSelectedIndex(Index);
            }
        }

        // TOD uncomment SendPanelItems
        //public static void PanelView_SendPanelItems(PanelView lvSender, PanelView lvReceiver)
        //{
        //    if (lvSender == null || lvReceiver == null)
        //        return;
        //    PanelItemList ItemListSender = lvSender.GetPresenter().Objects;
        //    PanelItemList ItemListReceiver = lvReceiver.GetPresenter().Objects;

        //    int NumAdded = 0;
        //    //int[] Indices = new int[lvSender.SelectedIndices.Count];
        //    foreach (int Index in lvSender.SelectedIndices)
        //    {
        //        PanelItem PItem = ItemListSender.Get(lvSender.Items[Index].Text);
        //        if (PItem != null)
        //        {
        //            ItemListReceiver.Add(PItem);
        //            //Indices[Index] = ItemListReceiver.Keys.IndexOf(PItem.Name);
        //            NumAdded++;
        //        }
        //    }
        //    ItemListReceiver.ApplyFilter();
        //    //View.ListView_Update(lvReceiver);
        //    lvReceiver.Focus();
        //    // выделяем добавленные итемы
        //    if (lvReceiver.Items.Count > 0)
        //    {
        //        lvReceiver.SelectItem(0);
        //        /*
        //        foreach(int Index in Indices)
        //            if (Index != -1)
        //                lvReceiver.SelectedIndices.Add(Index);
        //         */
        //    }
        //}

        // TODO uncomment mSendToNewTab_Click
        //public void mSendToNewTab_Click(object sender, EventArgs e)
        //{
        //    if (View.SelectedIndex == 0/* && 
        //        MainForm.GetInstance().CompBrowser.InternalStack.Count > 0*/)
        //        return;
        //    string NewTabName = TabView.InputBoxAsk("Новая вкладка", "Введите имя", "");
        //    if (NewTabName == null)
        //        return;
        //    ListView LV = View.GetActiveListView();
        //    PanelItemList ItemList = PanelItemList.GetObject(LV);
        //    PanelItemList Info = new PanelItemList(NewTabName);
        //    //Info.Items = ItemList.ListView_GetSelected(LV, false);
        //    Model.AddTab(Info);
        //    View.SelectedIndex = Model.Count - 1;
        //    Model.StoreSettings();
        //}

        // TODO uncommment mSendToSelectedTab_Click
        //public void mSendToSelectedTab_Click(object sender, EventArgs e)
        //{
        //    if (View.SelectedIndex == 0/* &&
        //        MainForm.GetInstance().CompBrowser.InternalStack.Count > 0*/)
        //        return;
        //    ListView lvSender = View.GetActiveListView();
        //    int Index = (int)(sender as ToolStripMenuItem).Tag;
        //    View.SelectedIndex = Index;
        //    ListView lvReceiver = View.GetActiveListView();
        //    ListView_SendPanelItems(lvSender, lvReceiver);
        //    Model.StoreSettings();
        //}

        public bool CanCloseTab()
        {
            return m_View.TabPagesCount > 1;
        }

        public void Model_AfterAppendTab(object sender, PanelItemListEventArgs e)
        {
            logger.Info("AfterAppendTab(\"{0}\")", e.Info.TabName);
            // create panel
            var PV = new PanelView { Dock = DockStyle.Fill };
            PV.GetPresenter().Objects = e.Info;
            ListView LV = PV.Controls[0] as ListView;
            if (LV != null)
            {
                LV.SmallImageList = LanExchangeIcons.SmallImageList;
                LV.LargeImageList = LanExchangeIcons.LargeImageList;
                if (MainForm.Instance != null)
                    MainForm.Instance.tipComps.SetToolTip(LV, " ");
            }
            PV.FocusedItemChanged += PV_FocusedItemChanged;
            // add new tab and insert panel into it
            m_View.NewTabFromItemList(e.Info);
            m_View.AddControl(m_View.TabPagesCount - 1, PV);
            // set update event
            PanelPresenter presenter = PV.GetPresenter();
            e.Info.Changed += presenter.Items_Changed;
            e.Info.SubscriptionChanged += Item_SubscriptionChanged;
        }

        public void PV_FocusedItemChanged(object sender, EventArgs e)
        {
            if (PanelViewFocusedItemChanged != null)
                PanelViewFocusedItemChanged(sender, e);
        }

        public void Item_SubscriptionChanged(object sender, EventArgs e)
        {
            PanelItemList Item = sender as PanelItemList;
            if (Item == null) return;
            int Index = m_Model.GetItemIndex(Item);
            if (Index != -1)
                m_View.SetTabToolTip(Index, Item.GetTabToolTip());
        }

        public void Model_AfterRemove(object sender, IndexEventArgs e)
        {
            m_View.RemoveTabAt(e.Index);
        }

        public void Model_AfterRename(object sender, PanelItemListEventArgs e)
        {
            m_View.SelectedTabText = e.Info.TabName;
        }

        public void Model_IndexChanged(object sender, IndexEventArgs e)
        {
            logger.Info("PagesModel_IndexChanged({0})", e.Index);
            m_View.SetSelectedIndex(e.Index);
            m_View.FocusPanelView();
        }
    }

}
