using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Windows.Forms;
using LanExchange.Interfaces;
using LanExchange.Plugin.WinForms.Components;
using LanExchange.Plugin.WinForms.Utils;
using LanExchange.Properties;
using LanExchange.SDK;
using System.Diagnostics.Contracts;
using LanExchange.SDK.Managers;
using LanExchange.Actions;
using LanExchange.Presentation.Interfaces;
using LanExchange.Presentation.Interfaces.Factories;

namespace LanExchange.Plugin.WinForms.Forms
{
    public sealed partial class MainForm : RunMinimizedForm, IMainView, ITranslationable
    {
        public PagesView Pages;

        private readonly ILazyThreadPool threadPool;
        private readonly IImageManager imageManager;
        private readonly IMainPresenter mainPresenter;
        private readonly IAboutPresenter aboutPresenter;
        private readonly IActionManager actionManager;
        private readonly ITranslationService translationService;
        private readonly IViewFactory viewFactory;

        private readonly IStatusPanelView statusPanel;

        public event EventHandler ViewClosed;

        [Obsolete("Should be only single depenedcy: Presenter.")]
        public MainForm(
            IMainPresenter mainPresenter,
            IAboutPresenter aboutPresenter,
            ILazyThreadPool threadPool,
            IImageManager imageManager,
            IActionManager actionManager,
            ITranslationService translationService,
            IViewFactory viewFactory
            )
        {
            Contract.Requires<ArgumentNullException>(mainPresenter != null);
            Contract.Requires<ArgumentNullException>(aboutPresenter != null);
            Contract.Requires<ArgumentNullException>(threadPool != null);
            Contract.Requires<ArgumentNullException>(imageManager != null);
            Contract.Requires<ArgumentNullException>(actionManager != null);
            Contract.Requires<ArgumentNullException>(translationService != null);
            Contract.Requires<ArgumentNullException>(viewFactory != null);

            this.aboutPresenter = aboutPresenter;
            this.threadPool = threadPool;
            this.imageManager = imageManager;
            this.actionManager = actionManager;
            this.translationService = translationService;
            this.viewFactory = viewFactory;

            // create status panel
            statusPanel = viewFactory.CreateStatusPanelView();
            Controls.Add((Control)statusPanel);

            InitializeComponent();
            this.mainPresenter = mainPresenter;
            mainPresenter.Initialize(this);

            if (translationService.RightToLeft)
            {
                RightToLeftLayout = true;
                RightToLeft = RightToLeft.Yes;
            }

            Menu = MainMenu;
        }

        public void SetupMenuLanguages()
        {
            var nameDict = translationService.GetLanguagesNames();
            if (nameDict.Count < 2)
            {
                mLanguage.Visible = false;
                return;
            }
            mLanguage.Visible = true;
            mLanguage.MenuItems.Clear();
            foreach(var pair in nameDict)
            {
                var menuItem = new MenuItem(pair.Value);
                menuItem.RadioCheck = true;
                menuItem.Tag = pair.Key;
                menuItem.Click += MenuItemOnClick;
                mLanguage.MenuItems.Add(menuItem);
            }
        }

        public void SetupPages()
        {
            Pages = (PagesView)viewFactory.GetPagesView();
            Pages.Dock = DockStyle.Fill;
            Controls.Add(Pages);
            Pages.BringToFront();
            // setup images
            imageManager.SetImagesTo(Pages.Pages);
            imageManager.SetImagesTo(statusPanel);
            // load saved pages from config
            Pages.SetupContextMenu();
        }

        private void MarkCurrentLanguage()
        {
            foreach (MenuItem menuItem in mLanguage.MenuItems)
                menuItem.Checked = menuItem.Tag.Equals(translationService.CurrentLanguage);
        }

        private void mLanguage_Popup(object sender, EventArgs e)
        {
            MarkCurrentLanguage();
        }

        private void MenuItemOnClick(object sender, EventArgs eventArgs)
        {
            var menuItem = sender as MenuItem;
            if (menuItem != null)
                App.Config.Language = (string)menuItem.Tag;
        }

        public void TranslateUI()
        {
            // translate sub-components
            TranslationUtils.TranslateComponents(Resources.ResourceManager, this, components);
            mTrayOpen_TranslateUI();
            TranslationUtils.TranslateControls(Controls);
            // refresh tab names
            //var shortcutIndex = mainPresenter.FindShortcutKeysPanelIndex();
            // TODO hide model
            //for (int index = 0; index < pagesPresenter.Count; index++ )
            //{
            //    var model = pagesPresenter.GetItem(index);
            //    pagesPresenter.UpdateTabName(index);
            //    if (index == shortcutIndex)
            //        model.AsyncRetrieveData(false);
            //}
        }

        private void mTrayOpen_TranslateUI()
        {
            mTrayOpen.Text = Visible ? Resources.MainForm_Close : Resources.mTrayOpen_Text;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Hide();
                e.Handled = true;
            }
        }

        private void tipComps_Popup(object sender, PopupEventArgs e)
        {
            var tooltip = (sender as ToolTip);
            if (tooltip == null) return;
            if (e.AssociatedControl is TabControl && e.AssociatedControl == Pages.Pages)
            {
                var tab = Pages.GetTabPageByPoint(e.AssociatedControl.PointToClient(MousePosition));
                if (tab != null)
                    tooltip.ToolTipTitle = tab.Text;
                else
                    e.Cancel = true;
                return;
            }
            tooltip.ToolTipTitle = string.Empty;
        }

