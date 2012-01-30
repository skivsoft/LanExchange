#define CHECK_ADMIN
#define NOUSE_PING
#define NOUSE_FLASH

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using OSTools;

namespace LanExchange
{
    public partial class MainForm : Form
    {
        #region Константы и переменные
        // индексы страниц
        public const int pageNetwork = 0;

        // тексты сообщений собственного протокола LANEX
        public const string MSG_LANEX_LOGIN    = "LANEX:LOGIN";
        public const string MSG_LANEX_LOGINOK  = "LANEX:LOGINOK";
        public const string MSG_LANEX_LOGOUT   = "LANEX:LOGOUT";
        public const string MSG_LANEX_SHUTDOWN = "LANEX:SHUTDOWN";
        public const string MSG_LANEX_CHAT     = "LANEX:CHAT";

        public static MainForm MainFormInstance = null;
        private static AboutForm AboutFormInstance = null;
        private static ParamsForm ParamsFormInstance = null;

        public bool bFirstStart = true;
        public TNetworkBrowser       CompBrowser = null;
        TTabController        TabController = null;
        FormWindowState       LastWindowState = FormWindowState.Normal;
        string[] FlashMovies;
        int FlashIndex = -1;

        public delegate void ProcRefresh(object obj);
        ProcRefresh myTrayChat;
        ProcRefresh myPagesRefresh;
        ProcRefresh myCompsRefresh;
        #endregion

        #region Инициализация и загрузка формы

        public MainForm()
        {
            TLogger.Print("MainForm init");
            InitializeComponent();
            //SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            MainFormInstance = this;
            lvCompsHandle = lvComps.Handle;
            CompBrowser = new TNetworkBrowser(lvComps);
            TPanelItemList.ListView_SetObject(lvComps, CompBrowser.InternalItemList);
            // содаем контроллер для управления вкладками
            Pages.TabPages[0].Text = Environment.UserDomainName;
            TabController = new TTabController(Pages);
            mSendToNewTab.Click += new System.EventHandler(TabController.mSendToNewTab_Click);

            // делегаты для обновления из потоков
            myCompsRefresh = new ProcRefresh(lvCompsRefreshMethod);
            myTrayChat = new ProcRefresh(TrayChatMethod);
            myPagesRefresh = new ProcRefresh(PagesRefreshMethod);
            // запуск в свернутом режиме
            /*
            if (TSettings.IsRunMinimized)
            {
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
            }
            */
        }
      
        private void SetupForm()
        {
            // размещаем форму внизу справа
            Rectangle Rect = new Rectangle();
            Rect.Size = new Size(450, Screen.PrimaryScreen.WorkingArea.Height);
            Rect.Location = new Point(Screen.PrimaryScreen.WorkingArea.Left + (Screen.PrimaryScreen.WorkingArea.Width - Rect.Width),
                                      Screen.PrimaryScreen.WorkingArea.Top + (Screen.PrimaryScreen.WorkingArea.Height - Rect.Height));
            this.SetBounds(Rect.X, Rect.Y, Rect.Width, Rect.Height);
            // выводим имя компьютера
            lCompName.Text = Environment.MachineName;
            // выводим имя пользователя
            System.Security.Principal.WindowsIdentity user = System.Security.Principal.WindowsIdentity.GetCurrent();
            string[] A = user.Name.Split('\\');
            lUserName.Text = A[1];
            // режим отображения: Компьютеры
            CompBrowser.ViewType = TNetworkBrowser.LVType.COMPUTERS;
            ActiveControl = lvComps;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TLogger.Print("MainForm load");
            SetupForm();
            // запуск определения прав администратора
            AdminMode = false;
            #if CHECK_ADMIN
            if (!DoCheckAdmin.IsBusy)
                DoCheckAdmin.RunWorkerAsync();
            #endif
            // запуск обновления компов
            BrowseTimer.Interval = TSettings.RefreshTimeInSec * 1000;
            #if DEBUG
            //BrowseTimer.Interval = 5 * 1000;
            #endif
            BrowseTimer_Tick(this, new EventArgs());
            // всплывающие подсказки
            TTabView.ListView_SetupTip(lvComps);
        }

        private void BrowseTimer_Tick(object sender, EventArgs e)
        {
            TLogger.Print("Timer BrowseTimer tick");
            if (!DoBrowse.IsBusy && CompBrowser.InternalStack.Count == 0)
            {
                BrowseTimer.Enabled = false;
                // запуск процесса обновления в асинхронном режиме
                DoBrowse.RunWorkerAsync();
            }
        }
        #endregion


        #region Методы обновления, вызываемые из асинхронных потоков
        void PagesRefreshMethod(object sender)
        {
            TabPage Tab = Pages.SelectedTab;
            if (Tab == null) return;
            ListView LV = (ListView)Tab.Controls[0];
            LV.Invoke(myCompsRefresh, LV);
        }

        void lvCompsRefreshMethod(object sender)
        {
            ListView LV = (ListView)sender;
            TPanelItemList ItemList = TPanelItemList.ListView_GetObject(LV);
            if (LV.Handle != IntPtr.Zero)
            {
                int Index = ItemList.Keys.IndexOf(RefreshCompName);
                if (Index != -1)
                    LV.RedrawItems(Index, Index, false);
            }
        }

        void TrayChatMethod(object sender)
        {
            TrayIcon.ShowBalloonTip(10000, "Сообщение от " + RefreshCompName, ChatMessage, ToolTipIcon.Info);
        }

        #endregion



        #region Сворачивание формы в трей

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            TLogger.Print("MainForm is closing with reason {0}", e.CloseReason.ToString());
            if (e.CloseReason == CloseReason.None || e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                IsFormVisible = false;
            }
            else
            {
                TabController.StoreSettings();
                if (DoPing.IsBusy)
                    DoPing.CancelAsync();
                // останавливаем прием UDP сообщений
                StopReceive();
                // сообщаем о выключении компа другим клиентам
                if (e.CloseReason == CloseReason.WindowsShutDown)
                    LANEX_SEND(null, MSG_LANEX_SHUTDOWN);
                else
                    // сообщаем о выходе из программы другим клиентам
                    LANEX_SEND(null, MSG_LANEX_LOGOUT);
            }
            if (e.Cancel)
                TLogger.Print("Closing is canceled");
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
            {
                if (LastWindowState != this.WindowState)
                    TLogger.Print("WindowState is {0}", this.WindowState.ToString());
                LastWindowState = this.WindowState;
            }
            else
                TLogger.Print("WindowState is {0}", this.WindowState.ToString());
        }

