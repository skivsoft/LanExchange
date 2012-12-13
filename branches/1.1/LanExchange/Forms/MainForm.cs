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
using LanExchange.Network;
using System.Security.Principal;
using LanExchange.Properties;

namespace LanExchange.Forms
{
    public partial class MainForm : Form
    {
        // logger object 
        private readonly static Logger logger = LogManager.GetCurrentClassLogger();
        // single instance for MainForm
        public static MainForm Instance;
        // controller for Pages (MVC-style)
        readonly TabController TabController;
        
        FormWindowState  LastWindowState = FormWindowState.Normal;

        public MainForm()
        {
            InitializeComponent();
            Instance = this;
            // load settings from cfg-file
            Settings.LoadSettings();
            // init main form
            SetupFormBounds();
            SetupForm();
            SetupMenu();
            // init network scanner
#if DEBUG
            NetworkScanner.GetInstance().RefreshInterval = 10 * 1000; // refresh every 5 sec in debug mode
#else
            NetworkScanner.GetInstance().RefreshInterval = Settings.RefreshTimeInSec * 1000;
#endif
            // init tab manager
            TabController = new TabController(Pages);
            mSendToNewTab.Click += new EventHandler(TabController.mSendToNewTab_Click);
            TabController.GetModel().LoadSettings();
            // set admin mode
            AdminMode = Settings.Instance.AdvancedMode;
        }

        /// <summary>
        /// Returns ProductName and Version for MainForm title.
        /// </summary>
        /// <returns>a MainForm title</returns>
        public static string GetMainFormTitle()
        {
            var Me = Assembly.GetExecutingAssembly();
            object[] attributes = Me.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
            if (attributes.Length == 0)
            {
                return "";
            }
            var Ver = Me.GetName().Version;
            return String.Format("{0} {1}.{2}", ((AssemblyProductAttribute)attributes[0]).Product, Ver.Major, Ver.Minor);
        }

        private void SetupFormBounds()
        {
            var Rect = new Rectangle();
            Rect.Size = new Size(450, Screen.PrimaryScreen.WorkingArea.Height);
            Rect.Location = new Point(Screen.PrimaryScreen.WorkingArea.Left + (Screen.PrimaryScreen.WorkingArea.Width - Rect.Width),
                                      Screen.PrimaryScreen.WorkingArea.Top + (Screen.PrimaryScreen.WorkingArea.Height - Rect.Height));
            SetBounds(Rect.X, Rect.Y, Rect.Width, Rect.Height);
        }

        private void SetupForm()
        {
            TrayIcon.Visible = true;
            Text = GetMainFormTitle();
            // выводим имя компьютера
            lCompName.Text = SystemInformation.ComputerName;
            // выводим имя пользователя
            var user = WindowsIdentity.GetCurrent();
            lUserName.Text = user.Name;
            string[] A = user.Name.Split('\\');
            if (A.Length > 1)
                lUserName.Text = A[1];
            else
                lUserName.Text = A[0];
        }

        private void SetupMenu()
        {
            ToolStripItem[] MyItems = new ToolStripItem[mComp.DropDownItems.Count];
            for (int i = 0; i < MyItems.Length; i++)
            {
                var TI = mComp.DropDownItems[i];
                if (TI is ToolStripSeparator)
                    MyItems[i] = new ToolStripSeparator();
                else
                    if (TI is ToolStripMenuItem)
                        MyItems[i] = (ToolStripItem)MenuUtils.Clone(TI as ToolStripMenuItem);
            }
            popTop.Items.Clear();
            popTop.Items.AddRange(MyItems);
        }

        public string StatusText
        {
            get { return lItemsCount.Text; }
            set { lItemsCount.Text = value; }
        }


        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            logger.Info("MainForm is closing with reason {0}", e.CloseReason.ToString());
            if (e.CloseReason == CloseReason.None || e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                IsFormVisible = false;
                logger.Info("Closing is canceled");
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            TabController.GetModel().StoreSettings();
            Settings.SaveSettings();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized)
            {
                if (LastWindowState != WindowState)
                    logger.Info("WindowState is {0}", WindowState.ToString());
                LastWindowState = WindowState;
            }
            else
                logger.Info("WindowState is {0}", WindowState.ToString());
        }

        public bool bReActivate;

