using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using AndreasJohansson.Win32.Shell;
using LanExchange.Action;
using LanExchange.Model;
using LanExchange.Model.Addon;
using LanExchange.Presenter;
using System.Drawing;
using System.ComponentModel;
using System.Security.Permissions;
using LanExchange.Model.Settings;
using LanExchange.Properties;
using LanExchange.SDK;
using LanExchange.Utils;
using Settings = LanExchange.Model.Settings.Settings;

namespace LanExchange.UI
{
    public partial class MainForm : RunMinimizedForm
    {
        public const int WAIT_FOR_KEYUP_MS = 500;

        /// <summary>
        /// ManiForm single instance.
        /// </summary>
        public static MainForm Instance;

        private readonly GlobalHotkeys m_Hotkeys;

        [Localizable(false)]
        public MainForm()
        {
            InitializeComponent();
            Instance = this;
            // load settings from cfg-file
            Settings.Instance.Changed += SettingsOnChanged;
            //Settings.Instance.Load();
            SetRunMinimized(Settings.Instance.RunMinimized);
            // init Pages presenter
            AppPresenter.MainPages = Pages.GetPresenter();
            AppPresenter.MainPages.PanelViewFocusedItemChanged += Pages_PanelViewFocusedItemChanged;
            AppPresenter.MainPages.PanelViewFilterTextChanged += Pages_FilterTextChanged;
            AppPresenter.MainPages.GetModel().LoadSettings();
            // init main form
            SetupActions();
            SetupForm();
            // setup images
            Instance.tipComps.SetToolTip(Pages.Pages, " ");
            Pages.Pages.ImageList = AppPresenter.Images.SmallImageList;
            Status.ImageList = AppPresenter.Images.SmallImageList;
            // set hotkey for activate: Ctrl+Win+X
            m_Hotkeys = new GlobalHotkeys();
            m_Hotkeys.RegisterGlobalHotKey((int)Keys.X, GlobalHotkeys.MOD_CONTROL + GlobalHotkeys.MOD_WIN, Handle);
            // set lazy events
            AppPresenter.LazyThreadPool.DataReady += OnDataReady;
#if DEBUG
            AppPresenter.LazyThreadPool.NumThreadsChanged += OnNumThreadsChanged;
#endif
        }

        [Localizable(false)]
        private void SettingsOnChanged(object sender, SettingsChangedArgs e)
        {
            if (e.Name.Equals("ShowMainMenu"))
            {
                MainMenu.Visible = (bool) e.Value;
                return;
            }
            if (e.Name.Equals("NumInfoLines"))
            {
                var value = (int) e.Value;
                if (value < 2)
                    value = 2;
                if (AppPresenter.PanelColumns != null)
                    if (value > AppPresenter.PanelColumns.MaxColumns)
                        value = AppPresenter.PanelColumns.MaxColumns;
                e.NewValue = value;
                pInfo.CountLines = value;
                AppPresenter.MainPages.PV_FocusedItemChanged(Pages.ActivePanelView, EventArgs.Empty);
                return;
            }
        }

        #region Global actions
        private IAction m_AboutAction;
        private IAction m_ReReadAction;
        private IAction m_ShortcutKeysAction;

        public void SetupActions()
        {
            m_AboutAction = new AboutAction();
            m_ReReadAction = new ReReadAction();
            m_ShortcutKeysAction = new ShortcutKeysAction();
        }
        #endregion

        private int GetDefaultWidth()
        {
            const double phi2 = 0.6180339887498949;
            return (int) (Screen.PrimaryScreen.WorkingArea.Width*phi2*phi2);
        }

