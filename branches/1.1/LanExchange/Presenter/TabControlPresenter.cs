using System;
using System.Windows.Forms;
using LanExchange.Model;
using LanExchange.View;
using NLog;
using LanExchange.UI;

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
            m_Model.AfterAppendTab += AfterAppendTab;
            m_Model.AfterRemove += AfterRemove;
            m_Model.AfterRename += AfterRename;
            m_Model.LoadSettings();
        }

        public TabControlModel GetModel()
        {
            return m_Model;
        }

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

        public void RenameTab()
        {
            //int Index = View.SelectedIndex;
            //string NewTabName = TabView.InputBoxAsk("Переименование", "Введите имя", View.SelectedTabText);
            //if (NewTabName != null)
            //{
            //    Model.RenameTab(Index, NewTabName);
            //    Model.StoreSettings();
            //}
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

        public static void ListView_SendPanelItems(ListView lvSender, ListView lvReceiver)
        {
            if (lvSender == null || lvReceiver == null)
                return;
            PanelItemList ItemListSender = PanelItemList.GetObject(lvSender);
            PanelItemList ItemListReceiver = PanelItemList.GetObject(lvReceiver);

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
                lvReceiver.SelectedIndices.Add(0);
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
        /// <summary>
        /// Заполняет список страниц внутри модели данными из представления.
        /// </summary>
        /*
        public void UpdateModelFromView()
        {
            Model.Clear();
            foreach (TabPage Tab in View.TabPages)
            {
                if (Tab.Controls.Count == 0) continue;
                PanelItemList Info = new PanelItemList(Tab.Text);
                ListView LV = (ListView)Tab.Controls[0];
                PanelItemList ItemList = LV.GetObject();
                Info.Items = ItemList.ListView_GetSelected(LV, true);
                Info.FilterText = ItemList.FilterText;
                Info.CurrentView = LV.View;
                Model.InternalAdd(Info);
            }
        }
        */


        public void AfterAppendTab(object sender, PanelItemListEventArgs e)
        {
            logger.Info("AfterAppendTab()");
            m_View.NewTab(e.Info.TabName);
        }

        public void AfterRemove(object sender, IndexEventArgs e)
        {
            //m_Pages.TabPages.RemoveAt(e.Index);
        }

        public void AfterRename(object sender, PanelItemListEventArgs e)
        {
            //m_Pages.SelectedTab.Text = e.Info.TabName;
        }
    }

}