        public bool IsFormVisible
        {
            get { return WindowState != FormWindowState.Minimized && Visible; }
            set
            {
                bool bMinimized = WindowState == FormWindowState.Minimized;
                if (bMinimized)
                {
                    ShowInTaskbar = true;
                    WindowState = LastWindowState;
                }
                else
                {
                    if (bReActivate)
                    {
                        bReActivate = false;
                        ShowInTaskbar = true;
                        WindowState = FormWindowState.Minimized;
                        WindowState = LastWindowState;
                    }
                }
                Visible = value;
                if (Visible)
                {
                    Activate();
                    //this.ActiveControl = lvComps;
                    //lvComps.Focus();
                }
            }
        }

        private void popTray_Opening(object sender, CancelEventArgs e)
        {
            mOpen.Text = IsFormVisible ? "Скрыть" : "Открыть";
        }

        private void mOpen_Click(object sender, EventArgs e)
        {
            IsFormVisible = !IsFormVisible;
        }

        private void TrayIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                IsFormVisible = !IsFormVisible;
        }

#if DEBUG
        public static void Debug_ShowProperties(object obj)
        {
            var F = new Form();
            F.Text = obj.ToString();
            var Grid = new PropertyGrid();
            Grid.Dock = DockStyle.Fill;
            Grid.SelectedObject = obj;
            F.Controls.Add(Grid);
            F.Show();
        }

        public static void Debug_ShowSubscribers()
        {
            var S = new StringBuilder();
            foreach(var Pair in NetworkScanner.GetInstance().GetSubjects())
                S.AppendLine(String.Format("{0} - {1}", Pair.Key, Pair.Value.Count));
            S.AppendLine(String.Format("ALL - {0}", NetworkScanner.GetInstance().GetAllSubjects().Count));
            MessageBox.Show(S.ToString());
        }
#endif

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
#if DEBUG
            // Ctrl+Alt+C - show properties of current page in debug mode
            if (e.Control && e.Alt && e.KeyCode == Keys.C)
            {
                Debug_ShowProperties(Pages.SelectedTab);
                e.Handled = true;
            }
            // Ctrl+Alt+S - show subscibers in debug mode
            if (e.Control && e.Alt && e.KeyCode == Keys.S)
            {
                Debug_ShowSubscribers();
                e.Handled = true;
            }
