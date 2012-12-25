using System;
using System.Windows.Forms;
using LanExchange.Model;
using LanExchange.View;


//
// попробуем организовать управление вкладками 
// по схеме MVC (Модель-Представление-Поведение) 
//
// Контроллер представляет собой связующее звено между двумя 
// основными компонентами системы — Моделью (Model) и Представлением (View). 
// Модель ничего «не знает» о Представлении, а Контроллер не содержит 
// в себе какой-либо бизнес-логики.
//
namespace LanExchange.Presenter
{

    // контроллеры должны избавляться от логики приложения (бизнес-логики). 
    // Таким образом Контроллер становится «тонким» и выполняет исключительно 
    // функцию связующего звена (glue layer) между отдельными 
    // компонентами системы.   

    public class TabControlPresenter
    {
        private readonly ITabControlView m_View;
        private readonly TabControlModel Model;

        public TabControlPresenter(ITabControlView pages)
        {
            m_View = pages;
            Model = new TabControlModel(m_View.Name);
            //UpdateModelFromView();
            //Model.AfterAppendTab += View.AfterAppendTab;
            //Model.AfterRemove += View.AfterRemove;
            //Model.AfterRename += View.AfterRename;
        }

        public TabControlModel GetModel()
        {
            return Model;
        }

        public void NewTab()
        {
            //string NewTabName = TabView.InputBoxAsk("Новая вкладка", "Введите имя", "");
            //if (!String.IsNullOrEmpty(NewTabName))
            //{
            //    Model.AddTab(new PanelItemList(NewTabName));
            //    Model.StoreSettings();
            //}
        }

        public void CloseTab()
        {
            int Index = m_View.SelectedIndex;
            if (CanCloseTab(Index))
            {
                Model.DelTab(Index);
                Model.StoreSettings();
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
            for (int i = 0; i < Model.Count; i++)
            {
                if (bHideActive && (!CanCloseTab(i) || (i == m_View.SelectedIndex)))
                    continue;
                ToolStripMenuItem Item = new ToolStripMenuItem
                {
                    Checked = (i == m_View.SelectedIndex),
                    Text = Model.GetTabName(i),
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
            //logger.Info("AfterAppendTab()");
            //TabControlModel Model = (TabControlModel)sender;
            //// init controls
            //TabPage NewTab = new TabPage();
            //ListView LV = new ListView();
            //// suspend layouts
            ////Instance.SuspendLayout();
            ////m_Pages.SuspendLayout();
            ////NewTab.SuspendLayout();
            ////
            //// m_Pages
            ////
            //m_Pages.Controls.Add(NewTab);
            ////
            //// LV
            ////
            //logger.Info("Setup control {0}", LV.ToString());
            //MethodInfo mi = typeof(Control).GetMethod("SetStyle", BindingFlags.Instance | BindingFlags.NonPublic);
            //mi.Invoke(LV, new object[] { ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true });
            ////LV.Dock = DockStyle.Fill;
            ////LV.Location = new Point(3, 3);
            ////LV.Size = new Size(NewTab.Size.Width - 6, NewTab.Size.Height - 6);
            ////LV.View = e.Info.CurrentView;
            ////LV.Columns.Add("Сетевое имя", 130);
            ////LV.Columns.Add("Описание", 250);
            ////LV.ContextMenuStrip = MainForm.Instance.popComps;
            ////LV.FullRowSelect = true;
            ////LV.GridLines = true;
            ////LV.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            ////LV.HideSelection = false;
            ////LV.LargeImageList = MainForm.Instance.ilLarge;
            ////LV.ShowGroups = false;
            ////LV.ShowItemToolTips = true;
            ////LV.SmallImageList = MainForm.Instance.ilSmall;
            ////LV.VirtualMode = true;

            //// events
            ////LV.ItemActivate += MainForm.Instance.lvRecent_ItemActivate;
            ////LV.RetrieveVirtualItem += MainForm.Instance.lvComps_RetrieveVirtualItem;
            ////LV.KeyPress += MainForm.Instance.lvComps_KeyPress;
            ////LV.KeyDown += MainForm.Instance.lvComps_KeyDown;
            ////LV.ItemSelectionChanged += MainForm.Instance.lvComps_ItemSelectionChanged;
            ////LV.SelectedIndexChanged += MainForm.Instance.lvComps_SelectedIndexChanged;
            ////LV.CacheVirtualItems += MainForm.Instance.lvComps_CacheVirtualItems;
            ////LV.RetrieveVirtualItem += MainForm.Instance.lvComps_RetrieveVirtualItem;
            //e.Info.AttachObjectTo(LV);
            ////e.Info.Changed += MainForm.Instance.Items_Changed;
            //e.Info.UpdateSubsctiption();
            ////
            //// NewTab
            ////
            //NewTab.Controls.Add(LV);
            //NewTab.Text = e.Info.TabName;
            //NewTab.UseVisualStyleBackColor = true;
            //// resume layouts
            //MainForm.Instance.tipComps.SetToolTip(LV, " ");
            ////NewTab.ResumeLayout(false);
            ////m_Pages.ResumeLayout(false);
            ////Instance.ResumeLayout(false);
            ////Instance.PerformLayout();
            //// update filter
            ////MainForm.GetInstance().UpdateFilter(LV, e.Info.FilterText, false);
            //Model.SelectedIndex = Model.Count - 1;
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
