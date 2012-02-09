using System;
using System.Collections.Generic;
using SkivSoft.LanExchange.SDK;
using System.Collections;


//
// попробуем организовать управление вкладками 
// по схеме MVC (Модель-Представление-Поведение) 
//
// Контроллер представляет собой связующее звено между двумя 
// основными компонентами системы — Моделью (Model) и Представлением (View). 
// Модель ничего «не знает» о Представлении, а Контроллер не содержит 
// в себе какой-либо бизнес-логики.
//
namespace SkivSoft.LanExchange.Core
{
    #region Модель (Model)
    // 
    // Модель предоставляет знания: данные и методы работы с этими данными, 
    // реагирует на запросы, изменяя своё состояние. 
    // Не содержит информации, как эти знания можно визуализировать.
    // 
 
    public class TLanEXTabModel : ILanEXTabModel
    {
        private List<TabInfo> infolist = new List<TabInfo>();

        public event TabInfoEventHandler AfterAppendTab;
        public event IndexEventHandler AfterRemove;
        public event TabInfoEventHandler AfterRename;

        public IList<TabInfo> InfoList { get { return infolist; } }

        public void DoAfterAppendTab(TabInfo Info)
        {
            if (AfterAppendTab != null)
               AfterAppendTab(this, new TabInfoEventArgs(Info));
        }

        public void DoAfterRemove(int Index)
        {
            if (AfterRemove != null)
                AfterRemove(this, new IndexEventArgs(Index));
        }

        public void DoAfterRename(TabInfo Info)
        {
            if (AfterRename != null)
                AfterRename(this, new TabInfoEventArgs(Info));
        }

        public void AddTab(TabInfo Info)
        {
            infolist.Add(Info);
            DoAfterAppendTab(Info);
        }

        public void InternalAdd(TabInfo Info)
        {
            infolist.Add(Info);
        }

        public void DelTab(int Index)
        {
            infolist.RemoveAt(Index);
            DoAfterRemove(Index);
        }

        public void RenameTab(int Index, string NewTabName)
        {
            TabInfo Info = infolist[Index];
            Info.TabName = NewTabName;
            DoAfterRename(Info);
        }

        public string GetTabName(int i)
        {
            if (i < 0 || i > infolist.Count - 1)
                return null;
            else
                return infolist[i].TabName;
        }

        public void Clear()
        {
            infolist.Clear();
        }
    }
    #endregion

    #region Представление (View)

    // Представление отвечает за отображение информации (визуализация). 
    // Часто в качестве представления выступает


    public class TLanEXTabView
    {
        public ILanEXTabControl pages;