#endif
        }

        public void Items_Changed(object sender, EventArgs e)
        {
            if (Pages.SelectedIndex == -1 || Pages.SelectedTab == null)
                return;
            PanelItemList ItemList = sender as PanelItemList;
            if (ItemList == null)
                return;
            // refresh only for current page
            PanelItemList CurrentItemList = TabController.GetModel().GetItem(Pages.SelectedIndex);
            if (!ItemList.Equals(CurrentItemList))
                return;
            // get number of visible items (filtered) and number of total items
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
            if (ShowCount != TotalCount)
                StatusText = String.Format("Элементов: {0} из {1}", ShowCount, TotalCount);
            else
                StatusText = String.Format("Элементов: {0}", ShowCount);
            // update list view
            ListView LV = (ListView)Pages.SelectedTab.Controls[0];
            //LV.SelectedIndices.Clear();
            LV.VirtualListSize = ShowCount;
            /*
            if (!String.IsNullOrEmpty(ItemList.FocusedItem) && !String.IsNullOrEmpty(ItemList.FocusedItem))
            {
                LV.FocusedItem = LV.Items[ItemList.FocusedItem];
                if (LV.FocusedItem != null)
                    LV.FocusedItem.Selected = true;
            }
            */
            /*
            // update filter panel
            string Text = ItemList.FilterText;
            eFilter.TextChanged -= eFilter_TextChanged;
            eFilter.Text = Text;
            eFilter.SelectionLength = 0;
            eFilter.SelectionStart = Text.Length;
            eFilter.TextChanged += eFilter_TextChanged;
            // показываем или скрываем панель фильтра
            tsBottom.Visible = ItemList.IsFiltered;
            if (!tsBottom.Visible)
                Pages.SelectedTab.Refresh();
             */
        }

        public void lvComps_CacheVirtualItems(object sender, CacheVirtualItemsEventArgs e)
        {

        }

        public void lvComps_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            ListView LV = sender as ListView;
            if (LV == null)
                return;
            PanelItemList ItemList = PanelItemList.GetObject(LV);
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
            ListView LV = GetActiveListView();
            PanelItemList ItemList = PanelItemList.GetObject(LV);
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
            ListView LV = GetActiveListView();
            PanelItemList ItemList = PanelItemList.GetObject(LV);
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
            ListView LV = GetActiveListView();
            PanelItemList ItemList = PanelItemList.GetObject(LV);
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
            ListView LV = GetActiveListView();
            PanelItemList ItemList = PanelItemList.GetObject(LV);
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
            ListView LV = (sender as ListView);
            // Ctrl+A выделение всех элементов
            if (e.Control && e.KeyCode == Keys.A)
            {
                User32.SelectAllItems(LV);
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
            // клавишы для всех пользовательских вкладок
            if (e.KeyCode == Keys.Back)
            {
                //CompBrowser.LevelUp();
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Delete)
            {
                PanelItemList ItemList = PanelItemList.GetObject(LV);
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

        private void lvComps_ItemActivate(object sender, EventArgs e)
        {
            ListView LV = (sender as ListView);
            if (LV.FocusedItem == null)
            {
                if (LV.VirtualListSize > 0)
                    LV.FocusedItem = LV.Items[0];
                else
                    return;
            }
            logger.Info("LV_ItemActivate on {0}", LV.FocusedItem.ToString());
            /*
            if (CompBrowser.InternalStack.Count == 0)
            {
                PanelItem Comp = GetFocusedPanelItem(true, true);
                if (Comp == null)
                    return;
            }
            CompBrowser.LevelDown();
             */
        }

        private void pInfo_ShowInfo(PanelItem PItem)
        {
            ComputerPanelItem Comp = PItem as ComputerPanelItem;
            if (Comp == null)
                return;
            lInfoComp.Text = Comp.Name;
            lInfoDesc.Text = Comp.Comment;
            lInfoOS.Text = Comp.SI.Version();
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

        public void lvComps_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            ListView LV = sender as ListView;
            if (LV == null)
                return;
            logger.Info("ItemSelectionChanged. {0}, Selected: {1}, {2}", e.Item, e.IsSelected, LV.FocusedItem);
            /*
            PanelItemList ItemList = LV.GetObject();
            if (ItemList == null)
                return;
            // is current item focused?
            ItemList.Select(e.ItemIndex, e.IsSelected, e.IsSelected);
            // show current item info in top panel
            if (e.IsSelected)
            {
                string KeyName = ItemList.Keys[e.ItemIndex];
                pInfo_ShowInfo(ItemList.Get(KeyName));
            }
             */
        }

        public void lvComps_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView LV = sender as ListView;
            if (LV == null)
                return;
            logger.Info("SelectedIndexChanged. FocusedItem: {0}", LV.FocusedItem);
        }

        public void lvRecent_ItemActivate(object sender, EventArgs e)
        {
            logger.Info("lvRecent_ItemActivate on ", (sender as ListView).FocusedItem.ToString());
            PanelItem PItem = GetFocusedPanelItem(false, true);
            if (PItem != null)
                GotoFavoriteComp(PItem.Name);
        }

        void SearchPanelVisible(bool value)
        {
            tsBottom.Visible = value;
            if (!value)
                Pages.SelectedTab.Refresh();
        }

        public void UpdateFilter(ListView LV, string NewFilter, bool bVisualUpdate)
        {
            if (LV == null)
                return;
            PanelItemList ItemList = PanelItemList.GetObject(LV);
            if (ItemList == null)
                return;
            //List<string> SaveSelected = null;

            // выходим на верхний уровень
            /*
            if (!String.IsNullOrEmpty(NewFilter))
                while (CompBrowser.InternalStack.Count > 0)
                    CompBrowser.LevelUp();
             */
            
          
            //string SaveCurrent = null;
            if (bVisualUpdate)
            {
                //SaveSelected = ItemList.ListView_GetSelected(LV, false);
                // запоминаем выделенные элементы
                //if (LV.FocusedItem != null)
                  //  SaveCurrent = lvComps.FocusedItem.Text;
            }
            // меняем фильтр
            ItemList.FilterText = NewFilter;
            if (bVisualUpdate)
            {
                //TotalItems = CompBrowser.InternalItemList.Count;
                eFilter.BackColor = ItemList.Count > 0 ? Color.White : Color.FromArgb(255, 102, 102); // Firefox Color
                // восстанавливаем выделенные элементы
                //ItemList.ListView_SetSelected(LV, SaveSelected);
                //CompBrowser.SelectComputer(SaveCurrent);
                UpdateFilterPanel();
            }
            else
            {
                //LV.VirtualListSize = ItemList.FilterCount;
            }
        }

        public void UpdateFilterPanel()
        {
            /*
            ListView LV = GetActiveListView();
            PanelItemList ItemList = LV.GetObject();
            string Text = ItemList.FilterText;
            eFilter.TextChanged -= eFilter_TextChanged;
            eFilter.Text = Text;
            eFilter.SelectionLength = 0;
            eFilter.SelectionStart = Text.Length;
            eFilter.TextChanged += eFilter_TextChanged;
            // показываем или скрываем панель фильтра
            SearchPanelVisible(ItemList.IsFiltered);
            // выводим количество элементов в статус
            Items_Changed(ItemList, new EventArgs());
             */
        }

        private void eFilter_TextChanged(object sender, EventArgs e)
        {
            UpdateFilter(GetActiveListView(), (sender as TextBox).Text, true);
        }

        private void eFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                ActiveControl = GetActiveListView();
                ActiveControl.Focus();
                if (e.KeyCode == Keys.Up) SendKeys.Send("{UP}");
                if (e.KeyCode == Keys.Down) SendKeys.Send("{DOWN}");
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                ActiveControl = GetActiveListView();
            }
        }

        /// <summary>
        /// Возвращает имя выбранного компьютера, предварительно проверив пингом включен ли он.
        /// </summary>
        /// <param name="bUpdateRecent">Добавлять ли комп в закладку Активность</param>
        /// <param name="bPingAndAsk">Пинговать ли комп</param>
        /// <returns>Возвращает TComputer</returns>
        public PanelItem GetFocusedPanelItem(bool bUpdateRecent, bool bPingAndAsk)
        {
            ListView LV = GetActiveListView();
            if (LV == null)
                return null;
            logger.Info("GetFocusedPanelItem. {0}", LV.FocusedItem);
            if (LV.FocusedItem == null)
                return null;
            PanelItemList ItemList = PanelItemList.GetObject(LV);
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
            
            if (!Kernel32.Is64BitOperatingSystem())
                CmdLine = TagCmd.Replace("%ProgramFiles(x86)%", "%ProgramFiles%");
            else
                CmdLine = TagCmd;

            CmdLine = String.Format(Environment.ExpandEnvironmentVariables(CmdLine), FmtParam);
            string FName;
            string Params;
            AutorunUtils.ExplodeCmd(CmdLine, out FName, out Params);
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
            ListView LV = GetActiveListView();
            PanelItem PItem = GetFocusedPanelItem(true, true);
            if (PItem == null) 
                return;
            ComputerPanelItem Comp = null;
            int CompIndex = -1;
            if (PItem is ComputerPanelItem)
            {
                Comp = PItem as ComputerPanelItem;
                CompIndex = LV.FocusedItem.Index;
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


        public void GotoFavoriteComp(string ComputerName)
        {
            ListView LV = GetActiveListView();
            LV.BeginUpdate();
            try
            {
                LV.SelectedIndices.Clear();
                UpdateFilter(LV, "", false);
                // выходим на верхний уровень
                /*
                while (CompBrowser.InternalStack.Count > 0)
                    CompBrowser.LevelUp();
                 */
                //!!!
                //CompBrowser.InternalItemList.ListView_SelectComputer(LV, ComputerName);
                PanelItem PItem = GetFocusedPanelItem(false, false);
                if (PItem != null && PItem.Name == ComputerName)
                    lvComps_ItemActivate(LV, new EventArgs());
            }
            finally
            {
                LV.EndUpdate();
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

        public ListView GetActiveListView()
        {
            if (Pages.SelectedTab == null)
                return null;
            else
            {
                Control.ControlCollection ctrls = Pages.SelectedTab.Controls;
                return ctrls.Count > 0 ? ctrls[0] as ListView : null;
            }
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
            for (int i = 0; i < Math.Min(mComp.DropDownItems.Count, popTop.Items.Count); i++)
            {
                ToolStripItem Src = mComp.DropDownItems[i];
                ToolStripItem Dest = popTop.Items[i];
                if (Src is ToolStripMenuItem && Dest is ToolStripMenuItem)
                    (Dest as ToolStripMenuItem).ShowShortcutKeys = (Src as ToolStripMenuItem).ShowShortcutKeys;
            }
        }

        private void popTop_Opening(object sender, CancelEventArgs e)
        {
            popComps_Opened(sender, e);
            e.Cancel = !mComp.Enabled;
        }

        private void popComps_Opened(object sender, EventArgs e)
        {
            ListView LV = GetActiveListView();
            if (LV == null) return;
            mCompLargeIcons.Checked = false;
            mCompSmallIcons.Checked = false;
            mCompList.Checked = false;
            mCompDetails.Checked = false;
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

            bool bSenderIsComputer = (Pages.SelectedIndex > 0) /*|| (CompBrowser.InternalStack.Count == 0)*/;
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
            if (e.AssociatedControl is ListView)
            {
                ListView LV = (ListView)e.AssociatedControl;
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

        private void Pages_Selected(object sender, TabControlEventArgs e)
        {
            if (Pages.SelectedTab != null)
            {
                Control.ControlCollection ctrls = Pages.SelectedTab.Controls;
                if (ctrls.Count > 0)
                {
                    ActiveControl = Pages.SelectedTab.Controls[0];
                    ActiveControl.Focus();
                    //lvComps_SelectedIndexChanged(ActiveControl, new EventArgs());
                    UpdateFilterPanel();
                }
            }
        }

        private void mSettings_Click(object sender, EventArgs e)
        {
            if (ParamsForm.Instance != null)
                return;
            using (ParamsForm.Instance = new ParamsForm())
            {
                ParamsForm.Instance.ShowDialog();
            }
            ParamsForm.Instance = null;
        }

        private void mAbout_Click(object sender, EventArgs e)
        {
            if (AboutForm.Instance != null)
                return;
            using (AboutForm.Instance = new AboutForm())
            {
                AboutForm.Instance.ShowDialog();
            }
            AboutForm.Instance = null;
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        void SetControlsVisible(bool bVisible)
        {
            foreach (Control control in Controls)
                control.Visible = bVisible;
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
        }
        
  
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

        private void popPages_Opened(object sender, EventArgs e)
        {
            mSelectTab.DropDownItems.Clear();
            TabController.AddTabsToMenuItem(mSelectTab, TabController.mSelectTab_Click, false);
            mCloseTab.Enabled = TabController.CanCloseTab(Pages.SelectedIndex);
        }

        private void mSendToTab_DropDownOpened(object sender, EventArgs e)
        {
            while (mSendToTab.DropDownItems.Count > 2)
                mSendToTab.DropDownItems.RemoveAt(mSendToTab.DropDownItems.Count - 1);

            TabController.AddTabsToMenuItem(mSendToTab, TabController.mSendToSelectedTab_Click, true);
            // скрываем разделитель, если нет новых вкладок
            mAfterSendTo.Visible = mSendToTab.DropDownItems.Count > 2;
        }

        private bool m_AdminMode;

        public bool AdminMode
        {
            get { return m_AdminMode; }
            set
            {
                if (m_AdminMode != value)
                {
                    logger.Info("AdminMode is {0}", value ? "ON" : "OFF");
                    m_AdminMode = value;
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
            imgClear.Image = Resources.clear_hover;
        }

        private void imgClear_MouseLeave(object sender, EventArgs e)
        {
            imgClear.Image = Resources.clear_normal;
        }

        private void mTabParams_Click(object sender, EventArgs e)
        {
            using (TabParamsForm Form = new TabParamsForm())
            {
                TabModel M = TabController.GetModel();
                PanelItemList Info = M.GetItem(Pages.SelectedIndex);
                Form.ScanMode = Info.ScanMode;
                Form.Groups = Info.Groups;
                if (Form.ShowDialog() == DialogResult.OK)
                {
                    Info.ScanMode = Form.ScanMode;
                    Info.Groups = Form.Groups;
                    Info.UpdateSubsctiption();
                }
            }
        }

        private void mContextClose_Click(object sender, EventArgs e)
        {
            IsFormVisible = false;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void popComps_Opening(object sender, CancelEventArgs e)
        {

        }
    }
}