        public Rectangle SettingsGetBounds()
        {
            // correct width and height
            bool BoundsIsNotSet = Settings.Instance.MainFormWidth == 0;
            Rectangle WorkingArea;
            if (BoundsIsNotSet)
                WorkingArea = Screen.PrimaryScreen.WorkingArea;
            else
                WorkingArea = Screen.GetWorkingArea(new Point(Settings.Instance.MainFormX + Settings.Instance.MainFormWidth/2, 0));
            var rect = new Rectangle();
            rect.X = Settings.Instance.MainFormX;
            rect.Y = WorkingArea.Top;
            rect.Width = Math.Min(Math.Max(GetDefaultWidth(), Settings.Instance.MainFormWidth), WorkingArea.Width);
            rect.Height = WorkingArea.Height;
            // determination side to snap right or left
            int CenterX = (rect.Left + rect.Right) >> 1;
            int WorkingAreaCenterX = (WorkingArea.Left + WorkingArea.Right) >> 1;
            if (BoundsIsNotSet || CenterX >= WorkingAreaCenterX)
                // snap to right side
                rect.X = WorkingArea.Right - rect.Width;
            else
                // snap to left side
                rect.X -= rect.Left - WorkingArea.Left;
            return rect;
        }

        public void SettingsSetBounds(Rectangle rect)
        {
            Rectangle WorkingArea = Screen.GetWorkingArea(rect);
            // shift rect into working area
            if (rect.Left < WorkingArea.Left) rect.X = WorkingArea.Left;
            if (rect.Top < WorkingArea.Top) rect.Y = WorkingArea.Top;
            if (rect.Right > WorkingArea.Right) rect.X -= rect.Right - WorkingArea.Right;
            if (rect.Bottom > WorkingArea.Bottom) rect.Y -= rect.Bottom - WorkingArea.Bottom;
            // determination side to snap right or left
            int CenterX = (rect.Left + rect.Right) >> 1;
            int WorkingAreaCenterX = (WorkingArea.Left + WorkingArea.Right) >> 1;
            if (CenterX >= WorkingAreaCenterX)
                // snap to right side
                rect.X = WorkingArea.Right - rect.Width;
            else
                // snap to left side
                rect.X -= rect.Left - WorkingArea.Left;
            // set properties
            if (rect.Left != Settings.Instance.MainFormX || rect.Width != Settings.Instance.MainFormWidth)
            {
                Settings.Instance.MainFormX = rect.Left;
                Settings.Instance.MainFormWidth = rect.Width;
            }
        }

        [Localizable(false)]
        private void SetupForm()
        {
            // set mainform bounds
            var Rect = SettingsGetBounds();
            SetBounds(Rect.Left, Rect.Top, Rect.Width, Rect.Height);
            // set mainform title
            Text = String.Format("{0} {1}", AboutInfo.Product, AboutInfo.VersionShort);
            // show tray
            TrayIcon.Text = Text;
            TrayIcon.Visible = true;
            // show computer name
            lCompName.Text = SystemInformation.ComputerName;
            lCompName.ImageIndex = AppPresenter.Images.IndexOf(PanelImageNames.ComputerNormal);
            // show current user
            lUserName.Text = Settings.GetCurrentUserName();
            lUserName.ImageIndex = AppPresenter.Images.IndexOf(PanelImageNames.UserNormal);
        }

