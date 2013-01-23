using System;
using System.Globalization;
using System.Windows.Forms;
using LanExchange.Model;
using LanExchange.Presenter;
using NLog;
using System.Drawing;
using System.ComponentModel;
using System.Text;
using LanExchange.Windows;
using System.Security.Permissions;
using LanExchange.Model.Panel;
using LanExchange.Model.Settings;

namespace LanExchange.UI
{
    public partial class MainForm : RunMinimizedForm
    {
        /// <summary>
        /// Logger object.
        /// </summary>
        private readonly static Logger logger = LogManager.GetCurrentClassLogger();

        public const int WAIT_FOR_KEYUP_MS = 500;

        /// <summary>
        /// ManiForm single instance.
        /// </summary>
        public static MainForm Instance;

        public PagesPresenter MainPages { get; set; }

        public MainForm()
        {
            InitializeComponent();
            Instance = this;
            BackgroundWorkers.Instance.CountChanged += BackgroundWorkers_CountChanged;
            // load settings from cfg-file
            Settings.Load();
            SetRunMinimized(Settings.Instance.RunMinimized);
            // init Pages presenter
            MainPages = Pages.GetPresenter();
            MainPages.PanelViewFocusedItemChanged += Pages_PanelViewFocusedItemChanged;
            MainPages.PanelViewFilterTextChanged += Pages_FilterTextChanged;
            MainPages.GetModel().LoadSettings();
            // here we call event for update items count in statusline
            //Pages.UpdateSelectedTab();
            // init main form
            SetupForm();
            
            // setup images
            Instance.tipComps.SetToolTip(Pages.Pages, " ");
            Pages.Pages.ImageList = LanExchangeIcons.SmallImageList;
            Status.ImageList = LanExchangeIcons.SmallImageList;
            // init network scanner
            PanelSubscription.Instance.RefreshInterval = (int)Settings.Instance.RefreshTimeInSec * 1000;
        }

        private void SetupForm()
        {
            // set mainform bounds
            var Rect = Settings.Instance.GetBounds();
            SetBounds(Rect.Left, Rect.Top, Rect.Width, Rect.Height);
            // set mainform title
            Text = String.Format("{0} {1}", Application.ProductName, AboutForm.AssemblyVersion);
            // show tray
            TrayIcon.Text = Text;
            TrayIcon.Visible = true;
            // show computer name
            lCompName.Text = SystemInformation.ComputerName;
            lCompName.ImageIndex = LanExchangeIcons.CompDefault;
            // show current user
            lUserName.Text = Settings.GetCurrentUserName();
        }

        private void BackgroundWorkers_CountChanged(object sender, EventArgs e)
        {
            lWorkers.Text = BackgroundWorkers.Instance.Count.ToString(CultureInfo.InvariantCulture);
        }

#if DEBUG
        //public static void Debug_ShowProperties(object obj)
        //{
        //    var F = new Form {Text = obj.ToString()};
        //    var Grid = new PropertyGrid {Dock = DockStyle.Fill, SelectedObject = obj};
        //    F.Controls.Add(Grid);
        //    F.Show();
        //}

        private static void Debug_ShowSubscribers()
        {
            var S = new StringBuilder();
            foreach (var Pair in PanelSubscription.Instance.GetSubjects())
                S.AppendLine(String.Format("{0} - {1}", Pair.Key.Subject, Pair.Value.Count));
            MessageBox.Show(S.ToString());
        }
#endif

