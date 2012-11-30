using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using LanExchange.Forms;
using LanExchange.Network;


//
// попробуем организовать управление вкладками 
// по схеме MVC (Модель-Представление-Поведение) 
//
// Контроллер представляет собой связующее звено между двумя 
// основными компонентами системы — Моделью (Model) и Представлением (View). 
// Модель ничего «не знает» о Представлении, а Контроллер не содержит 
// в себе какой-либо бизнес-логики.
//
namespace LanExchange
{

    // контроллеры должны избавляться от логики приложения (бизнес-логики). 
    // Таким образом Контроллер становится «тонким» и выполняет исключительно 
    // функцию связующего звена (glue layer) между отдельными 
    // компонентами системы.   

    public class TabController
    {
        private TabModel Model = null;
        private TabView View = null;

        public TabController(TabControl Pages)
        {
            Model = new TabModel(Pages.Name);
            View = new TabView(Pages);
            //UpdateModelFromView();
            Model.AfterAppendTab += new PanelItemListEventHandler(View.AfterAppendTab);
            Model.AfterRemove += new IndexEventHandler(View.AfterRemove);
            Model.AfterRename += new PanelItemListEventHandler(View.AfterRename);
        }

        public TabModel GetModel()
        {
            return this.Model;
        }

        public void NewTab()
        {
            string NewTabName = TabView.InputBoxAsk("Новая вкладка", "Введите имя", "");
            if (!String.IsNullOrEmpty(NewTabName))
            {
                Model.AddTab(new PanelItemList(NewTabName));
                Model.StoreSettings();
            }
        }

        public void CloseTab()
        {
            int Index = View.SelectedIndex;
            if (CanCloseTab(Index))
            {
                Model.DelTab(Index);
                Model.StoreSettings();
            }
        }

        public void RenameTab()
        {
            int Index = View.SelectedIndex;
            if (CanCloseTab(Index))
            {
                string NewTabName = TabView.InputBoxAsk("Переименование", "Введите имя", View.SelectedTabText);
                if (NewTabName != null)
                {
                    Model.RenameTab(Index, NewTabName);
                    Model.StoreSettings();
                }
            }
        }

        public void AddTabsToMenuItem(ToolStripMenuItem menuitem, EventHandler handler, bool bHideActive)
        {
            for (int i = 0; i < Model.Count; i++)
            {
                if (bHideActive && (!CanCloseTab(i) || (i == View.SelectedIndex)))
                    continue;
                ToolStripMenuItem Item = new ToolStripMenuItem();
                Item.Checked = (i == View.SelectedIndex);
                Item.Text = Model.GetTabName(i);
                Item.Click += handler;
                Item.Tag = i;
                menuitem.DropDownItems.Add(Item);
            }
        }

        public void mSelectTab_Click(object sender, EventArgs e)
        {
            int Index = (int)(sender as ToolStripMenuItem).Tag;
            if (View.SelectedIndex != Index)
                View.SelectedIndex = Index;
        }

        public void SendPanelItems(ListView lvSender, ListView lvReceiver)
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
            if (View.SelectedIndex == 0/* && 
                MainForm.GetInstance().CompBrowser.InternalStack.Count > 0*/)
                return;
            string NewTabName = TabView.InputBoxAsk("Новая вкладка", "Введите имя", "");
            if (NewTabName == null)
                return;
            ListView LV = View.GetActiveListView();
            PanelItemList ItemList = PanelItemList.GetObject(LV);
            PanelItemList Info = new PanelItemList(NewTabName);
            //Info.Items = ItemList.ListView_GetSelected(LV, false);
            Model.AddTab(Info);
            View.SelectedIndex = Model.Count - 1;
            Model.StoreSettings();
        }

        public void mSendToSelectedTab_Click(object sender, EventArgs e)
        {
            if (View.SelectedIndex == 0/* &&
                MainForm.GetInstance().CompBrowser.InternalStack.Count > 0*/)
                return;
            ListView lvSender = View.GetActiveListView();
            int Index = (int)(sender as ToolStripMenuItem).Tag;
            View.SelectedIndex = Index;
            ListView lvReceiver = View.GetActiveListView();
            SendPanelItems(lvSender, lvReceiver);
            Model.StoreSettings();
        }

        public bool CanCloseTab(int Index)
        {
            return View.TabPages.Count > 1;
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

        }
}