        private bool m_EscDown;
        private DateTime m_EscTime;

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                var pv = Pages.ActivePanelView;
                e.Handled = true;
                if (pv != null && pv.Filter.IsVisible)
                    pv.Filter.SetFilterText(string.Empty);
                else
                {
                    var parent = pv == null || pv.Presenter.Objects.CurrentPath.IsEmptyOrRoot
                                     ? null
                                     : pv.Presenter.Objects.CurrentPath.Peek();
                    if ((parent == null) || AppPresenter.PanelItemTypes.DefaultRoots.Contains(parent))
                        Instance.Hide();
                    else if (!m_EscDown)
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
            }
            // F9 - Show/Hide main menu
            if (e.KeyCode == Keys.F9)
            {
                Settings.Instance.SetBoolValue("ShowMainMenu", !Settings.Instance.GetBoolValue("ShowMainMenu"));
                e.Handled = true;
            }
            // Ctrl+Up/Ctrl+Down - change number of info lines
            if (e.Control && (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down))
            {
                int value = Settings.Instance.GetIntValue("NumInfoLines");
                if (e.KeyCode == Keys.Down)
                    Settings.Instance.SetIntValue("NumInfoLines", value + 1);
                else
                    Settings.Instance.SetIntValue("NumInfoLines", value - 1);
                e.Handled = true;
            }
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (m_EscDown)
                {
                    TimeSpan diff = DateTime.UtcNow - m_EscTime;
                    var pv = Pages.ActivePanelView;
                    var presenter = pv.Presenter;
                    if (pv != null && !presenter.Objects.CurrentPath.IsEmptyOrRoot)
                    {
                        if (diff.TotalMilliseconds < WAIT_FOR_KEYUP_MS)
                            presenter.CommandLevelUp();
                        else
                            Instance.Hide();
                    }
                    m_EscDown = false;
                }
                e.Handled = true;
            }
        }

        private void popTop_Opening(object sender, CancelEventArgs e)
        {
            var pv = Pages.ActivePanelView as PanelView;
            if (pv == null)
            {
                e.Cancel = true;
                return;
            }
            var panelItem = pv.Presenter.GetFocusedPanelItem(false, true);
            if (panelItem == null)
            {
                e.Cancel = true;
                return;
            }
            e.Cancel = !AddonManager.Instance.BuildMenuForPanelItemType(popTop, panelItem.GetType().Name);
        }

        private void tipComps_Popup(object sender, PopupEventArgs e)
        {
            //logger.Info("tipComps_Popup: {0}", e.AssociatedControl.GetType().Name);
            var tooltip = (sender as ToolTip);
            if (tooltip == null) return;
            if (e.AssociatedControl == pInfo.Picture)
            {
                tooltip.ToolTipIcon = ToolTipIcon.Info;
                tooltip.ToolTipTitle = Resources.MainForm_Legend;
                return;
            }
            if (e.AssociatedControl is ListView)
            {
                var listview = e.AssociatedControl as ListView;
                Point P = listview.PointToClient(MousePosition);
                ListViewHitTestInfo Info = listview.HitTest(P);
                tooltip.ToolTipTitle = Info.Item != null ? Info.Item.Text : Resources.MainForm_Information;
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

        private void mAbout_Click(object sender, EventArgs e)
        {
            m_AboutAction.Execute();
        }
        
        private void lItemsCount_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var point = Status.PointToScreen(e.Location);
                popTray.Show(point);
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
            if (pv == null) return;
            var panelItem = pv.Presenter.GetFocusedPanelItem(false, true);
            // check if parent item more informative than current panel item
            while (panelItem != null &&
                   !AppPresenter.PanelItemTypes.DefaultRoots.Contains(panelItem) &&
                   !AppPresenter.PanelItemTypes.DefaultRoots.Contains(panelItem.Parent))
                panelItem = panelItem.Parent;
            if (panelItem == null) return;
            // update info panel at top of the form
            pInfo.Picture.Image = AppPresenter.Images.GetLargeImage(panelItem.ImageName);
            tipComps.SetToolTip(pInfo.Picture, panelItem.ImageLegendText);
            var helper = new PanelItemsCopyHelper(null);
            helper.CurrentItem = panelItem;
            int index = 0;
            foreach (var column in helper.Columns)
            {
                pInfo.SetLine(index, helper.GetColumnValue(column.Index));
                ++index;
                if (index >= pInfo.CountLines) break;
            }
            for (int i = index; i < pInfo.CountLines; i++)
                pInfo.SetLine(i, string.Empty);
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
            mOpen.Text = Visible ? Resources.MainForm_Close : Resources.MainForm_Open;
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
                case GlobalHotkeys.WM_HOTKEY:
                    if ((short)m.WParam == m_Hotkeys.HotkeyID)
                    {
                        ToggleVisible();
                    }
                    break;
                case NativeMethods.WM_QUERYENDSESSION:
                    m.Result = new IntPtr(1);
                    break;
                case NativeMethods.WM_ENDSESSION:
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            SettingsSetBounds(Bounds);
            //Settings.SaveIfModified();
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            Pages.FocusPanelView();
        }

        private void timerTabSettingsSaver_Tick(object sender, EventArgs e)
        {
            // save tab settings and switch off timer
            AppPresenter.MainPages.GetModel().SaveSettings();
            if (sender is System.Windows.Forms.Timer)
                (sender as System.Windows.Forms.Timer).Enabled = false;
        }

        private void rereadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_ReReadAction.Execute();
        }

        private void lCompName_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var scm = new ShellContextMenu();
                scm.ShowContextMenuForCSIDL(Handle, ShellAPI.CSIDL.DRIVES, Cursor.Position);
            }
        }

        private void Status_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                Process.Start("explorer.exe", "/n,::{20D04FE0-3AEA-1069-A2D8-08002B30309D}");
        }

        private void UpdateViewTypeMenu()
        {
            mPanelLarge.Checked = false;
            mPanelSmall.Checked = false;
            mPanelList.Checked = false;
            mPanelDetails.Checked = false;
            var pv = Pages.ActivePanelView;
            var bEnabled = pv != null;
            mPanelLarge.Enabled = bEnabled;
            mPanelSmall.Enabled = bEnabled;
            mPanelList.Enabled = bEnabled;
            mPanelDetails.Enabled = bEnabled;
            if (pv != null)
                switch (pv.ViewMode)
                {
                    case PanelViewMode.LargeIcon:
                        mPanelLarge.Checked = true;
                        break;
                    case PanelViewMode.SmallIcon:
                        mPanelSmall.Checked = true;
                        break;
                    case PanelViewMode.List:
                        mPanelList.Checked = true;
                        break;
                    case PanelViewMode.Details:
                        mPanelDetails.Checked = true;
                        break;
                }
        }

        private void mPanel_DropDownOpening(object sender, EventArgs e)
        {
            UpdateViewTypeMenu();
        }

        private void mPanelLarge_Click(object sender, EventArgs e)
        {
            var pv = Pages.ActivePanelView;
            if (pv == null) return;
            var menuItem = sender as ToolStripMenuItem;
            if (menuItem == null) return;
            int tag;
            if (int.TryParse(menuItem.Tag.ToString(), out tag))
                pv.ViewMode = (PanelViewMode)tag;
        }

        private void mWebPage_Click(object sender, EventArgs e)
        {
            var presenter = new AboutPresenter(null);
            presenter.OpenWebLink();
        }

        private void mHelpKeys_Click(object sender, EventArgs e)
        {
            m_ShortcutKeysAction.Execute();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            AppPresenter.LazyThreadPool.Dispose();
        }

        public void OnDataReady(object sender, DataReadyArgs args)
        {
            BeginInvoke(new WaitCallback(MainForm_RefreshItem), new object[1] { args.Item });
        }