        public bool bReActivate = false;


        public bool IsFormVisible
        {
            get { return this.WindowState != FormWindowState.Minimized && this.Visible; }
            set
            {
                bool bMinimized = this.WindowState == FormWindowState.Minimized;
                if (bMinimized)
                {
                    this.ShowInTaskbar = true;
                    this.WindowState = LastWindowState;
                }
                else
                {
                    if (bReActivate)
                    {
                        bReActivate = false;
                        this.ShowInTaskbar = true;
                        this.WindowState = FormWindowState.Minimized;
                        this.WindowState = LastWindowState;
                    }
                }
                this.Visible = value;
                if (this.Visible)
                {
                    this.Activate();
                    //this.ActiveControl = lvComps;
                    //lvComps.Focus();
                }
            }
        }

        private void popTray_Opening(object sender, CancelEventArgs e)
        {
            mOpen.Text = this.IsFormVisible ? "Скрыть" : "Открыть";
        }

        private void mOpen_Click(object sender, EventArgs e)
        {
            this.IsFormVisible = !this.IsFormVisible;
        }

        private void TrayIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                this.IsFormVisible = !this.IsFormVisible;
        }
        #endregion

        public void ShowProperties(object obj)
        {
#if DEBUG
            Form F = new Form();
            F.Text = obj.ToString();
            PropertyGrid Grid = new PropertyGrid();
            Grid.Dock = DockStyle.Fill;
            Grid.SelectedObject = obj;
            F.Controls.Add(Grid);
            F.Show();
#endif
        }
        
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (tsBottom.Visible)
                    eFilter.Text = "";
                else
                    IsFormVisible = false;
                e.Handled = true;
            }
            if (e.Control && e.KeyCode == Keys.Tab)
            {
                if (e.Shift)
                    IncFlashMovieIndex(-1);
                else
                    IncFlashMovieIndex(+1);
                UpdateFlashMovie();
                e.Handled = true;
            }
            if (e.Control && e.KeyCode == Keys.Home)
            {
                FlashIndex = -1;
                UpdateFlashMovie();
                e.Handled = true;
            }
#if DEBUG
            if (e.Control && e.Alt && e.KeyCode == Keys.C)
            {
                //ListView LV = GetActiveListView();
                ShowProperties(Pages.SelectedTab);
                e.Handled = true;
            }
