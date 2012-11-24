#define USE_PING
#define NOUSE_FLASH
#define NLOG

#if DEBUG
using NLog;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using OSTools;
using System.Net.NetworkInformation;
using EventHandlerSupport;
using LanExchange.Network;

namespace LanExchange.Forms
{
    public partial class MainForm : Form
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

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
        public NetworkBrowser  CompBrowser = null;
        TabController          TabController = null;
        FormWindowState         LastWindowState = FormWindowState.Normal;
        string[] FlashMovies;
        int FlashIndex = -1;
        bool bNetworkConnected = false;
        bool bNetworkJustPlugged = false;
        // время ожидания после подключения сети до начала сканирования списка компов
        int NetworkWaitAfterPluggedInSec = 3;
        private string m_CurrentDomain;

        public delegate void ProcRefresh(object obj, object data);
        public delegate void ChatRefresh(object obj, object data, object message);
        ChatRefresh myTrayChat;
        ProcRefresh myPagesRefresh;
        ProcRefresh myCompsRefresh;
        ProcRefresh myTimerRefresh;

        #endregion

        #region Инициализация и загрузка формы

        public MainForm()
        {
            InitializeComponent();
            //Localization.GetLocalization().ApplyLanguage(this);
            //SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            MainFormInstance = this;
            lvCompsHandle = lvComps.Handle;
            CompBrowser = new NetworkBrowser(lvComps);
            //PanelItemList.ListView_SetObject(lvComps, CompBrowser.InternalItemList);
            PanelItemList Items = new PanelItemList();
            Items.Changed += new EventHandler(Items_Changed);
            PanelItemList.ListView_SetObject(lvComps, Items);
            // содаем контроллер для управления вкладками
            TabController = new TabController(Pages);
            mSendToNewTab.Click += new System.EventHandler(TabController.mSendToNewTab_Click);

            // делегаты для обновления из потоков
            myCompsRefresh = new ProcRefresh(lvCompsRefreshMethod);
            myPagesRefresh = new ProcRefresh(PagesRefreshMethod);
            myTrayChat = new ChatRefresh(TrayChatMethod);
            myTimerRefresh = new ProcRefresh(BrowseTimerRefreshMethod);
            // запуск в свернутом режиме
            /*
            if (TSettings.IsRunMinimized)
            {
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
            }
            */
            /*
            hotkey = new GlobalHotkeys();
            hotkey.Handle = this.Handle;
            hotkey.RegisterGlobalHotKey( (int) Keys.Z, GlobalHotkeys.MOD_CONTROL | GlobalHotkeys.MOD_ALT);
             */
        }

        private void Items_Changed(object sender, EventArgs e)
        {
            ListViewEx LV = GetActiveListView();
            PanelItemList ItemList = PanelItemList.ListView_GetObject(LV);
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
            lock (lItemsCount)
            {
                if (ShowCount != TotalCount)
                    lItemsCount.Text = String.Format("Элементов: {0} из {1}", ShowCount, TotalCount);
                else
                    lItemsCount.Text = String.Format("Элементов: {0}", ShowCount);
            }
        }

        /*
        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;

            switch (m.Msg)
            {
                case WM_HOTKEY:
                    {
                        if ((short)m.WParam == hotkey.HotkeyID)
                        {
                            IsFormVisible = true;
                        }
                        break;
                    }
                default:
                    {
                        base.WndProc(ref m);
                        break;
                    }
            }
        }
         */

        public string GetMainFormTitle()
        {
            Assembly Me = Assembly.GetExecutingAssembly();
            object[] attributes = Me.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
            if (attributes.Length == 0)
            {
                return "";
            }
            Version Ver = Me.GetName().Version;
            return String.Format("{0} {1}.{2}", ((AssemblyProductAttribute)attributes[0]).Product, Ver.Major, Ver.Minor);
        }

