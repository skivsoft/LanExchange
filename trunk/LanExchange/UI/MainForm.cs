using System;
using System.Windows.Forms;
using LanExchange.View;
using LanExchange.Model;
using LanExchange.Presenter;
using NLog;
using System.Drawing;
using System.Reflection;
using System.ComponentModel;
using System.Text;
using System.Security.Cryptography;

namespace LanExchange.UI
{
    public partial class MainForm : Form, IMainView
    {
        // logger object 
        private readonly static Logger logger = LogManager.GetCurrentClassLogger();
        // controller for Pages (MVC-style)

        private static MainPresenter m_Presenter;
        
        public static MainForm Instance;
        
        FormWindowState  LastWindowState = FormWindowState.Normal;

        public MainForm()
        {
            InitializeComponent();
            Instance = this;
            // load settings from cfg-file
            Settings.LoadSettings();
            // init MainForm presenter
            m_Presenter = new MainPresenter(this);
            m_Presenter.Pages = Pages.GetPresenter();
            // load pages from cfg-file
            m_Presenter.Pages.GetModel().LoadSettings();
            // here we call event for update items count in statusline
            Pages.UpdateSelectedTab();

            //mSendToNewTab.Click += new EventHandler(TabController.mSendToNewTab_Click);
            
            // init main form
            SetupForm();
            SetupMenu();
            // setup images
            MainForm.Instance.tipComps.SetToolTip(Pages.Pages, " ");
            Pages.Pages.ImageList = LanExchangeIcons.SmallImageList;
            Status.ImageList = LanExchangeIcons.SmallImageList;
            // init network scanner
#if DEBUG
            ServerListSubscription.Instance.RefreshInterval = 10 * 1000; // refresh every 5 sec in debug mode
#else
            ServerListSubscription.Instance.RefreshInterval = (int)Settings.Instance.RefreshTimeInSec * 1000;
#endif
            // set admin mode
            AdminMode = Settings.Instance.AdvancedMode;
        }

        public static void OnApplicationExit(object sender, EventArgs e)
        {
            Settings.SaveSettings();
            m_Presenter.Pages.GetModel().StoreSettings();
        }

        private void SetupRunMinimized()
        {
            if (Settings.Instance.RunMinimized)
            {
                WindowState = FormWindowState.Minimized;
                ShowInTaskbar = false;
                Visible = false;
            }
        }

        private void SetupForm()
        {
            // form pos at right
            var Rect = new Rectangle();
            Rect.Size = new Size(450, Screen.PrimaryScreen.WorkingArea.Height);
            Rect.Location = new Point(Screen.PrimaryScreen.WorkingArea.Left + (Screen.PrimaryScreen.WorkingArea.Width - Rect.Width),
                                      Screen.PrimaryScreen.WorkingArea.Top + (Screen.PrimaryScreen.WorkingArea.Height - Rect.Height));
            SetBounds(Rect.X, Rect.Y, Rect.Width, Rect.Height);
            // set mainform title
            var Ver = Assembly.GetExecutingAssembly().GetName().Version;
            Text = String.Format("{0} {1}.{2}", Application.ProductName, Ver.Major, Ver.Minor);
            // show tray
            TrayIcon.Text = Text;
            TrayIcon.Visible = true;
            // show computer name
            lCompName.Text = SystemInformation.ComputerName;
            lCompName.ImageIndex = LanExchangeIcons.imgCompDefault;
                // show current user
            lUserName.Text = Settings.GetCurrentUserName();
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
            foreach (var Pair in ServerListSubscription.Instance.GetSubjects())
                S.AppendLine(String.Format("{0} - {1}", Pair.Key, Pair.Value.Count));
            MessageBox.Show(S.ToString());
        }
#endif

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
#if DEBUG
            // Ctrl+Alt+S - show subscibers in debug mode
            if (e.Control && e.Alt && e.KeyCode == Keys.S)
            {
                Debug_ShowSubscribers();
                e.Handled = true;
            }
#endif
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
            PanelView PV = Pages.GetActivePanelView();
            if (PV != null)
            {
                PV.popComps_Opening(sender, e);
                e.Cancel = !PV.mComp.Enabled;
            }
        }

        private void lCompName_Click(object sender, EventArgs e)
        {
            // Open MyComputer
            //Process.Start("explorer.exe", "/n, /e,::{20D04FE0-3AEA-1069-A2D8-08002B30309D}");
            // Network
            //Process.Start("explorer.exe", "/n, ::{208D2C60-3AEA-1069-A2D7-08002B30309D},FERMAK");
            PanelView PV = Pages.GetActivePanelView();
            if (PV != null)
                PV.GotoFavoriteComp(SystemInformation.ComputerName);
        }

        private void tipComps_Popup(object sender, PopupEventArgs e)
        {
            logger.Info("tipComps_Popup: {0}", e.AssociatedControl.GetType().Name);
            if (e.AssociatedControl == pInfo.Picture)
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
            if (e.AssociatedControl is TabControl && e.AssociatedControl == Pages.Pages)
            {
                Point P = e.AssociatedControl.PointToClient(Control.MousePosition);
                TabPage Tab = Pages.GetTabPageByPoint(P);
                if (Tab != null)
                    (sender as ToolTip).ToolTipTitle = Tab.Text;
                return;
            }
            (sender as ToolTip).ToolTipTitle = "";
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

        public void Restart()
        {
            Application.Restart();
        }

        private void lItemsCount_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                Point P = Status.PointToScreen(new Point(e.X, e.Y));
                popTray.Show(P);
            }
        }

        /// <summary>
        /// This event fires when focused item of PanelView has been changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PV_FocusedItemChanged(object sender, EventArgs e)
        {
            // get focused item from current PanelView
            PanelItem PItem = (sender as PanelView).GetPresenter().GetFocusedPanelItem(false, false);
            if (PItem == null)
                return;
            // is focused item a computer?
            ComputerPanelItem Comp = PItem as ComputerPanelItem;
            if (Comp == null)
                return;
            // update info panel at top of the form
            pInfo.InfoComp = Comp.Name;
            pInfo.InfoDesc = Comp.Comment;
            pInfo.InfoOS = Comp.SI.Version();
            pInfo.Picture.Image = LanExchangeIcons.ExtraLargeImageList.Images[PItem.ImageIndex];
            switch (Comp.ImageIndex)
            {
                case LanExchangeIcons.imgCompDefault:
                    tipComps.SetToolTip(pInfo.Picture, "Компьютер найден в результате обзора сети.");
                    break;
                case LanExchangeIcons.imgCompDisabled:
                    tipComps.SetToolTip(pInfo.Picture, "Компьютер не доступен посредством PING.");
                    break;
                /*
                case LanExchangeIcons.imgCompGreen:
                    tipComps.SetToolTip(pInfo.Picture, "Компьютер с запущенной программой LanExchange.");
                    break;
                 */
                default:
                    tipComps.SetToolTip(pInfo.Picture, "");
                    break;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            SetupRunMinimized();
        }


        public void ShowStatusText(string format, params object[] args)
        {
            lItemsCount.Text = String.Format(format, args);
        }
    }
}