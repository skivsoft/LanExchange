using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;


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
    #region Вспомогательные классы для модели

    public class TabInfoEventArgs : EventArgs
    {
        private TTabInfo info;

        public TabInfoEventArgs(TTabInfo Info)
        {
            this.info = Info;
        }

        public TTabInfo Info { get { return this.info; }}
    }

    public delegate void TabInfoEventHandler(object sender, TabInfoEventArgs e);

    public class IndexEventArgs : EventArgs
    {
        private int index;

        public IndexEventArgs(int Index)
        {
            this.index = Index;
        }

        public int Index { get { return this.index; } }
    }

    public delegate void IndexEventHandler(object sender,  IndexEventArgs e);

    #endregion


    #region Модель (Model)
    // 
    // Модель предоставляет знания: данные и методы работы с этими данными, 
    // реагирует на запросы, изменяя своё состояние. 
    // Не содержит информации, как эти знания можно визуализировать.
    // 

    public class TTabInfo
    {
        public string TabName = "";
        public string FilterText = "";
        public View CurrentView = View.Details;
        public List<string> Items = null;

        public TTabInfo(string name)
        {
            this.TabName = name;
        }
    }
    
    public class TTabModel
    {
        private List<TTabInfo> InfoList = new List<TTabInfo>();

        public event TabInfoEventHandler AfterAppendTab;
        public event IndexEventHandler AfterRemove;
        public event TabInfoEventHandler AfterRename;

        public int Count { get { return this.InfoList.Count; }  }

        public TTabInfo GetItem(int Index)
        {
            return this.InfoList[Index];
        }

        public void DoAfterAppendTab(TTabInfo Info)
        {
            if (AfterAppendTab != null)
               AfterAppendTab(this, new TabInfoEventArgs(Info));
        }

        public void DoAfterRemove(int Index)
        {
            if (AfterRemove != null)
                AfterRemove(this, new IndexEventArgs(Index));
        }

        public void DoAfterRename(TTabInfo Info)
        {
            if (AfterRename != null)
                AfterRename(this, new TabInfoEventArgs(Info));
        }

        public void AddTab(TTabInfo Info)
        {
            InfoList.Add(Info);
            DoAfterAppendTab(Info);
        }

        public void InternalAdd(TTabInfo Info)
        {
            InfoList.Add(Info);
        }

        public void DelTab(int Index)
        {
            InfoList.RemoveAt(Index);
            DoAfterRemove(Index);
        }

        public void RenameTab(int Index, string NewTabName)
        {
            TTabInfo Info = InfoList[Index];
            Info.TabName = NewTabName;
            DoAfterRename(Info);
        }

        public string GetTabName(int i)
        {
            if (i < 0 || i > Count - 1)
                return null;
            else
                return InfoList[i].TabName;
        }

        internal void Clear()
        {
            InfoList.Clear();
        }
    }
    #endregion

    #region Представление (View)

    // Представление отвечает за отображение информации (визуализация). 
    // Часто в качестве представления выступает


    public class TTabView
    {
        TabControl pages;

        public TTabView(TabControl Pages)
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

        public void AfterAppendTab(object sender, TabInfoEventArgs e)
        {
            TabPage NewTab = null;
            ListView LV = null;
            TPanelItemList ItemList = null;
            TTabModel Model = (TTabModel)sender;
            // создаем новую вкладку или получаем существующую
            if (Model.Count <= this.pages.TabCount)
            {
                NewTab = this.pages.TabPages[Model.Count - 1];
                TLogger.Print("Get existing control {0}", NewTab.ToString());
            }
            else
            {
                NewTab = new TabPage(e.Info.TabName);
                NewTab.Padding = this.pages.TabPages[0].Padding;
                TLogger.Print("Create control {0}", NewTab.ToString());
                pages.TabPages.Add(NewTab);
            }
            // создаем ListView или получаем существующий
            bool bNewListView = NewTab.Controls.Count == 0;
            if (!bNewListView)
            {
                LV = (ListView)NewTab.Controls[0];
                TLogger.Print("Get existing control {0}", LV.ToString());
            }
            else
            {
                LV = new ListView();
                TLogger.Print("Create control {0}", LV.ToString());
                // настраиваем свойства и события для нового ListView
                LV.View = e.Info.CurrentView;
                NewTab.Controls.Add(LV);
            }
            // создаем внутренний список для хранения элементов или получаем существующий
            ItemList = TPanelItemList.ListView_GetObject(LV);
            if (ItemList != null)
                TLogger.Print("Get existing object {0}", ItemList.ToString());
            else
                ItemList = TPanelItemList.ListView_CreateObject(LV);
            // восстанавливаем список элементов
            if (e.Info.Items != null)
            {
                TPanelItemList TopList = MainForm.MainFormInstance.CompBrowser.InternalItemList;
                foreach (string ItemName in e.Info.Items)
                {
                    TPanelItem PItem = TopList.Get(ItemName);
                    if (PItem != null)
                        ItemList.Add(PItem);
                    else
                        ItemList.Add(new TComputerItem());
                }
                TLogger.Print("Added items to object {0}, Count: {1}", ItemList.ToString(), ItemList.Count);
            }
            // установка фильтра
            MainForm.MainFormInstance.UpdateFilter(LV, e.Info.FilterText, false);
            // настройка ListView
            if (bNewListView)
                ListView_Setup(LV);
            else
                ListView_Update(LV);
        }

        public void AfterRemove(object sender, IndexEventArgs e)
        {
            pages.TabPages.RemoveAt(e.Index);
        }

        public void AfterRename(object sender, TabInfoEventArgs e)
        {
            pages.SelectedTab.Text = e.Info.TabName;
        }

        public static void ListView_SetupTip(ListView LV)
        {
            MainForm.MainFormInstance.tipComps.SetToolTip(LV, "!");
            MainForm.MainFormInstance.tipComps.Active = true;
        }

        public void ListView_Setup(ListView LV)
        {
            TLogger.Print("Setup control {0}", LV.ToString());
            LV.Columns.Clear();
            LV.Columns.Add("Сетевое имя", 130);
            LV.Columns.Add("Описание", 250);
            LV.ContextMenuStrip = MainForm.MainFormInstance.popComps;
            LV.Location = new Point(3, 3);
            LV.Dock = System.Windows.Forms.DockStyle.Fill;
            LV.FullRowSelect = true;
            LV.GridLines = true;
            LV.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            LV.HideSelection = false;
            LV.LargeImageList = MainForm.MainFormInstance.ilLarge;
            LV.ShowGroups = false;
            LV.ShowItemToolTips = true;
            LV.SmallImageList = MainForm.MainFormInstance.ilSmall;
            LV.View = System.Windows.Forms.View.Details;
            LV.VirtualMode = true;
            LV.ItemActivate += new System.EventHandler(MainForm.MainFormInstance.lvRecent_ItemActivate);
            LV.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(MainForm.MainFormInstance.lvComps_RetrieveVirtualItem);
            LV.KeyPress += new System.Windows.Forms.KeyPressEventHandler(MainForm.MainFormInstance.lvComps_KeyPress);
            LV.KeyDown += new System.Windows.Forms.KeyEventHandler(MainForm.MainFormInstance.lvComps_KeyDown);
            ListView_SetupTip(LV);
            ListView_Update(LV);
        }

        public void ListView_Update(ListView LV)
        {
            TPanelItemList ItemList = TPanelItemList.ListView_GetObject(LV);
            if (ItemList != null)
            {
                LV.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(MainForm.MainFormInstance.lvComps_RetrieveVirtualItem);
                LV.VirtualMode = true;
                LV.VirtualListSize = ItemList.FilterCount;
            }
        }

        public static string InputBoxAsk(string caption, string prompt, string defText)
        {
            return MainForm.MainFormInstance.inputBox.Ask(caption, prompt, defText, false);
        }

        internal ListView GetActiveListView()
        {
            return (ListView)pages.SelectedTab.Controls[0];
        }

        internal SaveFileDialog GetSaveFileDialog()
        {
            return MainForm.MainFormInstance.dlgSave;
        }
    }
    #endregion

    #region Поведение (Controller)

    // контроллеры должны избавляться от логики приложения (бизнес-логики). 
    // Таким образом Контроллер становится «тонким» и выполняет исключительно 
    // функцию связующего звена (glue layer) между отдельными 
    // компонентами системы.   

    public class TTabController
    {
        private TTabModel Model = null;
        private TTabView View = null;

        public TTabController(TabControl Pages)
        {
            Model = new TTabModel();
            View = new TTabView(Pages);
            UpdateModelFromView();
            Model.AfterAppendTab += new TabInfoEventHandler(View.AfterAppendTab);
            Model.AfterRemove += new IndexEventHandler(View.AfterRemove);
            Model.AfterRename += new TabInfoEventHandler(View.AfterRename);
        }

        public TTabModel GetModel()
        {
            return this.Model;
        }

        public void NewTab()
        {
            string NewTabName = TTabView.InputBoxAsk("Новая вкладка", "Введите имя", "");
            if (!String.IsNullOrEmpty(NewTabName))
            {
                Model.AddTab(new TTabInfo(NewTabName));
                StoreSettings();
            }
        }

        public void CloseTab()
        {
            int Index = View.SelectedIndex;
            if (CanModifyTab(Index))
            {
                Model.DelTab(Index);
                StoreSettings();
            }
        }

        public void RenameTab()
        {
            int Index = View.SelectedIndex;
            if (CanModifyTab(Index))
            {
                string NewTabName = TTabView.InputBoxAsk("Переименование", "Введите имя", View.SelectedTabText);
                if (NewTabName != null)
                {
                    Model.RenameTab(Index, NewTabName);
                    StoreSettings();
                }
            }
        }

        public void SaveTab()
        {
            SaveFileDialog dlgSave = View.GetSaveFileDialog();
            if (dlgSave == null)
                return;
            dlgSave.FileName = String.Format("{0}.txt", View.SelectedTabText);
            DialogResult Res = dlgSave.ShowDialog();
            if (Res == DialogResult.OK)
            {
                ListView LV = View.GetActiveListView();
                if (LV == null) return;
                TPanelItemList ItemList = TPanelItemList.ListView_GetObject(LV);
                if (ItemList == null) return;
                // формируем строку для записи в файл
                StringBuilder S = new StringBuilder();
                for (int i = 0; i < ItemList.Count; i++)
                {
                    TPanelItem PItem = ItemList.Get(ItemList.Keys[i]);
                    if (PItem == null) continue;
                    if (S.Length > 0)
                        S.AppendLine();
                    S.Append(PItem.Name);
                    S.Append("\t");
                    S.Append(PItem.Comment);
                }
                // записываем в файл
                using (Stream stream = dlgSave.OpenFile())
                {
                    byte[] data = Encoding.UTF8.GetBytes(S.ToString());
                    stream.Write(data, 0, data.Length);
                    stream.Close();
                }
            }
        }

        public void ListTab()
        {
            MessageBox.Show("mListTab_Click");
        }

        public void AddTabsToMenuItem(ToolStripMenuItem menuitem, EventHandler handler, bool bHideActive)
        {
            for (int i = 0; i < Model.Count; i++)
            {
                if (bHideActive && (!CanModifyTab(i) || (i == View.SelectedIndex)))
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
            TPanelItemList ItemListSender = TPanelItemList.ListView_GetObject(lvSender);
            TPanelItemList ItemListReceiver = TPanelItemList.ListView_GetObject(lvReceiver);

            int NumAdded = 0;
            //int[] Indices = new int[lvSender.SelectedIndices.Count];
            foreach (int Index in lvSender.SelectedIndices)
            {
                TPanelItem PItem = ItemListSender.Get(lvSender.Items[Index].Text);
                if (PItem != null)
                {
                    ItemListReceiver.Add(PItem);
                    //Indices[Index] = ItemListReceiver.Keys.IndexOf(PItem.Name);
                    NumAdded++;
                }
            }
            ItemListReceiver.ApplyFilter();
            View.ListView_Update(lvReceiver);
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
            if (View.SelectedIndex == 0 && 
                MainForm.MainFormInstance.CompBrowser.InternalStack.Count > 0)
                return;
            string NewTabName = TTabView.InputBoxAsk("Новая вкладка", "Введите имя", "");
            if (NewTabName == null)
                return;
            ListView LV = View.GetActiveListView();
            TPanelItemList ItemList = TPanelItemList.ListView_GetObject(LV);
            TTabInfo Info = new TTabInfo(NewTabName);
            Info.Items = ItemList.ListView_GetSelected(LV, false);
            Model.AddTab(Info);
            View.SelectedIndex = Model.Count - 1;
            StoreSettings();
        }

        public void mSendToSelectedTab_Click(object sender, EventArgs e)
        {
            if (View.SelectedIndex == 0 &&
                MainForm.MainFormInstance.CompBrowser.InternalStack.Count > 0)
                return;
            ListView lvSender = View.GetActiveListView();
            int Index = (int)(sender as ToolStripMenuItem).Tag;
            View.SelectedIndex = Index;
            ListView lvReceiver = View.GetActiveListView();
            SendPanelItems(lvSender, lvReceiver);
            StoreSettings();
        }


        /// <summary>
        /// Определяет можно ли редактировать вкладку (удалять и переименовывать).
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        public bool CanModifyTab(int Index)
        {
            return Index != 0;
        }
        /// <summary>
        /// Заполняет список страниц внутри модели данными из представления.
        /// </summary>
        public void UpdateModelFromView()
        {
            Model.Clear();
            foreach (TabPage Tab in View.TabPages)
            {
                if (Tab.Controls.Count == 0) continue;
                TTabInfo Info = new TTabInfo(Tab.Text);
                ListView LV = (ListView)Tab.Controls[0];
                TPanelItemList ItemList = TPanelItemList.ListView_GetObject(LV);
                Info.Items = ItemList.ListView_GetSelected(LV, true);
                Info.FilterText = ItemList.FilterText;
                Info.CurrentView = LV.View;
                Model.InternalAdd(Info);
            }
        }

        public void StoreSettings()
        {
            UpdateModelFromView();
            string name = View.Name;
            TSettings.SetIntValue(String.Format(@"{0}\SelectedIndex", name), View.SelectedIndex);
            TSettings.SetIntValue(String.Format(@"{0}\Count", name), Model.Count);
            for (int i = 0; i < Model.Count; i++)
            {
                TTabInfo Info = Model.GetItem(i);
                string S = String.Format(@"{0}\{1}\", name, i);
                TSettings.SetStrValue(S + "TabName", Info.TabName);
                TSettings.SetStrValue(S + "FilterText", Info.FilterText);
                TSettings.SetIntValue(S + "CurrentView", (int)Info.CurrentView);
                // элементы нулевой закладки не сохраняем, т.к. они формируются после обзора сети
                TSettings.SetListValue(S + "Items", i > 0 ? Info.Items : null);
            }
        }

        public void LoadSettings()
        {
            string name = View.Name;
            Model.Clear();
            int CNT = TSettings.GetIntValue(String.Format(@"{0}\Count", name), 0);
            if (CNT > 0)
            {
                for (int i = 0; i < CNT; i++)
                {
                    string S = String.Format(@"{0}\{1}\", name, i);
                    string tabname = TSettings.GetStrValue(S + "TabName", "");
                    TTabInfo Info = new TTabInfo(tabname);
                    Info.FilterText = TSettings.GetStrValue(S + "FilterText", "");
                    Info.CurrentView = (View)TSettings.GetIntValue(S + "CurrentView", (int)System.Windows.Forms.View.Details);
                    Info.Items = TSettings.GetListValue(S + "Items");
                    Model.AddTab(Info);
                }
            }
            else
            {
                TTabInfo Info = new TTabInfo(Environment.UserDomainName);
                Info.FilterText = "";
                Info.CurrentView = (System.Windows.Forms.View.Details);
                Info.Items = new List<string>();
                Model.AddTab(Info);
            }
            int Index = TSettings.GetIntValue(String.Format(@"{0}\SelectedIndex", name), 0);
            // присваиваем сначала -1, чтобы всегда срабатывал евент PageSelected при установке нужной странице
            View.SelectedIndex = -1;
            View.SelectedIndex = Index;
        }
    }
    #endregion

}