        private void SetupForm()
        {
            TrayIcon.Visible = CanUseTray();
            this.Text = GetMainFormTitle();
            // размещаем форму внизу справа
            Rectangle Rect = new Rectangle();
            Rect.Size = new Size(450, Screen.PrimaryScreen.WorkingArea.Height);
            Rect.Location = new Point(Screen.PrimaryScreen.WorkingArea.Left + (Screen.PrimaryScreen.WorkingArea.Width - Rect.Width),
                                      Screen.PrimaryScreen.WorkingArea.Top + (Screen.PrimaryScreen.WorkingArea.Height - Rect.Height));
            this.SetBounds(Rect.X, Rect.Y, Rect.Width, Rect.Height);
            // domain name
            CurrentDomain = Utils.GetMachineNetBiosDomain(null);
            // выводим имя компьютера
            lCompName.Text = String.Format(@"{0}\{1}", CurrentDomain, SystemInformation.ComputerName);
            // выводим имя пользователя
            System.Security.Principal.WindowsIdentity user = System.Security.Principal.WindowsIdentity.GetCurrent();
            lUserName.Text = user.Name;
            /*
            string[] A = user.Name.Split('\\');
            if (A.Length > 1)
                lUserName.Text = A[1];
            else
                lUserName.Text = A[0];            
             */
            // режим отображения: Компьютеры
            CompBrowser.ViewType = NetworkBrowser.LVType.COMPUTERS;
            ActiveControl = lvComps;
        }

        private void SetupMenu()
        {
            ToolStripItem[] MyItems = new ToolStripItem[mComp.DropDownItems.Count];
            for (int i = 0; i < MyItems.Length; i++)
            {
                ToolStripItem TI = mComp.DropDownItems[i];
                if (TI is ToolStripSeparator)
                    MyItems[i] = new ToolStripSeparator();
                else
                    if (TI is ToolStripMenuItem)
                        MyItems[i] = (ToolStripItem)Extensions.Clone(TI as ToolStripMenuItem);
            }
            popTop.Items.Clear();
            popTop.Items.AddRange(MyItems);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetupForm();
            SetupMenu();
            if (!OSLayer.IsRunningOnMono())
            {
                // устанавливаем обработчик на изменение подключения к сети
                bNetworkConnected = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
                System.Net.NetworkInformation.NetworkChange.NetworkAvailabilityChanged += new System.Net.NetworkInformation.NetworkAvailabilityChangedEventHandler(NetworkChange_NetworkAvailabilityChanged);
                // права администратора берем из реестра
                AdminMode = Settings.IsAdvancedMode;
                // запуск обновления компов
                #if DEBUG
                NetworkScanner.GetInstance().RefreshInterval = 5 * 1000;
                #else
                NetworkScanner.GetInstance().RefreshInterval = Settings.RefreshTimeInSec * 1000;
                #endif
                //SetBrowseTimerInterval();
                //BrowseTimer_Tick(this, new EventArgs());
                // всплывающие подсказки
                TabView.ListView_SetupTip(lvComps);
                PanelItemList ItemList = PanelItemList.ListView_GetObject(lvComps);
                if (ItemList != null)
                {
                    NetworkScanner.GetInstance().SubscribeToSubject(ItemList, CurrentDomain);
                    //NetworkScanner.GetInstance().SubscribeToAll(ItemList);
                }
            }
        }

        private bool CanUseTray()
        {
            return !OSLayer.IsRunningOnMono();
        }

