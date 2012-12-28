using System;
using System.Windows.Forms;
using LanExchange.Model;
using LanExchange.View;
using LanExchange.UI;
using NLog;
using GongSolutions.Shell;
using GongSolutions.Shell.Interop;
using System.Drawing;

namespace LanExchange.Presenter
{
    public class TabControlPresenter
    {
        // logger object 
        private readonly static Logger logger = LogManager.GetCurrentClassLogger();

        private readonly ITabControlView m_View;
        private readonly TabControlModel m_Model;

        public TabControlPresenter(ITabControlView pages)
        {
            m_View = pages;
            m_Model = new TabControlModel(m_View.Name);
            m_Model.AfterAppendTab += Model_AfterAppendTab;
            m_Model.AfterRemove += Model_AfterRemove;
            m_Model.AfterRename += Model_AfterRename;
            m_Model.SelectedIndexChanged += Model_SelectedIndexChanged;
            m_Model.LoadSettings();
        }

        public int SelectedIndex
        {
            get
            {
                return m_View.SelectedIndex;
            }
            set
            {
                m_View.SelectedIndex = value;
            }
        }

        public TabControlModel GetModel()
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
                m_Model.StoreSettings();
            }
        }

        public void CloseTab()
        {
            int Index = m_View.SelectedIndex;
            if (CanCloseTab(Index))
            {
                m_Model.DelTab(Index);
                m_Model.StoreSettings();
            }
        }

        // TODO: Need check duplicates on rename tab
        public void RenameTab()
        {
            int Index = m_View.SelectedIndex;
            string NewTabName = InputBoxForm.Ask("Переименование", "Введите имя", m_View.SelectedTabText, false);
            if (NewTabName != null)
            {
                m_Model.RenameTab(Index, NewTabName);
                m_Model.StoreSettings();
            }
        }

        public void AddTabsToMenuItem(ToolStripMenuItem menuitem, EventHandler handler, bool bHideActive)
        {
            for (int i = 0; i < m_Model.Count; i++)
            {
                if (bHideActive && (!CanCloseTab(i) || (i == m_View.SelectedIndex)))
                    continue;
                ToolStripMenuItem Item = new ToolStripMenuItem
                {
                    Checked = (i == m_View.SelectedIndex),
                    Text = m_Model.GetTabName(i),
                    Tag = i
                };
                Item.Click += handler;
                menuitem.DropDownItems.Add(Item);
            }
        }

        public void mSelectTab_Click(object sender, EventArgs e)
        {
            int Index = (int)(sender as ToolStripMenuItem).Tag;
            if (m_View.SelectedIndex != Index)
                m_View.SelectedIndex = Index;
        }

        public static void PanelView_SendPanelItems(PanelView lvSender, PanelView lvReceiver)
        {
            if (lvSender == null || lvReceiver == null)
                return;
            PanelItemList ItemListSender = lvSender.Objects;
            PanelItemList ItemListReceiver = lvReceiver.Objects;

            int NumAdded = 0;
            //int[] Indices = new int[lvSender.SelectedIndices.Count];
            foreach (int Index in lvSender.SelectedIndices)
            {
                PanelItem PItem = ItemListSender.Get(lvSender.Items[Index].Text);
                if (PItem != null)
                {
                    ItemListReceiver.Add(PItem);
                    //Indices[Index] = ItemListReceiver.Keys.IndexOf(PItem.Name);
                    NumAdded++;
                }
            }
            ItemListReceiver.ApplyFilter();
            //View.ListView_Update(lvReceiver);
            lvReceiver.Focus();
            // выделяем добавленные итемы
            if (lvReceiver.Items.Count > 0)
            {
                lvReceiver.SelectItem(0);
                /*
                foreach(int Index in Indices)
                    if (Index != -1)
                        lvReceiver.SelectedIndices.Add(Index);
                 */
            }
        }

        public void mSendToNewTab_Click(object sender, EventArgs e)
        {
            //if (View.SelectedIndex == 0/* && 
            //    MainForm.GetInstance().CompBrowser.InternalStack.Count > 0*/)
            //    return;
            //string NewTabName = TabView.InputBoxAsk("Новая вкладка", "Введите имя", "");
            //if (NewTabName == null)
            //    return;
            //ListView LV = View.GetActiveListView();
            //PanelItemList ItemList = PanelItemList.GetObject(LV);
            //PanelItemList Info = new PanelItemList(NewTabName);
            ////Info.Items = ItemList.ListView_GetSelected(LV, false);
            //Model.AddTab(Info);
            //View.SelectedIndex = Model.Count - 1;
            //Model.StoreSettings();
        }

        public void mSendToSelectedTab_Click(object sender, EventArgs e)
        {
            //if (View.SelectedIndex == 0/* &&
            //    MainForm.GetInstance().CompBrowser.InternalStack.Count > 0*/)
            //    return;
            //ListView lvSender = View.GetActiveListView();
            //int Index = (int)(sender as ToolStripMenuItem).Tag;
            //View.SelectedIndex = Index;
            //ListView lvReceiver = View.GetActiveListView();
            //ListView_SendPanelItems(lvSender, lvReceiver);
            //Model.StoreSettings();
        }

        public bool CanCloseTab(int Index)
        {
            return m_View.TabPagesCount > 1;
        }

        public void Model_AfterAppendTab(object sender, PanelItemListEventArgs e)
        {
            logger.Info("AfterAppendTab({0})", e.Info.TabName);
            // create panel
            var PV = new PanelView { Dock = DockStyle.Fill, Objects = e.Info };
            //PV.SmallImageList = MainForm.Instance.ilSmall;
            //PV.LargeImageList = MainForm.Instance.ilLarge;
            //SystemImageList.UseSystemImageList(PV.Controls[0] as ListView);
            ListView LV = PV.Controls[0] as ListView;
            LV.SmallImageList = LanExchangeIcons.SmallImageList;
            LV.LargeImageList = LanExchangeIcons.LargeImageList;
            PV.FocusedItemChanged += MainForm.Instance.PV_FocusedItemChanged;
            // add new tab and insert panel into it
            m_View.NewTab(e.Info.TabName);
            m_View.AddControl(m_View.TabPagesCount - 1, PV);
            // set update event
            e.Info.Changed += PV.Items_Changed;
        }

        public void Model_AfterRemove(object sender, IndexEventArgs e)
        {
            m_View.RemoveTabAt(e.Index);
        }

        public void Model_AfterRename(object sender, PanelItemListEventArgs e)
        {
            m_View.SelectedTabText = e.Info.TabName;
        }

        public void Model_SelectedIndexChanged(object sender, IndexEventArgs e)
        {
            m_View.SelectedIndex = e.Index;
        }
    }

}