#if DEBUG
        public void OnNumThreadsChanged(object sender, EventArgs eventArgs)
        {
            BeginInvoke(new WaitCallback(MainForm_RefreshNumThreads), new object[1] { sender });
        }
#endif

        private void MainForm_RefreshItem(object item)
        {
            var pv = Pages.ActivePanelView;
            if (pv != null)
            {
                var index = pv.Presenter.Objects.IndexOf(item as PanelItemBase);
                if (index >= 0)
                    pv.RedrawItem(index);
            }
        }

 #if DEBUG
        [Localizable(false)]
        private void MainForm_RefreshNumThreads(object sender)
        {
            int count = 0;
            foreach (var column in AppPresenter.PanelColumns.EnumAllColumns())
                if (column.Callback != null)
                    count += column.LazyDict.Count;
            Text = String.Format("{0} {1} [Threads: {2}, Dict: {3}]", AboutInfo.Product, AboutInfo.VersionFull, AppPresenter.LazyThreadPool.NumThreads, count);
        }
 #endif

        private void lCompName_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                var label = (sender as ToolStripStatusLabel);
                if (label != null)
                {
                    var obj = new DataObject();
                    obj.SetText(label.Text, TextDataFormat.UnicodeText);
                    Status.DoDragDrop(obj, DragDropEffects.Copy);
                }
            }
        }

        private void mPanelNewTab_Click(object sender, EventArgs e)
        {
            AppPresenter.MainPages.CommandNewTab();
        }

        private void pInfo_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.None;
            if (!e.Data.GetDataPresent(typeof(PanelItemsCopyHelper))) return;
            var helper = (PanelItemsCopyHelper) e.Data.GetData(typeof (PanelItemsCopyHelper));
            if (helper != null && helper.IndexesCount > 0)
                e.Effect = DragDropEffects.Copy;
        }

        private void pInfo_DragDrop(object sender, DragEventArgs e)
        {
            AppPresenter.MainPages.CommandSendToNewTab();
        }
    }
}