        [Localizable(false)]
        private void mHelpAbout_Click(object sender, EventArgs e)
        {
            actionManager.ExecuteAction<AboutAction>();
        }
        
        private void popTray_Opening(object sender, CancelEventArgs e)
        {
            mTrayOpen_TranslateUI();
        }

        private void mOpen_Click(object sender, EventArgs e)
        {
            ToggleVisible();
        }

        private void TrayIcon_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                ToggleVisible();
        }

        private void mTrayExit_Click(object sender, EventArgs e)
        {
            ApplicationExit();
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x312;
            const int WM_QUERYENDSESSION = 0x0011;
            const int WM_ENDSESSION = 0x0016;

            switch (m.Msg)
            {
                case WM_HOTKEY:
                    if (mainPresenter.IsHotKey((short)m.WParam))
                    {
                        ToggleVisible();
                        m.Result = new IntPtr(1);
                    }
                    break;
                case WM_QUERYENDSESSION:
                    m.Result = new IntPtr(1);
                    break;
                case WM_ENDSESSION:
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            mainPresenter.SettingsSetBounds(Bounds);
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            Pages.FocusPanelView();
        }

        [Localizable(false)]
        private void mReRead_Click(object sender, EventArgs e)
        {
            actionManager.ExecuteAction<PagesReReadAction>();
        }

        private void UpdatePanelRelatedMenu()
        {
            mViewLarge.Checked = false;
            mViewSmall.Checked = false;
            mViewList.Checked = false;
            mViewDetails.Checked = false;
            var pv = Pages.ActivePanelView;
            var enabled = pv != null;
            mViewLarge.Enabled = enabled;
            mViewSmall.Enabled = enabled;
            mViewList.Enabled = enabled;
            mViewDetails.Enabled = enabled;
            if (pv != null)
                switch (pv.ViewMode)
                {
                    case PanelViewMode.LargeIcon:
                        mViewLarge.Checked = true;
                        break;
                    case PanelViewMode.SmallIcon:
                        mViewSmall.Checked = true;
                        break;
                    case PanelViewMode.List:
                        mViewList.Checked = true;
                        break;
                    case PanelViewMode.Details:
                        mViewDetails.Checked = true;
                        break;
                }
        }

        private void mView_Popup(object sender, EventArgs e)
        {
            mViewGrid.Checked = App.Config.ShowGridLines;
            UpdatePanelRelatedMenu();
        }

        private void mViewLarge_Click(object sender, EventArgs e)
        {
            var pv = Pages.ActivePanelView;
            if (pv == null) return;
            var menuItem = sender as MenuItem;
            if (menuItem == null) return;
            int tag;
            if (int.TryParse(menuItem.Tag.ToString(), out tag))
                pv.ViewMode = (PanelViewMode)tag;
        }

        private void mWebPage_Click(object sender, EventArgs e)
        {
            aboutPresenter.OpenHomeLink();
        }

        private void mHelpLangs_Click(object sender, EventArgs e)
        {
            aboutPresenter.OpenLocalizationLink();
        }

        private void mHelpBugs_Click(object sender, EventArgs e)
        {
            aboutPresenter.OpenBugTrackerWebLink();
        }

        private void mHelpFeedback_Click(object sender, EventArgs e)
        {
            aboutPresenter.OpenEmailLink();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            threadPool.Dispose();
        }

        public void SetToolTip(object control, string tipText)
        {
            if (control is Control)
                tipComps.SetToolTip((Control)control, tipText);
        }

        public void ShowStatusText(string format, params object[] args)
        {
            //lItemsCount.Text = String.Format(CultureInfo.InvariantCulture, format, args);
        }

        [Localizable(false)]
        private void mPanel_Popup(object sender, EventArgs e)
        {
            mReRead.Enabled = actionManager.IsActionEnabled<PagesReReadAction>();
            mCloseTab.Enabled = actionManager.IsActionEnabled<PagesCloseTabAction>();
            mCloseOther.Enabled = actionManager.IsActionEnabled<PagesCloseOtherAction>();
        }

        [Localizable(false)]
        private void mCloseTab_Click(object sender, EventArgs e)
        {
            actionManager.ExecuteAction<PagesCloseTabAction>();
        }

        [Localizable(false)]
        private void mCloseOther_Click(object sender, EventArgs e)
        {
            actionManager.ExecuteAction<PagesCloseOtherAction>();
        }

        private void mViewGrid_Click(object sender, EventArgs e)
        {
            App.Config.ShowGridLines = !App.Config.ShowGridLines;
        }

        public string TrayText
        {
            get { return TrayIcon.Text; }
            set { TrayIcon.Text = value; }
        }

        public bool TrayVisible
        {
            get { return TrayIcon.Visible; }
            set { TrayIcon.Visible = value; }
        }

        ///// <summary>
        ///// This params needs for omit flickering when tab's image changed.
        ///// </summary>
    //    protected override CreateParams CreateParams
    //    {
    //        get
    //        {
    //            CreateParams cp = base.CreateParams;
    //            cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
    //            return cp;
    //        }
    //    } 


        public string ShowWindowKey
        {
            get { return mTrayOpen.ShortcutKeyDisplayString; }
            set { mTrayOpen.ShortcutKeyDisplayString = value; }
        }

        public object SafeInvoke(Delegate method, params object[] args)
        {
            if (IsHandleCreated)
                return Invoke(method, args);
            return null;
        }

        private void MainForm_RightToLeftChanged(object sender, EventArgs e)
        {
            popTray.RightToLeft = RightToLeft;
            //Status.SizingGrip = RightToLeft == RightToLeft.No;
        }
    }
}