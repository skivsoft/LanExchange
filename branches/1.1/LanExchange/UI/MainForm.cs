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
using LanExchange.Network;
using System.Security.Principal;
using LanExchange.Properties;
using LanExchange.Model;
using LanExchange.View;
using LanExchange.Presenter;

namespace LanExchange.UI
{
    public partial class MainForm : Form, IMainView
    {
        // logger object 
        private readonly static Logger logger = LogManager.GetCurrentClassLogger();
        // controller for Pages (MVC-style)
        //readonly TabController TabController;

        private readonly MainPresenter m_Presenter;
        
        public static MainForm Instance;
        
        FormWindowState  LastWindowState = FormWindowState.Normal;

        public MainForm()
        {
            InitializeComponent();
            Instance = this;
            m_Presenter = new MainPresenter(this);

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
            //TabController = new TabController(Pages);
            //mSendToNewTab.Click += new EventHandler(TabController.mSendToNewTab_Click);
            //TabController.GetModel().LoadSettings();
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
            //ToolStripItem[] MyItems = new ToolStripItem[mComp.DropDownItems.Count];
            //for (int i = 0; i < MyItems.Length; i++)
            //{
            //    var TI = mComp.DropDownItems[i];
            //    if (TI is ToolStripSeparator)
            //        MyItems[i] = new ToolStripSeparator();
            //    else
            //        if (TI is ToolStripMenuItem)
            //            MyItems[i] = (ToolStripItem)MenuUtils.Clone(TI as ToolStripMenuItem);
            //}
            //popTop.Items.Clear();
            //popTop.Items.AddRange(MyItems);
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
            //TabController.GetModel().StoreSettings();
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
            //            if (e.KeyCode == Keys.Escape)
            //            {
            //                m_Presenter.CancelCurrentFilter();
            //                e.Handled = true;
            //            }
            //#if DEBUG
            //            // Ctrl+Alt+C - show properties of current page in debug mode
            //            if (e.Control && e.Alt && e.KeyCode == Keys.C)
            //            {
            //                Debug_ShowProperties(Pages.SelectedTab);
            //                e.Handled = true;
            //            }
            //            // Ctrl+Alt+S - show subscibers in debug mode
            //            if (e.Control && e.Alt && e.KeyCode == Keys.S)
            //            {
            //                Debug_ShowSubscribers();
            //                e.Handled = true;
            //            }
            //#endif
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

        public string GetMD5FromString(string str)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(str));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));
            return sBuilder.ToString();
        }

        private void pInfo_ShowInfo(PictureBox imgInfo, PanelItem PItem)
        {
            //ComputerPanelItem Comp = PItem as ComputerPanelItem;
            //if (Comp == null)
            //    return;
            //lInfoComp.Text = Comp.Name;
            //lInfoDesc.Text = Comp.Comment;
            //lInfoOS.Text = Comp.SI.Version();
            //imgInfo.Image = ilSmall.Images[PItem.ImageIndex];
            //switch (Comp.ImageIndex)
            //{
            //    case ComputerPanelItem.imgCompDefault:
            //        this.tipComps.SetToolTip(imgInfo, "Компьютер найден в результате обзора сети.");
            //        break;
            //    case ComputerPanelItem.imgCompRed:
            //        this.tipComps.SetToolTip(imgInfo, "Компьютер не доступен посредством PING.");
            //        break;
            //    case ComputerPanelItem.imgCompGreen:
            //        this.tipComps.SetToolTip(imgInfo, "Компьютер с запущенной программой LanExchange.");
            //        break;
            //    default:
            //        this.tipComps.SetToolTip(imgInfo, "");
            //        break;
            //}
        }


        public ListView GetActiveListView()
        {
            return null;
            //if (Pages.SelectedTab == null)
            //    return null;
            //else
            //{
            //    Control.ControlCollection ctrls = Pages.SelectedTab.Controls;
            //    return ctrls.Count > 0 ? ctrls[0] as ListView : null;
            //}
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
                        LV.View = System.Windows.Forms.View.LargeIcon;
                        break;
                    case 2:
                        LV.View = System.Windows.Forms.View.Details;
                        break;
                    case 3:
                        LV.View = System.Windows.Forms.View.SmallIcon;
                        break;
                    case 4:
                        LV.View = System.Windows.Forms.View.List;
                        break;
                }
            }
            finally
            {
                LV.EndUpdate();
                LV.SuspendLayout();
            }
        }

        private void popTop_Opened(object sender, EventArgs e)
        {
            //for (int i = 0; i < Math.Min(mComp.DropDownItems.Count, popTop.Items.Count); i++)
            //{
            //    ToolStripItem Src = mComp.DropDownItems[i];
            //    ToolStripItem Dest = popTop.Items[i];
            //    if (Src is ToolStripMenuItem && Dest is ToolStripMenuItem)
            //        (Dest as ToolStripMenuItem).ShowShortcutKeys = (Src as ToolStripMenuItem).ShowShortcutKeys;
            //}
        }

        private void popTop_Opening(object sender, CancelEventArgs e)
        {
            //popComps_Opened(sender, e);
            //e.Cancel = !mComp.Enabled;
        }

        private void lCompName_Click(object sender, EventArgs e)
        {
            //GotoFavoriteComp(SystemInformation.ComputerName);
        }

        private void tipComps_Popup(object sender, PopupEventArgs e)
        {
            //if (e.AssociatedControl == imgInfo)
            //{
            //    (sender as ToolTip).ToolTipIcon = ToolTipIcon.Info;
            //    (sender as ToolTip).ToolTipTitle = "Легенда";
            //    return;
            //}
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
            //if (Pages.SelectedTab != null)
            //{
            //    Control.ControlCollection ctrls = Pages.SelectedTab.Controls;
            //    if (ctrls.Count > 0)
            //    {
            //        ActiveControl = Pages.SelectedTab.Controls[0];
            //        ActiveControl.Focus();
            //        //lvComps_SelectedIndexChanged(ActiveControl, new EventArgs());
            //        UpdateFilterPanel();
            //    }
            //}
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
            //TabController.NewTab();
        }

        private void mCloseTab_Click(object sender, EventArgs e)
        {
            //TabController.CloseTab();
        }

        private void mRenameTab_Click(object sender, EventArgs e)
        {
            //TabController.RenameTab();
        }

        private void popPages_Opened(object sender, EventArgs e)
        {
            //mSelectTab.DropDownItems.Clear();
            //TabController.AddTabsToMenuItem(mSelectTab, TabController.mSelectTab_Click, false);
            //mCloseTab.Enabled = TabController.CanCloseTab(Pages.SelectedIndex);
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
                    //popComps_Opened(popComps, new EventArgs());
                }
            }
        }

        public void mExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mTabParams_Click(object sender, EventArgs e)
        {
            //using (TabParamsForm Form = new TabParamsForm())
            //{
            //    TabModel M = TabController.GetModel();
            //    PanelItemList Info = M.GetItem(Pages.SelectedIndex);
            //    Form.ScanMode = Info.ScanMode;
            //    Form.Groups = Info.Groups;
            //    if (Form.ShowDialog() == DialogResult.OK)
            //    {
            //        Info.ScanMode = Form.ScanMode;
            //        Info.Groups = Form.Groups;
            //        Info.UpdateSubsctiption();
            //    }
            //}
        }

        private void mContextClose_Click(object sender, EventArgs e)
        {
            IsFormVisible = false;
        }

        public void Restart()
        {
            Application.Restart();
        }
    }
}