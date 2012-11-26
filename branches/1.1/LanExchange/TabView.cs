using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using LanExchange.Forms;
using LanExchange.Network;

namespace LanExchange
{
    // Представление отвечает за отображение информации (визуализация). 
    // Часто в качестве представления выступает
    public class TabView
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        TabControl pages;

        public TabView(TabControl Pages)
        {
            this.pages = Pages;
        }

        public string Name { get { return this.pages.Name; }}

        public int SelectedIndex 
        { 
            get { return pages.SelectedIndex; }
            set { pages.SelectedIndex = value; }
        }

        public TabControl.TabPageCollection TabPages { get { return pages.TabPages; } }

        public string SelectedTabText 
        { 
            get 
            {
                TabPage Tab = pages.SelectedTab;
                return Tab != null ? pages.SelectedTab.Text : ""; 
            }
        }

        public void AfterAppendTab(object sender, PanelItemListEventArgs e)
        {
            TabPage NewTab = null;
            ListViewEx LV = null;
            TabModel Model = (TabModel)sender;
            // создаем новую вкладку
            NewTab = new TabPage(e.Info.TabName);
            logger.Info("Create control {0}", NewTab.ToString());
            pages.TabPages.Add(NewTab);
            // создаем ListView или получаем существующий
            LV = new ListViewEx();
            logger.Info("Create control {0}", LV.ToString());
            // настраиваем свойства и события для нового ListView
            LV.View = e.Info.CurrentView;
            NewTab.Controls.Add(LV);
            // создаем внутренний список для хранения элементов или получаем существующий
            LV.SetObject(e.Info);
            e.Info.Changed +=new EventHandler(ItemList_Changed);
            e.Info.UpdateSubsctiption();
            // восстанавливаем список элементов
            // ...
            // установка фильтра
            MainForm.GetInstance().UpdateFilter(LV, e.Info.FilterText, false);
            // настройка ListView
            ListView_Setup(LV);
        }

        public void AfterRemove(object sender, IndexEventArgs e)
        {
            pages.TabPages.RemoveAt(e.Index);
        }

        public void AfterRename(object sender, PanelItemListEventArgs e)
        {
            pages.SelectedTab.Text = e.Info.TabName;
        }

        public static void ListView_SetupTip(ListView LV)
        {
            MainForm.GetInstance().tipComps.SetToolTip(LV, "!");
            MainForm.GetInstance().tipComps.Active = true;
        }

        public void ListView_Setup(ListViewEx LV)
        {
            logger.Info("Setup control {0}", LV.ToString());
            LV.Columns.Clear();
            LV.Columns.Add("Сетевое имя", 130);
            LV.Columns.Add("Описание", 250);
            LV.ContextMenuStrip = MainForm.GetInstance().popComps;
            LV.Location = new Point(3, 3);
            LV.Dock = System.Windows.Forms.DockStyle.Fill;
            LV.FullRowSelect = true;
            LV.GridLines = true;
            LV.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            LV.HideSelection = false;
            LV.LargeImageList = MainForm.GetInstance().ilLarge;
            LV.ShowGroups = false;
            LV.ShowItemToolTips = true;
            LV.SmallImageList = MainForm.GetInstance().ilSmall;
            LV.View = System.Windows.Forms.View.Details;
            LV.VirtualMode = true;
            LV.ItemActivate += new System.EventHandler(MainForm.GetInstance().lvRecent_ItemActivate);
            LV.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(MainForm.GetInstance().lvComps_RetrieveVirtualItem);
            LV.KeyPress += new System.Windows.Forms.KeyPressEventHandler(MainForm.GetInstance().lvComps_KeyPress);
            LV.KeyDown += new System.Windows.Forms.KeyEventHandler(MainForm.GetInstance().lvComps_KeyDown);
            ListView_SetupTip(LV);
            ListView_Update(LV);
        }

        private void ItemList_Changed(object sender, EventArgs e)
        {
            ListViewEx LV = GetActiveListView();
            PanelItemList ItemList = LV.GetObject();
            if (ItemList == null)
                return;
            int ShowCount, TotalCount;
            if (ItemList.IsFiltered)
            {
                ShowCount = ItemList.FilterCount;
                TotalCount = ItemList.Count;
            }
            else
            {
                ShowCount = ItemList.Count;
                TotalCount = ItemList.Count;
            }
            lock (LV)
            {
                LV.VirtualListSize = ShowCount;
            }
            if (ShowCount != TotalCount)
                MainForm.GetInstance().StatusText = String.Format("Элементов: {0} из {1}", ShowCount, TotalCount);
            else
                MainForm.GetInstance().StatusText = String.Format("Элементов: {0}", ShowCount);
        }


        public void ListView_Update(ListViewEx LV)
        {
            PanelItemList ItemList = LV.GetObject();
            if (ItemList != null)
            {
                LV.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(MainForm.GetInstance().lvComps_RetrieveVirtualItem);
                LV.VirtualMode = true;
                //LV.VirtualListSize = ItemList.FilterCount;
            }
        }

        public static string InputBoxAsk(string caption, string prompt, string defText)
        {
            return MainForm.GetInstance().inputBox.Ask(caption, prompt, defText, false);
        }

        internal ListViewEx GetActiveListView()
        {
            return (ListViewEx)pages.SelectedTab.Controls[0];
        }

        internal SaveFileDialog GetSaveFileDialog()
        {
            return MainForm.GetInstance().dlgSave;
        }
    }
}