        private bool m_EscDown;
        private DateTime m_EscTime;

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                PanelView pv = Pages.GetActivePanelView();
                e.Handled = true;
                if (pv == null) return;
                if (pv.Filter.IsVisible)
                    pv.Filter.SetFilterText(String.Empty);
                else
                    if (pv.GetPresenter().Objects.CurrentPath.IsEmpty)
                        Instance.Hide();
                    else
                        if (!m_EscDown)
                        {
                            m_EscTime = DateTime.UtcNow;
                            m_EscDown = true;
                        }
                        else
                        {
                            TimeSpan diff = DateTime.UtcNow - m_EscTime;
                            if (diff.TotalMilliseconds >= WAIT_FOR_KEYUP_MS)
                            {
                                Instance.Hide();
                                m_EscDown = false;
                            }
                        }
            }
#if DEBUG
            // Ctrl+Alt+S - show subscibers in debug mode
            if (e.Control && e.Alt && e.KeyCode == Keys.S)
            {
                Debug_ShowSubscribers();
                e.Handled = true;
            }
#endif
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (m_EscDown)
                {
                    TimeSpan diff = DateTime.UtcNow - m_EscTime;
                    PanelView pv = Pages.GetActivePanelView();
                    if (pv != null && !pv.GetPresenter().Objects.CurrentPath.IsEmpty)
                    {
                        if (diff.TotalMilliseconds < WAIT_FOR_KEYUP_MS)
                            pv.GetPresenter().LevelUp();
                        else
                            Instance.Hide();
                    }
                    m_EscDown = false;
                }
                e.Handled = true;
            }
        }

        private void popTop_Opened(object sender, EventArgs e)
        {
            var pv = Pages.GetActivePanelView();
            if (pv == null) return;
            for (int i = 0; i < Math.Min(pv.mComp.DropDownItems.Count, popTop.Items.Count); i++)
            {
                ToolStripItem Src = pv.mComp.DropDownItems[i];
                ToolStripItem Dest = popTop.Items[i];
                if (Src is ToolStripMenuItem && Dest is ToolStripMenuItem)
                    (Dest as ToolStripMenuItem).ShowShortcutKeys = (Src as ToolStripMenuItem).ShowShortcutKeys;
            }
        }

        private void popTop_Opening(object sender, CancelEventArgs e)
        {
            var pv = Pages.GetActivePanelView();
            if (pv == null)
            {
                e.Cancel = true;
                return;
            }
            if (popTop.Items.Count == 1)
                pv.SetupMenu(popTop);
            pv.popComps_Opening(sender, e);
            e.Cancel = !pv.mComp.Enabled;
        }

        //private void lCompName_Click(object sender, EventArgs e)
        //{
        //    // TODO uncomment mycomputer click
        //    // Open MyComputer
        //    //Process.Start("explorer.exe", "/n, /e,::{20D04FE0-3AEA-1069-A2D8-08002B30309D}");
        //    // Network
        //    //Process.Start("explorer.exe", "/n, ::{208D2C60-3AEA-1069-A2D7-08002B30309D},FERMAK");
        //    //IPanelView PV = Pages.GetActivePanelView();
        //    //if (PV != null)
        //    //    PV.GotoFavoriteComp(SystemInformation.ComputerName);
        //}

        private void tipComps_Popup(object sender, PopupEventArgs e)
        {
            //logger.Info("tipComps_Popup: {0}", e.AssociatedControl.GetType().Name);
            var tooltip = (sender as ToolTip);
            if (tooltip == null) return;
            if (e.AssociatedControl == pInfo.Picture)
            {
                tooltip.ToolTipIcon = ToolTipIcon.Info;
                tooltip.ToolTipTitle = "Легенда";
                return;
            }
            if (e.AssociatedControl is ListView)
            {
                var listview = e.AssociatedControl as ListView;
                Point P = listview.PointToClient(MousePosition);
                ListViewHitTestInfo Info = listview.HitTest(P);
                tooltip.ToolTipTitle = Info.Item != null ? Info.Item.Text : "Информация";
                return;
            }
            if (e.AssociatedControl is TabControl && e.AssociatedControl == Pages.Pages)
            {
                Point P = e.AssociatedControl.PointToClient(MousePosition);
                TabPage Tab = Pages.GetTabPageByPoint(P);
                if (Tab != null)
                    tooltip.ToolTipTitle = Tab.Text;
                else
                    e.Cancel = true;
                return;
            }
            tooltip.ToolTipTitle = "";
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

        //private void panel1_DragEnter(object sender, DragEventArgs e)
        //{
        //    if (e.Data.GetDataPresent(DataFormats.FileDrop))
        //        e.Effect = DragDropEffects.Copy;
        //}

        //private void panel1_DragDrop(object sender, DragEventArgs e)
        //{
        //    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
        //}
        
        private void lItemsCount_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point P = Status.PointToScreen(e.Location);
                popTray.Show(P);
            }
        }

        private void lWorkers_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point P = Status.PointToScreen(e.Location);
                popWorkers.Show(P);
            }
        }

        /// <summary>
        /// This event fires when focused item of PanelView has been changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pages_PanelViewFocusedItemChanged(object sender, EventArgs e)
        {
            // get focused item from current PanelView
            var pv = sender as PanelView;
            ComputerPanelItem Comp = null;
            if (pv != null) 
            {
                var PItem = pv.GetPresenter().GetFocusedPanelItem(false, false);
                // is focused changed on top level?
                if (PItem is ComputerPanelItem)
                {
                    // run/re-run timer for saving tab settings
                    timerTabSettingsSaver.Stop();
                    timerTabSettingsSaver.Start();
                }
                Comp = pv.GetPresenter().GetFocusedComputer(false);
            }
            // is focused item a computer?
            if (Comp == null)
            {
                pInfo.Picture.Image = LanExchangeIcons.LargeImageList.Images[LanExchangeIcons.CompDefault];
                pInfo.InfoComp = "";
                pInfo.InfoDesc = "";
                pInfo.InfoOS = "";
                return;
            }
            // update info panel at top of the form
            pInfo.InfoComp = Comp.Name;
            pInfo.InfoDesc = Comp.Comment;
            pInfo.InfoOS = Comp.SI.Version();
            pInfo.Picture.Image = LanExchangeIcons.LargeImageList.Images[Comp.ImageIndex];
            switch (Comp.ImageIndex)
            {
                case LanExchangeIcons.CompDefault:
                    tipComps.SetToolTip(pInfo.Picture, "Компьютер найден в результате обзора сети.");
                    break;
                case LanExchangeIcons.CompDisabled:
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

        private void Pages_FilterTextChanged(object sender, EventArgs e)
        {
            timerTabSettingsSaver.Stop();
            timerTabSettingsSaver.Start();
        }

        public void ShowStatusText(string format, params object[] args)
        {
            lItemsCount.Text = String.Format(format, args);
        }

        private void popTray_Opening(object sender, CancelEventArgs e)
        {
            mOpen.Text = Visible ? "Скрыть" : "Открыть";
        }

        //private void MainForm_Shown(object sender, EventArgs e)
        //{
        //    m_RunMinimized.Form_Shown();
        //}

        //private void MainForm_Resize(object sender, EventArgs e)
        //{
        //    m_RunMinimized.Form_Resize();
        //    logger.Info("MainForm_Resize: {0}", Width);
        //}

        //private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    logger.Info("MainForm is closing with reason {0}", e.CloseReason.ToString());
        //    if (e.CloseReason == CloseReason.None || e.CloseReason == CloseReason.UserClosing)
        //    {
        //        e.Cancel = true;
        //        IsFormVisible = false;
        //        logger.Info("Closing is canceled");
        //    }
        //}

        private void mOpen_Click(object sender, EventArgs e)
        {
            ToggleVisible();
        }

        private void TrayIcon_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                ToggleVisible();
        }

        private void mExit_Click(object sender, EventArgs e)
        {
            ApplicationExit();
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case NativeMethods.WM_QUERYENDSESSION:
                    logger.Info("WM_QUERYENDSESSION: {0}", m.LParam.ToString("X"));
                    m.Result = new IntPtr(1);
                    break;
                case NativeMethods.WM_ENDSESSION:
                    logger.Info("WM_ENDSESSION: {0}", m.LParam.ToString("X"));
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            Settings.Instance.SetBounds(Bounds);
            Settings.SaveIfModified();
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            Pages.FocusPanelView();
        }

        private void mStopWorkers_Click(object sender, EventArgs e)
        {
            BackgroundWorkers.Instance.StopAllWorkers();
        }

        private void timerTabSettingsSaver_Tick(object sender, EventArgs e)
        {
            // save tab settings and switch off timer
            MainPages.GetModel().SaveSettings();
            if (sender is Timer)
                (sender as Timer).Enabled = false;
        }

    }
}