        public TLanEXTabView(ILanEXTabControl Pages)
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
            TLanEXTabModel Model = (TLanEXTabModel)sender;
            // создаем новую вкладку или получаем существующую
            if (Model.InfoList.Count <= this.pages.TabCount)
            {
                NewTab = this.pages.GetPage(Model.InfoList.Count - 1);
                TMainApp.App.LogPrint("Get existing control {0}", NewTab.ToString());
            }
            else
            {
                NewTab = TMainApp.App.CreateControl(typeof(ILanEXTabPage)) as ILanEXTabPage;
                NewTab.Text = e.Info.TabName;
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
                // настраиваем свойства и события для нового ListView
                LV.View = e.Info.CurrentView;
                NewTab.ListView = LV;
            }
            // создаем внутренний список для хранения элементов или получаем существующий
            if (LV.ItemList != null)
                TMainApp.App.LogPrint("Get existing object {0}", LV.ItemList.ToString());
            else
            {
                LV.ItemList = TMainApp.App.CreateComponent(typeof(ILanEXItemList)) as ILanEXItemList;
                //TMainApp.App.LogPrint("Create object {0}", LV.ItemList);
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

    public class TLanEXTabController : ILanEXTabController
    {
        private TLanEXTabModel model = null;
        private TLanEXTabView view = null;

        public TLanEXTabController(ILanEXTabControl Pages)
        {
            model = new TLanEXTabModel();
            view = new TLanEXTabView(Pages);
            UpdateModelFromView();
            model.AfterAppendTab += new TabInfoEventHandler(view.AfterAppendTab);
            model.AfterRemove += new IndexEventHandler(view.AfterRemove);
            model.AfterRename += new TabInfoEventHandler(view.AfterRename);
        }

        public ILanEXTabModel Model { get { return this.model; }}

        public void NewTab()
        {
            string NewTabName = TLanEXTabView.InputBoxAsk("Новая вкладка", "Введите имя", "");
            if (!String.IsNullOrEmpty(NewTabName))
            {
                model.AddTab(new TabInfo(NewTabName));
                StoreSettings();
            }
        }

        public void CloseTab()
        {
            int Index = view.pages.SelectedIndex;
            if (CanModifyTab(Index))
            {
                model.DelTab(Index);
                StoreSettings();
            }
        }

        public void RenameTab()
        {
            int Index = view.pages.SelectedIndex;
            if (CanModifyTab(Index))
            {
                string NewTabName = TLanEXTabView.InputBoxAsk("Переименование", "Введите имя", view.SelectedTabText);
                if (NewTabName != null)
                {
                    model.RenameTab(Index, NewTabName);
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
            /*
            for (int i = 0; i < model.InfoList.Count; i++)
            {
                if (bHideActive && (!CanModifyTab(i) || (i == view.pages.SelectedIndex)))
                    continue;
                ILanEXMenuItem Item = TMainApp.App.CreateControl(typeof(ILanEXMenuItem)) as ILanEXMenuItem;
                Item.Checked = (i == view.pages.SelectedIndex);
                Item.Text = model.GetTabName(i);
                Item.Click += handler;
                Item.Tag = i;
                menuitem.DropDownItems.Add(Item);
            }
             */
        }

        public void mSelectTab_Click(object sender, EventArgs e)
        {
            int Index = (int)(sender as ILanEXMenuItem).Tag;
            if (view.pages.SelectedIndex != Index)
                view.pages.SelectedIndex = Index;
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
                ILanEXItem PItem = ItemListSender.Get(lvSender.GetItem(Index).Text);
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
            return true;
        }
        /// <summary>
        /// Заполняет список страниц внутри модели данными из представления.
        /// </summary>
        public void UpdateModelFromView()
        {
            model.Clear();
            for (int i = 0; i < view.pages.TabCount; i++)
            {
                ILanEXTabPage Tab = view.pages.GetPage(i);
                if (!Tab.IsListViewPresent) continue;
                TabInfo Info = new TabInfo(Tab.Text);
                ILanEXListView LV = Tab.ListView;
                Info.Items = LV.GetSelected(true);
                if (LV.ItemList != null)
                    Info.FilterText = LV.ItemList.FilterText;
                Info.CurrentView = LV.View;
                model.InternalAdd(Info);
            }
        }

        public void StoreSettings()
        {
            UpdateModelFromView();
            string name = view.pages.Name;
            TSettings.SetIntValue(String.Format(@"{0}\SelectedIndex", name), view.pages.SelectedIndex);
            TSettings.SetIntValue(String.Format(@"{0}\Count", name), model.InfoList.Count);
            for (int i = 0; i < model.InfoList.Count; i++)
            {
                TabInfo Info = (TabInfo)model.InfoList[i];
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
            string name = view.pages.Name;
            model.Clear();
            int CNT = TSettings.GetIntValue(String.Format(@"{0}\Count", name), 0);
            if (CNT > 0)
            {
                for (int i = 0; i < CNT; i++)
                {
                    string S = String.Format(@"{0}\{1}\", name, i);
                    string tabname = TSettings.GetStrValue(S + "TabName", "");
                    TabInfo Info = new TabInfo(tabname);
                    Info.FilterText = TSettings.GetStrValue(S + "FilterText", "");
                    Info.CurrentView = TSettings.GetIntValue(S + "CurrentView", (int)System.Windows.Forms.View.Details);
                    Info.Items = TSettings.GetListValue(S + "Items");
                    model.AddTab(Info);
                }
            }
            else
            {
                TabInfo Info = new TabInfo(Environment.UserDomainName);
                Info.FilterText = "";
                Info.CurrentView = 2;
                Info.Items = new List<string>();
                model.AddTab(Info);
            }
            int Index = TSettings.GetIntValue(String.Format(@"{0}\SelectedIndex", name), 0);
            // присваиваем сначала -1, чтобы всегда срабатывал евент PageSelected при установке нужной странице
            view.pages.SelectedIndex = -1;
            view.pages.SelectedIndex = Index;
        }
    }
    #endregion

}