#endif
        }

        #region Фоновые действия: сканирование компов и пинги
        private void DoBrowse_DoWork(object sender, DoWorkEventArgs e)
        {
            TLogger.Print("Thread for browse network start");
            e.Result = TPanelItemList.GetServerList();
            e.Cancel = DoPing.CancellationPending;
            if (e.Cancel)
                TLogger.Print("Thread for browse network canceled");
            else
                TLogger.Print("Thread for browse network finish");
        }

        private List<string> CompareDataSources(List<TPanelItem> OldDT, List<TPanelItem> NewDT)
        {
            List<string> Result = new List<string>();
            if (OldDT == null || NewDT == null) 
                return Result;
            Dictionary<string, int> OldHash = new Dictionary<string, int>();
            int value = 0;

            foreach (TPanelItem Comp in OldDT)
                OldHash.Add(Comp.Name, 0);
            foreach (TPanelItem Comp in NewDT)
                if (!OldHash.TryGetValue(Comp.Name, out value))
                    Result.Add(Comp.Name);
            return Result;
        }

        private void DoBrowse_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            List<TPanelItem> ScannedComps = (List<TPanelItem>)e.Result;
            
            if (!e.Cancelled)
            {
                List<string> AddedNew = CompareDataSources(CompBrowser.CurrentDataTable, ScannedComps);
                List<string> RemovedNew = CompareDataSources(ScannedComps, CompBrowser.CurrentDataTable);
                // если список компов не изменился, ничего не обновляем
                #if (!DEBUG)
                if (!(CompBrowser.CurrentDataTable != null && (AddedNew.Count == 0 && RemovedNew.Count == 0)))
                #endif
                {
                    if (DoPing.IsBusy)
                        DoPing.CancelAsync();
                    // обновление грида
                    if (CompBrowser.InternalItemList.Count > 0)
                        foreach (TPanelItem Comp in ScannedComps)
                        {
                            TPanelItem OldComp = CompBrowser.InternalItemList.Get(Comp.Name);
                            Comp.CopyExtraFrom(OldComp);
                        }
                    CompBrowser.CurrentDataTable = ScannedComps;
                    // запускаем процесс пинга
                    #if USE_PING
                    if (!DoPing.IsBusy)
                        DoPing.RunWorkerAsync(ScannedComps);
                    #endif
                }
                if (bFirstStart)
                {
                    bFirstStart = false;
                    // загрузка списка недавно использованных компов
                    TabController.LoadSettings();
                    // выбираем текущий компьютер, чтобы пользователь видел описание
                    ListView LV = GetActiveListView();
                    TPanelItemList ItemList = TPanelItemList.ListView_GetObject(LV);
                    if (ItemList != null)
                        ItemList.ListView_SelectComputer(LV, Environment.MachineName);
                    // запускаем поток на ожидание UDP сообщений
                    StartReceive();
                    // сообщаем другим клиентам о запуске
                    LANEX_SEND(null, MSG_LANEX_LOGIN);
                }
            }
            // запускаем таймер для следующего обновления
            BrowseTimer.Enabled = true;
        }

        private void DoPing_DoWork(object sender, DoWorkEventArgs e)
        {
#if USE_PING
            List<TPanelItem> ScannedComps = (List<TPanelItem>)e.Argument;
            if (ScannedComps == null || ScannedComps.Count == 0)
                return;
            if (!(ScannedComps[0] is TComputerItem))
                return;
            TLogger.Print("Thread for ping comps start");
            // пингуем найденные компы
            foreach (TPanelItem Comp in ScannedComps)
            {
                if (DoPing.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                bool bNewPingable = PingThread.FastPing(Comp.Name);
                if ((Comp as TComputerItem).IsPingable != bNewPingable)
                {
                        (Comp as TComputerItem).IsPingable = bNewPingable;
                        RefreshCompName = Comp.Name;
                        if(lvComps != null)
                            lvComps.Invoke(myCompsRefresh);
                }
            }
            e.Cancel = DoPing.CancellationPending;
            if (e.Cancel)
                TLogger.Print("Thread for ping comps canceled");
            else
                TLogger.Print("Thread for ping comps finish");
#endif
        }
        #endregion

        #region Функции общие для всех списков ListView

        public void lvComps_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            ListView LV = sender as ListView;
            if (e.Item == null)
                e.Item = new ListViewItem();
            TPanelItemList ItemList = TPanelItemList.ListView_GetObject(LV);
            if (ItemList == null) return;
            //if (e.ItemIndex < 0 || e.ItemIndex > Math.Min(ItemList.Keys.Count, LV.VirtualListSize)-1)
            //    return;
            String ItemName = ItemList.Keys[e.ItemIndex];
            TPanelItem PItem = ItemList.Get(ItemName);
            if (PItem != null)
            {
                e.Item.Text = ItemName;
                string[] A = PItem.GetSubItems();
                foreach (string str in A)
                    e.Item.SubItems.Add(str);
                e.Item.ImageIndex = PItem.ImageIndex;
                e.Item.ToolTipText = PItem.ToolTipText;
            }
        }

        public void SendKeysCorrect(string Keys)
        {
            string Chars = "+^%~{}()[]";
            string NewKeys = "";
            foreach (Char Ch in Keys)
            {
                if (Chars.Contains(Ch.ToString()))
                    NewKeys += "{" + Ch.ToString() + "}";
                else
                    NewKeys = Ch.ToString();
            }
            SendKeys.Send(NewKeys);
        }

        public void lvComps_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetterOrDigit(e.KeyChar) || Char.IsPunctuation(e.KeyChar) || TPuntoSwitcher.IsValidChar(e.KeyChar))
            {
                SearchPanelVisible(true);
                ActiveControl = eFilter;
                eFilter.Focus();
                SendKeysCorrect(e.KeyChar.ToString());
                e.Handled = true;
            }
        }

        private void mCopyCompName_Click(object sender, EventArgs e)
        {
            ListView LV = GetActiveListView();
            TPanelItemList ItemList = TPanelItemList.ListView_GetObject(LV);
            StringBuilder S = new StringBuilder();
            foreach (int index in LV.SelectedIndices)
            {
                if (S.Length > 0)
                    S.AppendLine();
                S.Append(ItemList.Keys[index]);
            }
            if (S.Length > 0)
                Clipboard.SetText(S.ToString());
        }

        private void mCopyComment_Click(object sender, EventArgs e)
        {
            ListView LV = GetActiveListView();
            TPanelItemList ItemList = TPanelItemList.ListView_GetObject(LV);
            StringBuilder S = new StringBuilder();
            string ItemName = null;
            TPanelItem PItem = null;
            foreach (int index in LV.SelectedIndices)
            {
                if (S.Length > 0)
                    S.AppendLine();
                ItemName = ItemList.Keys[index];
                PItem = ItemList.Get(ItemName);
                if (PItem != null)
                    S.Append(PItem.Comment);
            }
            if (S.Length > 0)
                Clipboard.SetText(S.ToString());
        }

        private void mCopySelected_Click(object sender, EventArgs e)
        {
            ListView LV = GetActiveListView();
            TPanelItemList ItemList = TPanelItemList.ListView_GetObject(LV);
            StringBuilder S = new StringBuilder();
            string ItemName = null;
            TPanelItem PItem = null;
            foreach (int index in LV.SelectedIndices)
            {
                if (S.Length > 0)
                    S.AppendLine();
                ItemName = ItemList.Keys[index];
                PItem = ItemList.Get(ItemName);
                if (PItem != null)
                {
                    S.Append(ItemName);
                    S.Append("\t");
                    S.Append(PItem.Comment);
                }
            }
            if (S.Length > 0)
                Clipboard.SetText(S.ToString());
        }

        public void lvComps_KeyDown(object sender, KeyEventArgs e)
        {
            ListView LV = (sender as ListView);
            // Ctrl+A выделение всех элементов
            if (e.Control && e.KeyCode == Keys.A)
            {
                NativeMethods.SelectAllItems(LV);
                e.Handled = true;
            }
            // Ctrl+Enter
            if (e.Shift && e.KeyCode == Keys.Enter)
            {
                TPanelItem PItem = GetFocusedPanelItem(true, false);
                if (PItem is TComputerItem)
                    mCompOpen_Click(mCompOpen, new EventArgs());
                if (PItem is TShareItem)
                    mCompOpen_Click(mFolderOpen, new EventArgs());
                e.Handled = true;
            }
            // Shift+Enter в режиме администратора
            if (AdminMode && e.Control && e.KeyCode == Keys.Enter)
            {
                TPanelItem PItem = GetFocusedPanelItem(true, false);
                if (PItem is TComputerItem)
                    mCompOpen_Click(mRadmin1, new EventArgs());
                if (PItem is TShareItem)
                    if (!(PItem as TShareItem).IsPrinter)
                        mCompOpen_Click(mFAROpen, new EventArgs());
                e.Handled = true;
            }
            // Ctrl+Alt+Shift+S - отправка текстового сообщения
            if (e.Control && e.Alt && e.Shift && e.KeyCode == Keys.S)
            {
                SendChatMsg();
                e.Handled = true;
            }
            // клавишы для 1 вкладки
            if (Pages.SelectedIndex == pageNetwork)
            {
                if (e.KeyCode == Keys.Back)
                {
                    CompBrowser.LevelUp();
                    e.Handled = true;
                }
            }
            // клавишы для всех пользовательских вкладок
            else
            {
                if (e.KeyCode == Keys.Delete)
                {
                    TPanelItemList ItemList = TPanelItemList.ListView_GetObject(LV);
                    for (int i = LV.SelectedIndices.Count - 1; i >= 0; i--)
                    {
                        int Index = LV.SelectedIndices[i];
                        TPanelItem Comp = ItemList.Get(ItemList.Keys[Index]);
                        if (Comp != null)
                            ItemList.Delete(Comp);
                    }
                    LV.SelectedIndices.Clear();
                    ItemList.ApplyFilter();
                    LV.VirtualListSize = ItemList.FilterCount;
                }
            }
        }

        private void lvComps_ItemActivate(object sender, EventArgs e)
        {
            if (lvComps.FocusedItem == null)
            {
                if (lvComps.VirtualListSize > 0)
                    lvComps.FocusedItem = lvComps.Items[0];
                else
                    return;
            }
            TLogger.Print("lvComps_ItemActivate on {0}", lvComps.FocusedItem.ToString());
            if (CompBrowser.InternalStack.Count == 0)
            {
                TPanelItem Comp = GetFocusedPanelItem(true, true);
                if (Comp == null)
                    return;
            }
            CompBrowser.LevelDown();
        }

        public void lvRecent_ItemActivate(object sender, EventArgs e)
        {
            TLogger.Print("lvRecent_ItemActivate on ", (sender as ListView).FocusedItem.ToString());
            TPanelItem PItem = GetFocusedPanelItem(false, true);
            if (PItem != null)
                GotoFavoriteComp(PItem.Name);
        }
        #endregion

        #region Управление панелью для фильтрации

        void SearchPanelVisible(bool value)
        {
            tsBottom.Visible = value;
            /*
            if (!value)
                Pages.SelectedTab.Refresh();
             */
        }

        public int TotalItems
        {
            get
            {
                ListView LV = GetActiveListView();
                return LV.VirtualListSize;
            }
            set
            {
                ListView LV = GetActiveListView();
                TPanelItemList ItemList = TPanelItemList.ListView_GetObject(LV);
                int ShowCount, TotalCount;
                if (ItemList.IsFiltered)
                {
                    ShowCount = ItemList.FilterCount;
                    TotalCount = ItemList.Count;
                }
                else
                {
                    ShowCount = value;
                    TotalCount = value;
                }
                LV.VirtualListSize = ShowCount;
                if (ShowCount != TotalCount)
                    lItemsCount.Text = String.Format("Показано элементов: {0} из {1}", ShowCount, TotalCount);
                else
                    lItemsCount.Text = String.Format("Показано элементов: {0}", ShowCount);
            }
        }

        public void UpdateFilter(ListView LV, string NewFilter, bool bVisualUpdate)
        {
            TPanelItemList ItemList = TPanelItemList.ListView_GetObject(LV);
            List<string> SaveSelected = null;
            //string SaveCurrent = null;
            if (bVisualUpdate)
            {
                SaveSelected = ItemList.ListView_GetSelected(LV, false);
                // запоминаем выделенные элементы
                //if (LV.FocusedItem != null)
                  //  SaveCurrent = lvComps.FocusedItem.Text;
            }
            // меняем фильтр
            ItemList.FilterText = NewFilter;
            if (bVisualUpdate)
            {
                TotalItems = CompBrowser.InternalItemList.Count;
                eFilter.BackColor = TotalItems > 0 ? Color.White : Color.FromArgb(255, 102, 102); // Firefox Color
                // восстанавливаем выделенные элементы
                ItemList.ListView_SetSelected(LV, SaveSelected);
                //CompBrowser.SelectComputer(SaveCurrent);
                UpdateFilterPanel();
            }
        }

        private void UpdateFilterPanel()
        {
            ListView LV = GetActiveListView();
            TPanelItemList ItemList = TPanelItemList.ListView_GetObject(LV);
            string Text = ItemList.FilterText;
            eFilter.TextChanged -= eFilter_TextChanged;
            eFilter.Text = Text;
            eFilter.SelectionLength = 0;
            eFilter.SelectionStart = Text.Length;
            eFilter.TextChanged += eFilter_TextChanged;
            // показываем или скрываем панель фильтра
            SearchPanelVisible(ItemList.IsFiltered);
            // выводим количество элементов в статус
            TotalItems = ItemList.Count;
        }

        private void eFilter_TextChanged(object sender, EventArgs e)
        {
            UpdateFilter(GetActiveListView(), (sender as TextBox).Text, true);
        }

        private void eFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                this.ActiveControl = lvComps;
                lvComps.Focus();
                if (e.KeyCode == Keys.Up) SendKeys.Send("{UP}");
                if (e.KeyCode == Keys.Down) SendKeys.Send("{DOWN}");
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                this.ActiveControl = lvComps;
            }
        }
        #endregion


        /// <summary>
        /// Возвращает имя выбранного компьютера, предварительно проверив пингом включен ли он.
        /// </summary>
        /// <param name="bUpdateRecent">Добавлять ли комп в закладку Активность</param>
        /// <param name="bPingAndAsk">Пинговать ли комп</param>
        /// <returns>Возвращает TComputer</returns>
        public TPanelItem GetFocusedPanelItem(bool bUpdateRecent, bool bPingAndAsk)
        {
            ListView LV = GetActiveListView();
            if (LV.FocusedItem == null)
                return null;
            TPanelItemList ItemList = TPanelItemList.ListView_GetObject(LV);
            TPanelItem PItem = ItemList.Get(LV.FocusedItem.Text);
            if (PItem == null)
                return null;
            if (PItem is TComputerItem)
            {
                // пингуем
                if (bPingAndAsk && (PItem is TComputerItem))
                {
                    bool bPingResult = PingThread.FastPing(PItem.Name);
                    if ((PItem as TComputerItem).IsPingable != bPingResult)
                    {
                        (PItem as TComputerItem).IsPingable = bPingResult;
                        int FocusedIndex = LV.FocusedItem.Index;
                        LV.RedrawItems(FocusedIndex, FocusedIndex, false);
                    }
                    if (!bPingResult)
                    {
                        DialogResult Result = MessageBox.Show(
                            String.Format("Компьютер «{0}» не доступен посредством PING.\nПродолжить?", PItem.Name), "Запрос",
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (Result != DialogResult.Yes)
                            PItem = null;
                    }
                }
            }
            return PItem;
        }

        public void RunCmdOnFocusedItem(string TagCmd, string TagParent)
        {
            // получаем выбранный комп
            TPanelItem PItem = GetFocusedPanelItem(true, true);
            if (PItem == null) return;

            string CmdLine = TagCmd;
            string FmtParam = null;

            switch(TagParent)
            {
                case "computer":
                    if (PItem is TComputerItem)
                        FmtParam = PItem.Name;
                    else
                        if (PItem is TShareItem)
                            FmtParam = (PItem as TShareItem).ComputerName;
                    break;
                case "folder":
                    if (PItem is TComputerItem)
                        return;
                    if (PItem is TShareItem)
                        FmtParam = String.Format(@"\\{0}\{1}", (PItem as TShareItem).ComputerName, PItem.Name);
                    break;
            }
            
            if (!OsVersionHelper.Is64BitOperatingSystem())
                CmdLine = TagCmd.Replace("%ProgramFiles(x86)%", "%ProgramFiles%");
            else
                CmdLine = TagCmd;

            CmdLine = String.Format(Environment.ExpandEnvironmentVariables(CmdLine), FmtParam);
            string FName;
            string Params;
            PathUtils.ExplodeCmd(CmdLine, out FName, out Params);
            try
            {
                Process.Start(FName, Params);
            }
            catch
            {
                MessageBox.Show(String.Format("Не удалось выполнить команду:\n{0}", CmdLine), "Ошибка при запуске", 
                    MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
            }
        }

        private void mWMIDescription_Click(object sender, EventArgs e)
        {
            TPanelItem PItem = GetFocusedPanelItem(true, true);
            if (PItem == null) 
                return;
            TComputerItem Comp = null;
            int CompIndex = -1;
            if (PItem is TComputerItem)
            {
                Comp = PItem as TComputerItem;
                CompIndex = CompBrowser.LV.FocusedItem.Index;
            }
            if (PItem is TShareItem)
            {
                Comp = new TComputerItem();
                Comp.Name = (PItem as TShareItem).ComputerName;
            }
            string OldValue = "";
            string NewValue = "";

            // изменение описания компьютера через WMI
            try
            {
                // подключаемся к компьютеру
                ConnectionOptions connection = new ConnectionOptions();
                ManagementScope scope = new ManagementScope(String.Format(@"\\{0}\root\CIMV2", Comp.Name), connection);
                scope.Connect();
                // WMI-запрос
                ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
                // читаем описание
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    OldValue = queryObj["Description"].ToString();
                    break;
                }
                // запрос нового описания у пользователя
                inputBox.Caption = "Редактирование описания компьютера";
                inputBox.Prompt = @"Описание компьютера \\" + Comp.Name;
                NewValue = inputBox.Ask(OldValue, true);
                // если нажали OK и описание отличается от старого надо менять
                if (NewValue != null && NewValue != OldValue)
                {
                    // записываем описание компа
                    foreach (ManagementObject queryObj in searcher.Get())
                    {
                        queryObj["Description"] = NewValue;
                        queryObj.Put();
                    }
                    // обновляем в списке
                    Comp.Comment = NewValue;
                    if (CompIndex != -1)
                    {
                        CompBrowser.InternalItemList.ApplyFilter();
                        CompBrowser.LV.RedrawItems(CompIndex, CompIndex, false);
                    }
                    // сообщение об успешной смене описания
                    MessageBox.Show(this,
                        String.Format("Описание компьютера «{0}» успешно изменено.", Comp.Name), "Информация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                if ((uint)ex.ErrorCode == 0x800706BA)
                    MessageBox.Show(String.Format(
                        "Не удалось поделючиться к компьютеру \\\\{0}.\n" +
                        "Возможно удаленное подключение было заблокировано брэнмауэром.", Comp.Name), "Ошибка подключения",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка подключения",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
            }
        }

        public void mCompOpen_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem MenuItem = sender as ToolStripMenuItem;
            ToolStripMenuItem ParentItem = MenuItem.OwnerItem as ToolStripMenuItem;
            RunCmdOnFocusedItem(MenuItem.Tag.ToString(), ParentItem.Tag.ToString());
        }

        public void CancelCompRelatedThreads()
        {
            if (DoBrowse.IsBusy)
                DoBrowse.CancelAsync();
            if (DoPing.IsBusy)
                DoPing.CancelAsync();

        }
        
        #region Обработка событий для пользовательской вкладке

        public void GotoFavoriteComp(string ComputerName)
        {
            Pages.SelectedIndex = pageNetwork;
            lvComps.SelectedIndices.Clear();
            ListView LV = GetActiveListView();
            UpdateFilter(LV, "", false);
            // выходим на верхний уровень
            while (CompBrowser.InternalStack.Count > 0)
                CompBrowser.LevelUp();
            CompBrowser.InternalItemList.ListView_SelectComputer(LV, ComputerName);
            TPanelItem PItem = GetFocusedPanelItem(false, false);
            if (PItem != null && PItem.Name == ComputerName)
                lvComps_ItemActivate(lvComps, new EventArgs());
        }

        public string GetMD5FromString(string str)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(str));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));
            return sBuilder.ToString();
        }

        private void SendChatMsg()
        {
            TPanelItem Comp = GetFocusedPanelItem(false, true);
            if (Comp == null || !(Comp is TComputerItem)) return;
            if ((Comp as TComputerItem).EndPoint == null) return;
            string NewValue = "";
            IPHostEntry DestHost = Dns.GetHostEntry((Comp as TComputerItem).EndPoint.Address);
            inputBox.Caption = String.Format("Отправка сообщения на компьютер {0}", DestHost.HostName);
            inputBox.Prompt = "Текст сообщения";
            NewValue = inputBox.Ask(NewValue, false);
            if (NewValue != null && (Comp is TComputerItem))
            {
                // получаем md5 хэш ID-строки получателя
                string[] A = DestHost.HostName.Split('.');
                string DestID = GetMD5FromString((A[0] + "." + A[1]).ToLower());
                byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.UTF8.GetBytes(NewValue);
                string data = Convert.ToBase64String(toEncodeAsBytes);
                LANEX_SEND((Comp as TComputerItem).EndPoint, String.Format("{0}|{1}|{2}", MSG_LANEX_CHAT, DestID, data));
            }
        }

        public ListView GetActiveListView()
        {
            return (ListView)Pages.SelectedTab.Controls[0];
        }

        #endregion

        private void mContextClose_Click(object sender, EventArgs e)
        {
            IsFormVisible = false;
        }

        private void mLargeIcons_Click(object sender, EventArgs e)
        {
            ListView LV = GetActiveListView();
            LV.SuspendLayout();
            LV.BeginUpdate();
            try
            {
                switch (Int32.Parse((sender as ToolStripMenuItem).Tag.ToString()))
                {
                    case 1:
                        LV.View = View.LargeIcon;
                        break;
                    case 2:
                        LV.View = View.Details;
                        break;
                    case 3:
                        LV.View = View.SmallIcon;
                        break;
                    case 4:
                        LV.View = View.List;
                        break;
                }
            }
            finally
            {
                LV.EndUpdate();
                LV.SuspendLayout();
            }
        }

        private void popComps_Opened(object sender, EventArgs e)
        {
            mLargeIcons.Checked = false;
            mSmallIcons.Checked = false;
            mList.Checked = false;
            mDetails.Checked = false;
            ListView LV = GetActiveListView();
            switch (LV.View)
            {
                case View.LargeIcon:
                    mLargeIcons.Checked = true;
                    break;
                case View.SmallIcon:
                    mSmallIcons.Checked = true;
                    break;
                case View.List:
                    mList.Checked = true;
                    break;
                case View.Details:
                    mDetails.Checked = true;
                    break;
            }
            TPanelItem PItem = GetFocusedPanelItem(false, false);
            bool bCopyVisible = PItem != null;
            bool bCompVisible = false;
            bool bFolderVisible = false;
            bool bSenderIsComputer = (Pages.SelectedIndex > 0) || (CompBrowser.InternalStack.Count == 0);
            if (PItem != null)
            {
                if (PItem is TComputerItem)
                {
                    mComp.Text = @"\\" + PItem.Name;
                    bCompVisible = AdminMode;
                }
                if (PItem is TShareItem)
                {
                    if (PItem.Name == "..")
                    {
                        mComp.Text = @"\\" + (PItem as TShareItem).ComputerName;
                        bCompVisible = true;
                    }
                    else
                    {
                        mFolder.Text = String.Format(@"\\{0}\{1}", (PItem as TShareItem).ComputerName, PItem.Name);
                        bFolderVisible = AdminMode;
                        mFAROpen.Enabled = !(PItem as TShareItem).IsPrinter;
                    }
                }
            }
            if (!bSenderIsComputer)
            {
                bCompVisible = false;
                bCopyVisible = false;
            }
            
            mComp.Visible = bCompVisible;
            mComp.Enabled = bCompVisible;
            mWMIDescription.Visible = bCompVisible;
            mWMIDescription.Enabled = bCompVisible;
            mSeparatorAdmin.Visible = bCompVisible;

            mFolder.Visible = bFolderVisible;
            mFolder.Enabled = bFolderVisible;

            mCopyCompName.Enabled = bCopyVisible;
            mCopyComment.Enabled = bCopyVisible;
            mCopySelected.Enabled = bCopyVisible;
            mSendToTab.Enabled = bCopyVisible;
        }

        private void lCompName_Click(object sender, EventArgs e)
        {
            GotoFavoriteComp(Environment.MachineName);
        }

        private void DoCheckAdmin_DoWork(object sender, DoWorkEventArgs e)
        {
            #if CHECK_ADMIN
            e.Result = false;
            // если задан параметр /USER, то запускаем в режиме пользователя без проверки прав
            string[] args = Environment.GetCommandLineArgs();
            for (int i = 1; i < args.Length; i++ )
                if (args[i].ToUpper().Equals("/USER"))
                {
                    TLogger.Print("Parameter /USER found in cmd line AdminMode is OFF");
                    return;
                }
            TLogger.Print("Thread for checking admin rights start");
            try
            {
                //Assembly AccountManagement = System.Reflection.Assembly.Load("System.DirectoryServices.AccountManagement.dll");
                // проверка прав текущего пользователя
                System.DirectoryServices.AccountManagement.PrincipalContext pc_local =
                    new System.DirectoryServices.AccountManagement.PrincipalContext(System.DirectoryServices.AccountManagement.ContextType.Machine);
                System.DirectoryServices.AccountManagement.PrincipalContext pc_domain = null;
                System.DirectoryServices.AccountManagement.UserPrincipal CurrentUser =
                    System.DirectoryServices.AccountManagement.UserPrincipal.Current;
                // получаем локальную группу "Администраторы"
                var groupAdmins = System.DirectoryServices.AccountManagement.GroupPrincipal.FindByIdentity(pc_local,
                    System.DirectoryServices.AccountManagement.IdentityType.Sid, "S-1-5-32-544");
                if (groupAdmins != null)
                {
                    var groups = groupAdmins.GetMembers(false);

                    if (groups != null)
                        foreach (var group in groups)
                        {
                            // проверяем текущего пользователя
                            if (group.Sid.Equals(CurrentUser.Sid))
                            {
                                TLogger.Print("Current user [{0}] found in local group [{1}]", 
                                    CurrentUser.SamAccountName, groupAdmins.SamAccountName);
                                e.Result = true;
                                break;
                            }
                            else
                                // ищем доменную группу
                                if (group.ContextType == System.DirectoryServices.AccountManagement.ContextType.Domain)
                                {
                                    if (pc_domain == null)
                                        pc_domain = new System.DirectoryServices.AccountManagement.PrincipalContext(System.DirectoryServices.AccountManagement.ContextType.Domain);
                                    System.DirectoryServices.AccountManagement.GroupPrincipal gr =
                                        System.DirectoryServices.AccountManagement.GroupPrincipal.FindByIdentity(pc_domain, System.DirectoryServices.AccountManagement.IdentityType.Sid, group.Sid.ToString());
                                    // проверяем текущего пользователя на принадлежность группе, которая входит в "Администраторы"
                                    if (gr != null && CurrentUser.IsMemberOf(gr))
                                    {
                                        TLogger.Print("Current user [{0}] found in domain group [{1}]",
                                            CurrentUser.SamAccountName, gr.SamAccountName);
                                        e.Result = true;
                                        break;
                                    }
                                }
                        }
                }
            }
            catch (Exception ex)
            {
                TLogger.Print("Error: {0}", ex.Message);
            }
            TLogger.Print("Thread for checking admin rights finish");
            #endif
        }

        private void DoCheckAdmin_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
                AdminMode = (bool)e.Result;
        }

        private void tipComps_Popup(object sender, PopupEventArgs e)
        {
            if (!(e.AssociatedControl is ListView))
                return;
            ListView LV = (ListView)e.AssociatedControl;
            Point P = LV.PointToClient(Control.MousePosition);
            ListViewHitTestInfo Info = LV.HitTest(P);
            if (Info != null && Info.Item != null)
                (sender as ToolTip).ToolTipTitle = Info.Item.Text;
            else
                (sender as ToolTip).ToolTipTitle = "Информация";
        }

        private void tsBottom_DoubleClick(object sender, EventArgs e)
        {
            eFilter.Text = "";
        }

        // --- ИСХОДНЫЙ КОД ОТПРАВКИ СООБЩЕНИЙ ---

        // Отправка сообщения
        void LANEX_SEND(IPEndPoint ipendpoint, string MSG)
        {
            UdpClient udp = new UdpClient();

            // Указываем адрес отправки сообщения
            //IPAddress ipaddress = IPAddress.Parse("255.255.255.255");
            if (ipendpoint == null)
                ipendpoint = new IPEndPoint(IPAddress.Broadcast, 3333);

            // Формирование оправляемого сообщения и его отправка.
            // Сеть "понимает" только поток байтов и ей безраличны
            // объекты классов, строки и т.п. Поэтому преобразуем текстовое
            /// сообщение в поток байтов.
            byte[] message = Encoding.Default.GetBytes(MSG);
            int sended = udp.Send(message, message.Length, ipendpoint);

            // Если количество переданных байтов и предназначенных для 
            // отправки совпадают, то 99,9% вероятности, что они доберутся 
            // до адресата.
            if (sended == message.Length)
            {
                TLogger.Print("UDP SEND data [{0}] to [{1}]", MSG, ipendpoint.ToString());
            }


            // После окончания попытки отправки закрываем UDP соединение,
            // и освобождаем занятые объектом UdpClient ресурсы.
            udp.Close();
        }


        // --- ИСХОДНЫЙ КОД ИЗВЛЕЧЕНИЯ СООБЩЕНИЙ ---

        private Thread rec = null;
        private UdpClient udp = null;
        private bool stopReceive = false;
        private static IntPtr lvCompsHandle = new IntPtr(0);
        private string RefreshCompName = null;
        private string ChatMessage = null;

        // Запуск отдельного потока для приема сообщений
        void StartReceive()
        {
            rec = new Thread(new ThreadStart(Receive));
            rec.Start();
        }

        // Функция извлекающая пришедшие сообщения
        // работающая в отдельном потоке.
        void Receive()
        {
            TLogger.Print("Thread for udp receive start");
            // При принудительном завершении работы метода 
            // класса UdpClient Receive() и непредвиденных ситуациях 
            // возможны исключения в этом месте исходного кода,
            // заключим его в блок try чтобы не появлялись окна ошибок.
            try
            {
                // Перед созданием нового объекта закрываем старый
                // для освобождения занятых ресурсов.
                if (udp != null) udp.Close();

                int Port = 3333;
                TLogger.Print("UDP LISTEN on port {0}", Port);
                udp = new UdpClient(Port);

                while (true)
                {

                    IPEndPoint ipendpoint = new IPEndPoint(IPAddress.Any, 0);
                    byte[] message = udp.Receive(ref ipendpoint);
                    string HostName = Dns.GetHostEntry(ipendpoint.Address).HostName;
                    string CompName = HostName.Remove(HostName.IndexOf('.'), HostName.Length - HostName.IndexOf('.')).ToUpper();
                    string TextMsg = Encoding.Default.GetString(message);
                    string[] MSG = TextMsg.Split('|');

                    TLogger.Print("UDP RECV data [{0}] from [ip={1}, host={2}]", TextMsg, ipendpoint.ToString(), HostName);

                    TPanelItem PItem = CompBrowser.InternalItemList.Get(CompName);
                    TComputerItem Comp = PItem as TComputerItem;
                    bool bNeedRefresh = false;
                    if (Comp != null)
                    {
                        Comp.EndPoint = ipendpoint;
                        Comp.EndPoint.Port = 3333;
                        RefreshCompName = Comp.Name;
                    }
                    switch (MSG[0])
                    {
                        case MSG_LANEX_LOGIN:
                            if (Comp != null)
                            {
                                Comp.IsLogged = true;
                            }
                            // отвечаем на сообщение, чтобы наш комп отрисовался подключенным
                            ipendpoint.Port = 3333;
                            LANEX_SEND(ipendpoint, MSG_LANEX_LOGINOK);
                            bNeedRefresh = true;
                            break;
                        case MSG_LANEX_LOGINOK:
                            if (Comp != null)
                            {
                                Comp.IsLogged = true;
                            }
                            bNeedRefresh = true;
                            break;
                        case MSG_LANEX_LOGOUT:
                            if (Comp != null)
                            {
                                Comp.IsLogged = false;
                            }
                            bNeedRefresh = true;
                            break;

                        case MSG_LANEX_SHUTDOWN:
                            if (Comp != null)
                            {
                                Comp.IsLogged = false;
                                Comp.IsPingable = false;
                            }
                            bNeedRefresh = true;
                            break;
                        case MSG_LANEX_CHAT:
                            IPHostEntry MyHost = Dns.GetHostEntry(IPAddress.Loopback);
                            string[] A = MyHost.HostName.Split('.');
                            // получаем md5 хэш ID-строки получателя
                            string MyID = GetMD5FromString((A[0] + "." + A[1]).ToLower());
                            if (MyID.Equals(MSG[1]))
                            {
                                byte[] data = Convert.FromBase64String(MSG[2]);
                                ChatMessage = System.Text.ASCIIEncoding.UTF8.GetString(data);
                                MainFormInstance.Invoke(myTrayChat, MainFormInstance);
                            }
                            break;
                    }
                    // Если дана команда остановить поток, останавливаем бесконечный цикл.
                    if (stopReceive == true) break;
                    // обовление элемента списка
                    if (bNeedRefresh)
                    {
                        Pages.Invoke(myPagesRefresh, Pages);
                    }
                }

                udp.Close();
            }
            catch
            {
            }
            TLogger.Print("Thread for udp receive finish");
        }


        // Функция безопасной остановки дополнительного потока
        void StopReceive()
        {
            // Останавливаем цикл в дополнительном потоке
            stopReceive = true;

            // Принудительно закрываем объект класса UdpClient 
            if (udp != null) udp.Close();

            // Для корректного завершения дополнительного потока
            // подключаем его к основному потоку.
            if (rec != null) rec.Join();
        }

        //функция _getHost для сервера отличается только портом. Вместо 9050, отправлять всегда будем на 9051
        private EndPoint _getHost(string text)
        {
	        //вырезаем из строки только IP адрес формата IPv4
	        string host = text.Remove(text.IndexOf(":"), text.Length - text.IndexOf(":"));

	        //создаем объект адреса. Переменная host уже имеет вид [0-255].[0-255].[0-255].[0-255]
	        IPAddress hostIPAddress = IPAddress.Parse(host);

	        //создаем конечную точку. В нашем случае это адрес сервера, который слушает порт 9051
	        IPEndPoint hostIPEndPoint = new IPEndPoint(hostIPAddress, 9051);
	        EndPoint To = (EndPoint)(hostIPEndPoint);
	        return To;
        }

        private void Pages_Selected(object sender, TabControlEventArgs e)
        {
            if (Pages.SelectedTab != null)
            {
                ActiveControl = Pages.SelectedTab.Controls[0];
                ActiveControl.Focus();
                UpdateFilterPanel();
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (LegendForm F = new LegendForm())
            {
                F.ShowDialog();
            }
        }

        private void mSettings_Click(object sender, EventArgs e)
        {
            if (ParamsFormInstance == null)
            {
                using (ParamsFormInstance = new ParamsForm())
                {
                    ParamsFormInstance.ShowDialog();
                }
                ParamsFormInstance = null;
            }
        }

        private void mAbout_Click(object sender, EventArgs e)
        {
            if (AboutFormInstance == null)
            {
                using (AboutFormInstance = new AboutForm())
                {
                    AboutFormInstance.ShowDialog();
                }
                AboutFormInstance = null;
            }
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        void SetControlsVisible(bool bVisible)
        {
            foreach (Control control in this.Controls)
                control.Visible = bVisible;
        }

        void UpdateFlashMovie()
        {
            #if USE_FLASH
            if (FlashIndex == -1)
            {
                SetControlsVisible(true);
                axShockwaveFlash1.Stop();
                axShockwaveFlash1.Visible = false;
                axShockwaveFlash1.Movie = null;
            }
            else
            {
                SetControlsVisible(false);
                axShockwaveFlash1.Visible = true;
                axShockwaveFlash1.Movie = FlashMovies[FlashIndex];
            }
            #endif
        }

        void IncFlashMovieIndex(int Delta)
        {
            FlashIndex += Delta;
            if (FlashIndex < -1)
                FlashIndex = FlashMovies.Length - 1;
            if (FlashIndex > FlashMovies.Length - 1)
                FlashIndex = -1;
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            // скрываем все элементы формы
            SetControlsVisible(false);
            List<string> SWFFiles = new List<string>();
            foreach (string file in files)
            {
                if (Directory.Exists(file))
                {
                    string[] files2 = Directory.GetFiles(file, "*.swf", SearchOption.AllDirectories);
                    foreach (string file2 in files2)
                        SWFFiles.Add(file2);
                } else
                if (Path.GetExtension(file) == ".swf")
                    SWFFiles.Add(file);
            }
            FlashMovies = SWFFiles.ToArray();
            if (FlashMovies.Length > 0)
                FlashIndex = 0;
            else
                FlashIndex = -1;
            UpdateFlashMovie();
        }
        
        #region Редактирование и переключение вкладок    
  
        private void mNewTab_Click(object sender, EventArgs e)
        {
            TabController.NewTab();
        }

        private void mCloseTab_Click(object sender, EventArgs e)
        {
            TabController.CloseTab();
        }

        private void mRenameTab_Click(object sender, EventArgs e)
        {
            TabController.RenameTab();
        }

        private void mSaveTab_Click(object sender, EventArgs e)
        {
            TabController.SaveTab();
        }

        private void mListTab_Click(object sender, EventArgs e)
        {
            TabController.ListTab();
        }

        private void popPages_Opened(object sender, EventArgs e)
        {
            mSelectTab.DropDownItems.Clear();
            TabController.AddTabsToMenuItem(mSelectTab, TabController.mSelectTab_Click, false);
            mCloseTab.Enabled = TabController.CanModifyTab(Pages.SelectedIndex);
            mRenameTab.Enabled = mCloseTab.Enabled;
        }

        private void mSendToTab_DropDownOpened(object sender, EventArgs e)
        {
            while (mSendToTab.DropDownItems.Count > 2)
                mSendToTab.DropDownItems.RemoveAt(mSendToTab.DropDownItems.Count - 1);

            TabController.AddTabsToMenuItem(mSendToTab, TabController.mSendToSelectedTab_Click, true);
            // скрываем разделитель, если нет новых вкладок
            mAfterSendTo.Visible = mSendToTab.DropDownItems.Count > 2;
        }

        #endregion

        private bool admin_mode = false;

        public bool AdminMode
        {
            get { return admin_mode; }
            set
            {
                if (admin_mode != value)
                {
                    TLogger.Print("AdminMode is {0}", value ? "ON" : "OFF");
                    admin_mode = value;
                    popComps_Opened(popComps, new EventArgs());
                }
            }
        }

        public void mExit_Click(object sender, EventArgs e)
        {
            CancelCompRelatedThreads();
            Application.Exit();
        }

    }
}