        /**
         * Изменилось состояние подключения локальной сети.
         * 
         */
        private void NetworkChange_NetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            logger.Info("Network availability changed: IsAvailable = {0}", e.IsAvailable);
            bNetworkConnected = e.IsAvailable;
            if (e.IsAvailable)
            {
                bNetworkJustPlugged = true;
                this.Invoke(myTimerRefresh, this, null);
            }
            else
            {
                CancelCompRelatedThreads();
                Pages.Invoke(myPagesRefresh, Pages, null);
            }
        }

        #endregion


        #region Методы обновления, вызываемые из асинхронных потоков
        void PagesRefreshMethod(object sender, object data)
        {
            TabPage Tab = Pages.SelectedTab;
            if (Tab == null) return;
            ListViewEx LV = (ListViewEx)Tab.Controls[0];
            LV.Invoke(myCompsRefresh, LV, data);
        }

        void lvCompsRefreshMethod(object sender, object data)
        {
            string RefreshCompName = (string)data;
            ListViewEx LV = (ListViewEx)sender;
            PanelItemList ItemList = PanelItemList.ListView_GetObject(LV);
            if (LV.Handle != IntPtr.Zero)
            {
                int Index;
                if (String.IsNullOrEmpty(RefreshCompName))
                {
                    LV.Refresh();
                }
                else
                {
                    Index = ItemList.Keys.IndexOf(RefreshCompName);
                    if (Index != -1)
                        LV.RedrawItems(Index, Index, false);
                }
            }
        }

        void TrayChatMethod(object sender, object data, object message)
        {
            string RefreshCompName = (string)data;
            string ChatMessage = (string)message;
            TrayIcon.ShowBalloonTip(10000, "Сообщение от " + RefreshCompName, ChatMessage, ToolTipIcon.Info);
        }

        void BrowseTimerRefreshMethod(object sender, object data)
        {
            //BrowseTimer_Tick(BrowseTimer, new EventArgs());
            //BrowseTimer.Interval = NetworkWaitAfterPluggedInSec * 1000;
            //BrowseTimer.Start();
        }

        #endregion



        #region Сворачивание формы в трей

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            logger.Info("MainForm is closing with reason {0}", e.CloseReason.ToString());
            bool bExiting = false;
            if (e.CloseReason == CloseReason.None || e.CloseReason == CloseReason.UserClosing)
            {
                if (CanUseTray())
                {
                    e.Cancel = true;
                    IsFormVisible = false;
                }
                else
                    bExiting = true;
            }
            else
                bExiting = true;
            if (bExiting)
            {
                TabController.StoreSettings();
                CancelCompRelatedThreads();
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
                logger.Info("Closing is canceled");
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
            {
                if (LastWindowState != this.WindowState)
                    logger.Info("WindowState is {0}", this.WindowState.ToString());
                LastWindowState = this.WindowState;
            }
            else
                logger.Info("WindowState is {0}", this.WindowState.ToString());
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
            // перезапуск приложения по Ctrl+Alt+R
            if (e.KeyCode == Keys.R && e.Control && e.Alt)
            {
                Application.Restart();
                e.Handled = true;
            }
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
        private List<string> CompareDataSources(IList<PanelItem> OldDT, IList<PanelItem> NewDT)
        {
            List<string> Result = new List<string>();
            if (OldDT == null || NewDT == null)
                return Result;
            Dictionary<string, int> OldHash = new Dictionary<string, int>();
            int value = 0;

            foreach (PanelItem Comp in OldDT)
                OldHash.Add(Comp.Name, 0);
            foreach (PanelItem Comp in NewDT)
                if (!OldHash.TryGetValue(Comp.Name, out value))
                    Result.Add(Comp.Name);
            return Result;
        }

        private void DoBrowse_DoWork(object sender, DoWorkEventArgs e)
        {
            /*
            logger.Info("Thread for browse network start");
            string domain = (string)e.Argument;
            e.Result = new BrowseProcessResult(domain, PanelItemList.GetServerList(domain));
            e.Cancel = DoPing.CancellationPending;
            if (e.Cancel)
                logger.Info("Thread for browse network canceled");
            else
                logger.Info("Thread for browse network finish");
             */
        }

        private void DoBrowse_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            /*
            NetworkScannerResult Result = (NetworkScannerResult)e.Result;
            if (!e.Cancelled)
            {
                // update server list
                List<string> AddedNew = CompareDataSources(CompBrowser.CurrentDataTable, Result.ServerList);
                List<string> RemovedNew = CompareDataSources(Result.ServerList, CompBrowser.CurrentDataTable);
                // если список компов не изменился, ничего не обновляем
                if (bNetworkJustPlugged || !(CompBrowser.CurrentDataTable != null && (AddedNew.Count == 0 && RemovedNew.Count == 0)))
                {
                    if (DoPing.IsBusy)
                        DoPing.CancelAsync();
                    // обновление грида
                    if (CompBrowser.InternalItemList.Count > 0)
                        foreach (PanelItem Comp in Result.ServerList)
                        {
                            PanelItem OldComp = CompBrowser.InternalItemList.Get(Comp.Name);
                            Comp.CopyExtraFrom(OldComp);
                        }
                    CompBrowser.CurrentDataTable = Result.ServerList;
                    CurrentDomain = Result.Domain;
                }
                if (bNetworkConnected && bFirstStart)
                {
                    bFirstStart = false;
                    // загрузка списка недавно использованных компов
                    try
                    {
                        TabController.LoadSettings();
                    }
                    catch (Exception ex)
                    {
                        logger.Info("Can't load settings: {0}", ex.ToString());
                    }
                    // выбираем текущий компьютер, чтобы пользователь видел описание
                    CListViewEx LV = GetActiveListView();
                    PanelItemList ItemList = PanelItemList.ListView_GetObject(LV);
                    if (ItemList != null)
                        ItemList.ListView_SelectComputer(LV, Environment.MachineName);
                    // запускаем поток на ожидание UDP сообщений
                    StartReceive();
                    // сообщаем другим клиентам о запуске
                    LANEX_SEND(null, MSG_LANEX_LOGIN);
                }
                // запускаем процесс пинга
                #if USE_PING
                if (!DoPing.IsBusy)
                    DoPing.RunWorkerAsync(Result.ServerList);
                #endif
            }
            // запускаем таймер для следующего обновления
            //SetBrowseTimerInterval();
            //BrowseTimer.Enabled = true;
             */
        }

        private void DoPing_DoWork(object sender, DoWorkEventArgs e)
        {
#if USE_PING
            List<PanelItem> ScannedComps = (List<PanelItem>)e.Argument;
            if (ScannedComps == null || ScannedComps.Count == 0)
                return;
            if (!(ScannedComps[0] is ComputerPanelItem))
                return;
            logger.Info("Thread for ping comps start");
            // пингуем найденные компы
            foreach (PanelItem Comp in ScannedComps)
            {
                if (DoPing.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                bool bNewPingable = PingThread.FastPing(Comp.Name);
                if ((Comp as ComputerPanelItem).IsPingable != bNewPingable)
                {
                        (Comp as ComputerPanelItem).IsPingable = bNewPingable;
                        //Pages.Invoke(myPagesRefresh, Pages, Comp.Name);
                }
            }
            e.Cancel = DoPing.CancellationPending;
            if (e.Cancel)
                logger.Info("Thread for ping comps canceled");
            else
                logger.Info("Thread for ping comps finish");
#endif
        }
        #endregion

        #region Функции общие для всех списков ListView

        public void lvComps_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            ListViewEx LV = sender as ListViewEx;
            if (LV == null)
                return;
            PanelItemList ItemList = PanelItemList.ListView_GetObject(LV);
            if (ItemList == null) 
                return;
            if (e.ItemIndex < 0 || e.ItemIndex > Math.Min(ItemList.Keys.Count, LV.VirtualListSize)-1)
                return;
            if (e.Item == null)
                e.Item = new ListViewItem();
            String ItemName = ItemList.Keys[e.ItemIndex];
            PanelItem PItem = ItemList.Get(ItemName);
            if (PItem != null)
            {
                e.Item.Text = ItemName;
                string[] A = PItem.GetSubItems();
                foreach (string str in A)
                    e.Item.SubItems.Add(str);
                if (!bNetworkConnected)
                    e.Item.ImageIndex = ComputerPanelItem.imgCompRed;
                else
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
            if (Char.IsLetterOrDigit(e.KeyChar) || Char.IsPunctuation(e.KeyChar) || PuntoSwitcher.IsValidChar(e.KeyChar))
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
            ListViewEx LV = GetActiveListView();
            PanelItemList ItemList = PanelItemList.ListView_GetObject(LV);
            StringBuilder S = new StringBuilder();
            foreach (int index in LV.SelectedIndices)
            {
                if (S.Length > 0)
                    S.AppendLine();
                S.Append(@"\\" + ItemList.Keys[index]);
            }
            if (S.Length > 0)
                Clipboard.SetText(S.ToString());
        }

        private void mCopyPath_Click(object sender, EventArgs e)
        {
            ListViewEx LV = GetActiveListView();
            PanelItemList ItemList = PanelItemList.ListView_GetObject(LV);
            StringBuilder S = new StringBuilder();
            string ItemName = null;
            SharePanelItem PItem = null;
            foreach (int index in LV.SelectedIndices)
            {
                if (S.Length > 0)
                    S.AppendLine();
                ItemName = ItemList.Keys[index];
                if (!String.IsNullOrEmpty(ItemName))
                {
                    PItem = ItemList.Get(ItemName) as SharePanelItem;
                    if (PItem != null)
                        S.Append(String.Format(@"\\{0}\{1}", PItem.ComputerName, PItem.Name));
                }
            }
            if (S.Length > 0)
                Clipboard.SetText(S.ToString());
        }

        private void mCopyComment_Click(object sender, EventArgs e)
        {
            ListViewEx LV = GetActiveListView();
            PanelItemList ItemList = PanelItemList.ListView_GetObject(LV);
            StringBuilder S = new StringBuilder();
            PanelItem PItem = null;
            foreach (int index in LV.SelectedIndices)
            {
                if (S.Length > 0)
                    S.AppendLine();
                PItem = ItemList.Get(ItemList.Keys[index]);
                if (PItem != null)
                    S.Append(PItem.Comment);
            }
            if (S.Length > 0)
                Clipboard.SetText(S.ToString());
        }

        private void mCopySelected_Click(object sender, EventArgs e)
        {
            ListViewEx LV = GetActiveListView();
            PanelItemList ItemList = PanelItemList.ListView_GetObject(LV);
            StringBuilder S = new StringBuilder();
            string ItemName = null;
            PanelItem PItem = null;
            foreach (int index in LV.SelectedIndices)
            {
                if (S.Length > 0)
                    S.AppendLine();
                ItemName = ItemList.Keys[index];
                PItem = ItemList.Get(ItemName);
                if (PItem != null)
                {
                    S.Append(@"\\" + ItemName);
                    S.Append("\t");
                    S.Append(PItem.Comment);
                }
            }
            if (S.Length > 0)
                Clipboard.SetText(S.ToString());
        }

        public void lvComps_KeyDown(object sender, KeyEventArgs e)
        {
            ListViewEx LV = (sender as ListViewEx);
            // Ctrl+A выделение всех элементов
            if (e.Control && e.KeyCode == Keys.A)
            {
                NativeMethods.SelectAllItems(LV);
                e.Handled = true;
            }
            // Shift+Enter
            if (e.Shift && e.KeyCode == Keys.Enter)
            {
                PanelItem PItem = GetFocusedPanelItem(true, false);
                if (PItem is ComputerPanelItem)
                    mCompOpen_Click(mCompOpen, new EventArgs());
                if (PItem is SharePanelItem)
                    mFolderOpen_Click(mFolderOpen, new EventArgs());
                e.Handled = true;
            }
            // Ctrl+Enter в режиме администратора
            if (AdminMode && e.Control && e.KeyCode == Keys.Enter)
            {
                PanelItem PItem = GetFocusedPanelItem(true, false);
                if (PItem is ComputerPanelItem)
                    mCompOpen_Click(mRadmin1, new EventArgs());
                if (PItem is SharePanelItem)
                    if (!(PItem as SharePanelItem).IsPrinter)
                        mFolderOpen_Click(mFAROpen, new EventArgs());
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
                    PanelItemList ItemList = PanelItemList.ListView_GetObject(LV);
                    for (int i = LV.SelectedIndices.Count - 1; i >= 0; i--)
                    {
                        int Index = LV.SelectedIndices[i];
                        PanelItem Comp = ItemList.Get(ItemList.Keys[Index]);
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
            logger.Info("lvComps_ItemActivate on {0}", lvComps.FocusedItem.ToString());
            if (CompBrowser.InternalStack.Count == 0)
            {
                PanelItem Comp = GetFocusedPanelItem(true, true);
                if (Comp == null)
                    return;
            }
            CompBrowser.LevelDown();
        }

        private void lvComps_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.Item == null) 
                return;
            if (CompBrowser.InternalStack.Count == 0)
            {
                ListViewEx LV = GetActiveListView();
                PanelItemList ItemList = PanelItemList.ListView_GetObject(LV);
                PanelItem PItem = ItemList.Get(e.Item.Text);
                ComputerPanelItem Comp = PItem as ComputerPanelItem;
                if (Comp == null)
                    return;
                lInfoComp.Text = Comp.Name;
                lInfoDesc.Text = Comp.Comment;
                lInfoOS.Text   = Comp.SI.Version();
                imgInfo.Image = ilSmall.Images[PItem.ImageIndex];
                switch (Comp.ImageIndex)
                {
                    case ComputerPanelItem.imgCompDefault:
                        this.tipComps.SetToolTip(this.imgInfo, "Компьютер найден в результате обзора сети.");
                        break;
                    case ComputerPanelItem.imgCompRed:
                        this.tipComps.SetToolTip(this.imgInfo, "Компьютер не доступен посредством PING.");
                        break;
                    case ComputerPanelItem.imgCompGreen:
                        this.tipComps.SetToolTip(this.imgInfo, "Компьютер с запущенной программой LanExchange.");
                        break;
                    default:
                        this.tipComps.SetToolTip(this.imgInfo, "");
                        break;
                }
            }
        }


        public void lvRecent_ItemActivate(object sender, EventArgs e)
        {
            logger.Info("lvRecent_ItemActivate on ", (sender as ListView).FocusedItem.ToString());
            PanelItem PItem = GetFocusedPanelItem(false, true);
            if (PItem != null)
                GotoFavoriteComp(PItem.Name);
        }
        #endregion

        #region Управление панелью для фильтрации

        void SearchPanelVisible(bool value)
        {
            tsBottom.Visible = value;
            if (!value)
                Pages.SelectedTab.Refresh();
        }

        public int TotalItems
        {
            get
            {
                ListViewEx LV = GetActiveListView();
                return LV.VirtualListSize;
            }
            set
            {
                /*
                ListViewEx LV = GetActiveListView();
                PanelItemList ItemList = PanelItemList.ListView_GetObject(LV);
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
                    lItemsCount.Text = String.Format("Элементов: {0} из {1}", ShowCount, TotalCount);
                else
                    lItemsCount.Text = String.Format("Элементов: {0}", ShowCount);
                 */
            }
        }

        public void UpdateFilter(ListViewEx LV, string NewFilter, bool bVisualUpdate)
        {
            PanelItemList ItemList = PanelItemList.ListView_GetObject(LV);
            List<string> SaveSelected = null;

            // выходим на верхний уровень
            if (!String.IsNullOrEmpty(NewFilter))
                while (CompBrowser.InternalStack.Count > 0)
                    CompBrowser.LevelUp();
            
          
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
                /*
                TotalItems = CompBrowser.InternalItemList.Count;
                eFilter.BackColor = TotalItems > 0 ? Color.White : Color.FromArgb(255, 102, 102); // Firefox Color
                // восстанавливаем выделенные элементы
                ItemList.ListView_SetSelected(LV, SaveSelected);
                //CompBrowser.SelectComputer(SaveCurrent);
                UpdateFilterPanel();
                 */
            }
            else
                LV.VirtualListSize = ItemList.FilterCount;
        }

        public void UpdateFilterPanel()
        {
            ListViewEx LV = GetActiveListView();
            PanelItemList ItemList = PanelItemList.ListView_GetObject(LV);
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
        public PanelItem GetFocusedPanelItem(bool bUpdateRecent, bool bPingAndAsk)
        {
            ListViewEx LV = GetActiveListView();
            if (LV.FocusedItem == null)
                return null;
            PanelItemList ItemList = PanelItemList.ListView_GetObject(LV);
            PanelItem PItem = ItemList.Get(LV.FocusedItem.Text);
            if (PItem == null)
                return null;
            if (PItem is ComputerPanelItem)
            {
                // пингуем
                if (bPingAndAsk && (PItem is ComputerPanelItem))
                {
                    bool bPingResult = PingThread.FastPing(PItem.Name);
                    if ((PItem as ComputerPanelItem).IsPingable != bPingResult)
                    {
                        (PItem as ComputerPanelItem).IsPingable = bPingResult;
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

        private const string COMPUTER_MENU = "computer";
        private const string FOLDER_MENU = "folder";

        /// <summary>
        /// Run parametrized cmdline for focused panel item.
        /// {0} is computer name
        /// {1} is folder name
        /// </summary>
        /// <param name="TagCmd">cmdline from Tag of menu item</param>
        /// <param name="TagParent">Can be "computer" or "folder"</param>
        public void RunCmdOnFocusedItem(string TagCmd, string TagParent)
        {
            // получаем выбранный комп
            PanelItem PItem = GetFocusedPanelItem(true, true);
            if (PItem == null) return;

            string CmdLine = TagCmd;
            string FmtParam = null;

            switch(TagParent)
            {
                case COMPUTER_MENU:
                    if (PItem is ComputerPanelItem)
                        FmtParam = PItem.Name;
                    else
                        if (PItem is SharePanelItem)
                            FmtParam = (PItem as SharePanelItem).ComputerName;
                    break;
                case FOLDER_MENU:
                    if (PItem is ComputerPanelItem)
                        return;
                    if (PItem is SharePanelItem)
                        FmtParam = String.Format(@"\\{0}\{1}", (PItem as SharePanelItem).ComputerName, PItem.Name);
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
            PanelItem PItem = GetFocusedPanelItem(true, true);
            if (PItem == null) 
                return;
            ComputerPanelItem Comp = null;
            int CompIndex = -1;
            if (PItem is ComputerPanelItem)
            {
                Comp = PItem as ComputerPanelItem;
                CompIndex = CompBrowser.LV.FocusedItem.Index;
            }
            if (PItem is SharePanelItem)
            {
                Comp = new ComputerPanelItem();
                Comp.Name = (PItem as SharePanelItem).ComputerName;
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
                        //!!!
                        //CompBrowser.InternalItemList.ApplyFilter();
                        //CompBrowser.LV.RedrawItems(CompIndex, CompIndex, false);
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
            RunCmdOnFocusedItem(MenuItem.Tag.ToString(), COMPUTER_MENU);
        }

        public void mFolderOpen_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem MenuItem = sender as ToolStripMenuItem;
            RunCmdOnFocusedItem(MenuItem.Tag.ToString(), FOLDER_MENU);
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
            lvComps.BeginUpdate();
            try
            {
                lvComps.SelectedIndices.Clear();
                ListViewEx LV = GetActiveListView();
                UpdateFilter(LV, "", false);
                // выходим на верхний уровень
                while (CompBrowser.InternalStack.Count > 0)
                    CompBrowser.LevelUp();
                //!!!
                //CompBrowser.InternalItemList.ListView_SelectComputer(LV, ComputerName);
                PanelItem PItem = GetFocusedPanelItem(false, false);
                if (PItem != null && PItem.Name == ComputerName)
                    lvComps_ItemActivate(lvComps, new EventArgs());
            }
            finally
            {
                lvComps.EndUpdate();
            }
            UpdateFilterPanel();
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
            PanelItem Comp = GetFocusedPanelItem(false, true);
            if (Comp == null || !(Comp is ComputerPanelItem)) return;
            if ((Comp as ComputerPanelItem).EndPoint == null) return;
            string NewValue = "";
            IPHostEntry DestHost = Dns.GetHostEntry((Comp as ComputerPanelItem).EndPoint.Address);
            inputBox.Caption = String.Format("Отправка сообщения на компьютер {0}", DestHost.HostName);
            inputBox.Prompt = "Текст сообщения";
            NewValue = inputBox.Ask(NewValue, false);
            if (NewValue != null && (Comp is ComputerPanelItem))
            {
                // получаем md5 хэш ID-строки получателя
                string[] A = DestHost.HostName.Split('.');
                string DestID = GetMD5FromString((A[0] + "." + A[1]).ToLower());
                byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.UTF8.GetBytes(NewValue);
                string data = Convert.ToBase64String(toEncodeAsBytes);
                LANEX_SEND((Comp as ComputerPanelItem).EndPoint, String.Format("{0}|{1}|{2}", MSG_LANEX_CHAT, DestID, data));
            }
        }

        public ListViewEx GetActiveListView()
        {
            return (ListViewEx)Pages.SelectedTab.Controls[0];
        }

        #endregion

        private void mContextClose_Click(object sender, EventArgs e)
        {
            if (CanUseTray())
                IsFormVisible = false;
            else
                Application.Exit();
        }

        private void mLargeIcons_Click(object sender, EventArgs e)
        {
            ListViewEx LV = GetActiveListView();
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

        private void SetEnabledAndVisible(ToolStripItem Item, bool Value)
        {
            Item.Enabled = Value;
            Item.Visible = Value;
        }

        private void SetEnabledAndVisible(ToolStripItem[] Items, bool Value)
        {
            foreach (ToolStripItem Item in Items)
                SetEnabledAndVisible(Item, Value);
        }

        
        private void popTop_Opened(object sender, EventArgs e)
        {
            popComps_Opened(sender, e);
            for (int i = 0; i < Math.Min(mComp.DropDownItems.Count, popTop.Items.Count); i++)
            {
                ToolStripItem Src = mComp.DropDownItems[i];
                ToolStripItem Dest = popTop.Items[i];
                if (Src is ToolStripMenuItem && Dest is ToolStripMenuItem)
                    (Dest as ToolStripMenuItem).ShowShortcutKeys = (Src as ToolStripMenuItem).ShowShortcutKeys;
            }
       }

        private void popComps_Opened(object sender, EventArgs e)
        {
            #region set radio button
            mCompLargeIcons.Checked = false;
            mCompSmallIcons.Checked = false;
            mCompList.Checked = false;
            mCompDetails.Checked = false;
            ListViewEx LV = GetActiveListView();
            switch (LV.View)
            {
                case View.LargeIcon:
                    mCompLargeIcons.Checked = true;
                    break;
                case View.SmallIcon:
                    mCompSmallIcons.Checked = true;
                    break;
                case View.List:
                    mCompList.Checked = true;
                    break;
                case View.Details:
                    mCompDetails.Checked = true;
                    break;
            }
            #endregion
            PanelItem PItem = GetFocusedPanelItem(false, false);
            bool bCompVisible = false;
            bool bFolderVisible = false;
            if (PItem != null)
            {
                if (PItem is ComputerPanelItem)
                {
                    mComp.Text = @"\\" + PItem.Name;
                    bCompVisible = AdminMode;
                }
                if (PItem is SharePanelItem)
                {
                    mComp.Text = @"\\" + (PItem as SharePanelItem).ComputerName;
                    bCompVisible = AdminMode;
                    if (!String.IsNullOrEmpty(PItem.Name))
                    {
                        mFolder.Text = String.Format(@"\\{0}\{1}", (PItem as SharePanelItem).ComputerName, PItem.Name);
                        mFolder.Image = ilSmall.Images[PItem.ImageIndex];
                        bFolderVisible = true;
                        mFAROpen.Enabled = !(PItem as SharePanelItem).IsPrinter;
                    }
                }
            }
            SetEnabledAndVisible(mComp, bCompVisible);
            SetEnabledAndVisible(mFolder, bFolderVisible);

            bool bSenderIsComputer = (Pages.SelectedIndex > 0) || (CompBrowser.InternalStack.Count == 0);
            SetEnabledAndVisible(new ToolStripItem[] { 
                mCopyCompName, mCopyComment, mCopySelected, 
                mSendSeparator, mSendToTab }, bSenderIsComputer);
            SetEnabledAndVisible(mCopyPath, !bSenderIsComputer);

            mSeparatorAdmin.Visible = bCompVisible || bFolderVisible;

            // resolve computer related and folder related shortcut conflict
            mCompOpen.ShowShortcutKeys = bCompVisible && !bFolderVisible;
            mRadmin1.ShowShortcutKeys = bCompVisible && !bFolderVisible;
        }

        private void lCompName_Click(object sender, EventArgs e)
        {
            GotoFavoriteComp(SystemInformation.ComputerName);
        }

        private void tipComps_Popup(object sender, PopupEventArgs e)
        {
            if (e.AssociatedControl == imgInfo)
            {
                (sender as ToolTip).ToolTipIcon = ToolTipIcon.Info;
                (sender as ToolTip).ToolTipTitle = "Легенда";
                return;
            }
            if (e.AssociatedControl is ListViewEx)
            {
                ListViewEx LV = (ListViewEx)e.AssociatedControl;
                Point P = LV.PointToClient(Control.MousePosition);
                ListViewHitTestInfo Info = LV.HitTest(P);
                if (Info != null && Info.Item != null)
                    (sender as ToolTip).ToolTipTitle = Info.Item.Text;
                else
                    (sender as ToolTip).ToolTipTitle = "Информация";
                return;
            }
            (sender as ToolTip).ToolTipTitle = "";
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
                logger.Info("UDP SEND data [{0}] to [{1}]", MSG, ipendpoint.ToString());
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
            /*
            logger.Info("Thread for udp receive start");
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
                logger.Info("UDP LISTEN on port {0}", Port);
                udp = new UdpClient(Port);

                while (true)
                {

                    IPEndPoint ipendpoint = new IPEndPoint(IPAddress.Any, 0);
                    byte[] message = udp.Receive(ref ipendpoint);
                    string HostName = Dns.GetHostEntry(ipendpoint.Address).HostName;
                    string CompName = HostName.Remove(HostName.IndexOf('.'), HostName.Length - HostName.IndexOf('.')).ToUpper();
                    string TextMsg = Encoding.Default.GetString(message);
                    string[] MSG = TextMsg.Split('|');

                    logger.Info("UDP RECV data [{0}] from [ip={1}, host={2}]", TextMsg, ipendpoint.ToString(), HostName);

                    PanelItem PItem = CompBrowser.InternalItemList.Get(CompName);
                    ComputerPanelItem Comp = PItem as ComputerPanelItem;
                    bool bNeedRefresh = false;
                    string RefreshCompName = String.Empty;
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
                                string ChatMessage = System.Text.ASCIIEncoding.UTF8.GetString(data);
                                MainFormInstance.Invoke(myTrayChat, RefreshCompName, ChatMessage);
                            }
                            break;
                    }
                    // Если дана команда остановить поток, останавливаем бесконечный цикл.
                    if (stopReceive == true) break;
                    // обовление элемента списка
                    if (bNeedRefresh)
                    {
                        Pages.Invoke(myPagesRefresh, Pages, RefreshCompName);
                    }
                }

                udp.Close();
            }
            catch
            {
            }
            logger.Info("Thread for udp receive finish");
             */
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
                    logger.Info("AdminMode is {0}", value ? "ON" : "OFF");
                    admin_mode = value;
                    popComps_Opened(popComps, new EventArgs());
                }
            }
        }

        public void mExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            eFilter.Text = "";
        }

        private void imgClear_MouseHover(object sender, EventArgs e)
        {
            imgClear.Image = LanExchange.Properties.Resources.clear_hover;
        }

        private void imgClear_MouseLeave(object sender, EventArgs e)
        {
            imgClear.Image = LanExchange.Properties.Resources.clear_normal;
        }

        public string CurrentDomain
        {
            get { return m_CurrentDomain; }
            set
            {
                m_CurrentDomain = value;
                Pages.TabPages[0].Text = value;
            }
        }

        private void mTabParams_Click(object sender, EventArgs e)
        {
            using (TabParamsForm Form = new TabParamsForm())
            {
                Form.ShowDialog();
            }
        }
    }
}