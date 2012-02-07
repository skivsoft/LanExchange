using System;
using System.Collections.Generic;
using SkivSoft.LanExchange.SDK;


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
        public int CurrentView = 2;
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
        public ILanEXTabControl pages;

        public TTabView(ILanEXTabControl Pages)
        {
            this.pages = Pages;
        }

        public string SelectedTabText 
        { 
            get 
            {
                ILanEXTabPage Tab = pages.SelectedTab;
                return Tab != null ? pages.SelectedTab.Text : ""; 
            }
        }

        public void AfterAppendTab(object sender, TabInfoEventArgs e)
        {
            ILanEXTabPage NewTab = null;
            ILanEXListView LV = null;
            TTabModel Model = (TTabModel)sender;
            // создаем новую вкладку или получаем существующую
            if (Model.Count <= this.pages.TabCount)
            {
                NewTab = this.pages.GetPage(Model.Count - 1);
                TMainApp.App.LogPrint("Get existing control {0}", NewTab.ToString());
            }
            else
            {
                NewTab = TMainApp.App.CreateControl(typeof(ILanEXTabPage)) as ILanEXTabPage;
                NewTab.Name = e.Info.TabName;
                TMainApp.App.LogPrint("Create control {0}", NewTab.ToString());
                pages.Add(NewTab);
            }
            // создаем ListView или получаем существующий
            bool bNewListView = !NewTab.IsListViewPresent;
            if (!bNewListView)
            {
                LV = NewTab.ListView;
                TMainApp.App.LogPrint("Get existing control {0}", LV.ToString());
            }
            else
            {
                LV = TMainApp.App.CreateControl(typeof(ILanEXListView)) as ILanEXListView;
                TMainApp.App.LogPrint("Create control {0}", LV.ToString());
                // настраиваем свойства и события для нового ListView
                LV.View = e.Info.CurrentView;
                NewTab.ListView = LV;
            }
            // создаем внутренний список для хранения элементов или получаем существующий
            if (LV.ItemList != null)
                TMainApp.App.LogPrint("Get existing object {0}", LV.ItemList.ToString());
            else
            {
                LV.ItemList = new TPanelItemList();
                TMainApp.App.LogPrint("Create object {0}", LV.ItemList);
            }
            // восстанавливаем список элементов
            /*
            if (e.Info.Items != null)
            {
                TPanelItemList TopList = MainForm.MainFormInstance.CompBrowser.InternalItemList;
                foreach (string ItemName in e.Info.Items)
                {
                    TPanelItem PItem = TopList.Get(ItemName);
                    if (PItem != null)
                        ItemList.Add(PItem);
                    else
                    {
                        TComputerItem Comp = new TComputerItem();
                        Comp.Name = ItemName;
                        ItemList.Add(Comp);
                    }
                }
                TMainApp.App.LogPrint("Added items to object {0}, Count: {1}", ItemList.ToString(), ItemList.Count);
            }
            // установка фильтра
            MainForm.MainFormInstance.UpdateFilter(LV, e.Info.FilterText, false);
             */
            // настройка ListView
            if (bNewListView)
                TMainApp.App.ListView_Setup(LV);
            else
                TMainApp.App.ListView_Update(LV);
        }

        public void AfterRemove(object sender, IndexEventArgs e)
        {
            pages.RemoveAt(e.Index);
        }

        public void AfterRename(object sender, TabInfoEventArgs e)
        {
            pages.SelectedTab.Text = e.Info.TabName;
        }

        public static string InputBoxAsk(string caption, string prompt, string defText)
        {
            return TMainApp.App.InputBoxAsk(caption, prompt, defText);
        }

        internal ILanEXListView GetActiveListView()
        {
            return pages.SelectedTab.ListView;
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

        public TTabController(ILanEXTabControl Pages)
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
            int Index = View.pages.SelectedIndex;
            if (CanModifyTab(Index))
            {
                Model.DelTab(Index);
                StoreSettings();
            }
        }

        public void RenameTab()
        {
            int Index = View.pages.SelectedIndex;
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
            /*
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
             */
        }

        public void ListTab()
        {
        }

        public void AddTabsToMenuItem(ILanEXMenuItem menuitem, EventHandler handler, bool bHideActive)
        {
            for (int i = 0; i < Model.Count; i++)
            {
                if (bHideActive && (!CanModifyTab(i) || (i == View.pages.SelectedIndex)))
                    continue;
                ILanEXMenuItem Item = TMainApp.App.CreateControl(typeof(ILanEXMenuItem)) as ILanEXMenuItem;
                Item.Checked = (i == View.pages.SelectedIndex);
                Item.Text = Model.GetTabName(i);
                Item.Click += handler;
                Item.Tag = i;
                menuitem.DropDownItems.Add(Item);
            }
        }

        public void mSelectTab_Click(object sender, EventArgs e)
        {
            int Index = (int)(sender as ILanEXMenuItem).Tag;
            if (View.pages.SelectedIndex != Index)
                View.pages.SelectedIndex = Index;
        }

        public void SendPanelItems(ILanEXListView lvSender, ILanEXListView lvReceiver)
        {
            if (lvSender == null || lvReceiver == null)
                return;
            ILanEXItemList ItemListSender = lvSender.ItemList;
            ILanEXItemList ItemListReceiver = lvReceiver.ItemList;

            int NumAdded = 0;
            //int[] Indices = new int[lvSender.SelectedIndices.Count];
            foreach (int Index in lvSender.SelectedIndices)
            {
                IPanelItem PItem = ItemListSender.Get(lvSender.GetItem(Index).Text);
                if (PItem != null)
                {
                    ItemListReceiver.Add(PItem);
                    //Indices[Index] = ItemListReceiver.Keys.IndexOf(PItem.Name);
                    NumAdded++;
                }
            }
            ItemListReceiver.ApplyFilter();
            TMainApp.App.ListView_Update(lvReceiver);
            lvReceiver.Focus();
            // выделяем добавленные итемы
            if (lvReceiver.ItemsCount > 0)
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
            /*
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
             */
        }

        public void mSendToSelectedTab_Click(object sender, EventArgs e)
        {
            /*
            if (View.SelectedIndex == 0 &&
                MainForm.MainFormInstance.CompBrowser.InternalStack.Count > 0)
                return;
            ListView lvSender = View.GetActiveListView();
            int Index = (int)(sender as ToolStripMenuItem).Tag;
            View.SelectedIndex = Index;
            ListView lvReceiver = View.GetActiveListView();
            SendPanelItems(lvSender, lvReceiver);
            StoreSettings();
             */
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
            for (int i = 0; i < View.pages.TabCount; i++)
            {
                ILanEXTabPage Tab = View.pages.GetPage(i);
                if (!Tab.IsListViewPresent) continue;
                TTabInfo Info = new TTabInfo(Tab.Text);
                ILanEXListView LV = Tab.ListView;
                Info.Items = LV.GetSelected(true);
                Info.FilterText = LV.ItemList.FilterText;
                Info.CurrentView = LV.View;
                Model.InternalAdd(Info);
            }
        }

        public void StoreSettings()
        {
            UpdateModelFromView();
            string name = View.pages.Name;
            TSettings.SetIntValue(String.Format(@"{0}\SelectedIndex", name), View.pages.SelectedIndex);
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
            string name = View.pages.Name;
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
                    Info.CurrentView = TSettings.GetIntValue(S + "CurrentView", (int)System.Windows.Forms.View.Details);
                    Info.Items = TSettings.GetListValue(S + "Items");
                    Model.AddTab(Info);
                }
            }
            else
            {
                TTabInfo Info = new TTabInfo(Environment.UserDomainName);
                Info.FilterText = "";
                Info.CurrentView = 2;
                Info.Items = new List<string>();
                Model.AddTab(Info);
            }
            int Index = TSettings.GetIntValue(String.Format(@"{0}\SelectedIndex", name), 0);
            // присваиваем сначала -1, чтобы всегда срабатывал евент PageSelected при установке нужной странице
            View.pages.SelectedIndex = -1;
            View.pages.SelectedIndex = Index;
        }
    }
    #endregion

}
