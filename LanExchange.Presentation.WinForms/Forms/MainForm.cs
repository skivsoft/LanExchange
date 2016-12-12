using System;
using System.Security.Permissions;
using System.Windows.Forms;
using LanExchange.Presentation.Interfaces;
using LanExchange.Presentation.WinForms.Helpers;
using LanExchange.Presentation.WinForms.Properties;
using LanExchange.Presentation.Interfaces.Menu;
using LanExchange.Presentation.WinForms.Visitors;

namespace LanExchange.Presentation.WinForms.Forms
{
    internal sealed partial class MainForm : Form, IMainView
    {
        private readonly IMainPresenter mainPresenter;

        public event EventHandler ViewClosed;

        public MainForm(IMainPresenter mainPresenter)
        {
            if (mainPresenter != null) throw new ArgumentNullException(nameof(mainPresenter));

            InitializeComponent();
            this.mainPresenter = mainPresenter;
            mainPresenter.Initialize(this);
        }

        private void MarkCurrentLanguage()
        {
            //foreach (MenuItem menuItem in mLanguage.MenuItems)
            //    menuItem.Checked = menuItem.Tag.Equals(translationService.CurrentLanguage);
        }

        private void mLanguage_Popup(object sender, EventArgs e)
        {
            MarkCurrentLanguage();
        }

        private void MenuItemOnClick(object sender, EventArgs eventArgs)
        {
            var menuItem = sender as MenuItem;
            if (menuItem != null)
                Settings.Default.Language = (string)menuItem.Tag;
        }

        public void TranslateUI()
        {
            // translate sub-components
            TranslationHelper.TranslateComponents(Resources.ResourceManager, this, components);
            TranslationHelper.TranslateControls(Controls);
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

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                mainPresenter.PerformEscapeKeyDown();
                e.Handled = true;
            }
            if (e.KeyCode == Keys.F10 || e.KeyCode == Keys.Menu)
            {
                mainPresenter.PerformMenuKeyDown();
            }
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                mainPresenter.PerformEscapeKeyUp();
                e.Handled = true;
            }
            if (e.KeyCode == Keys.F10 || e.KeyCode == Keys.Menu)
            {
                e.Handled = mainPresenter.PerformMenuKeyUp();
            }
        }

        private void tipComps_Popup(object sender, PopupEventArgs e)
        {
            //var tooltip = (sender as ToolTip);
            //if (tooltip == null) return;
            //if (e.AssociatedControl is TabControl && e.AssociatedControl == Pages.Pages)
            //{
            //    var tab = Pages.GetTabPageByPoint(e.AssociatedControl.PointToClient(MousePosition));
            //    if (tab != null)
            //        tooltip.ToolTipTitle = tab.Text;
            //    else
            //        e.Cancel = true;
            //    return;
            //}
            //tooltip.ToolTipTitle = string.Empty;
        }
     
        private void TrayIcon_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                mainPresenter.DoToggleVisible();
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
                        mainPresenter.DoToggleVisible();
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

        private void mReRead_Click(object sender, EventArgs e)
        {
            mainPresenter.DoPagesReRead();
        }

        private void UpdatePanelRelatedMenu()
        {
            //mViewLarge.Checked = false;
            //mViewSmall.Checked = false;
            //mViewList.Checked = false;
            //mViewDetails.Checked = false;
            //var pv = Pages.ActivePanelView;
            //var enabled = pv != null;
            //mViewLarge.Enabled = enabled;
            //mViewSmall.Enabled = enabled;
            //mViewList.Enabled = enabled;
            //mViewDetails.Enabled = enabled;
            //if (pv != null)
            //    switch (pv.ViewMode)
            //    {
            //        case PanelViewMode.LargeIcon:
            //            mViewLarge.Checked = true;
            //            break;
            //        case PanelViewMode.SmallIcon:
            //            mViewSmall.Checked = true;
            //            break;
            //        case PanelViewMode.List:
            //            mViewList.Checked = true;
            //            break;
            //        case PanelViewMode.Details:
            //            mViewDetails.Checked = true;
            //            break;
            //    }
        }

        private void mView_Popup(object sender, EventArgs e)
        {
            UpdatePanelRelatedMenu();
        }

        private void mView_Click(object sender, EventArgs e)
        {
            var menu = (Menu)sender;
            mainPresenter.DoChangeView((PanelViewMode)menu.Tag);
        }

        private void mWebPage_Click(object sender, EventArgs e)
        {
            mainPresenter.OpenHomeLink();
        }

        private void mHelpLangs_Click(object sender, EventArgs e)
        {
            mainPresenter.OpenLocalizationLink();
        }

        private void mHelpBugs_Click(object sender, EventArgs e)
        {
            mainPresenter.OpenBugTrackerWebLink();
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

        private void mPanel_Popup(object sender, EventArgs e)
        {
            // TODO remove commandManager
            //mReRead.Enabled = commandManager.IsCommandEnabled<PagesReReadCommand>();
            //mCloseTab.Enabled = commandManager.IsCommandEnabled<PagesCloseTabCommand>();
            //mCloseOther.Enabled = commandManager.IsCommandEnabled<PagesCloseOtherCommand>();
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


        public object SafeInvoke(Delegate method, params object[] args)
        {
            if (IsHandleCreated)
                return Invoke(method, args);
            return null;
        }

        public void AddView(IView view, ViewDockStyle dockStyle)
        {
            ((Control)view).Dock = (DockStyle)dockStyle;
            Controls.Add((Control)view);
        }

        MainMenu mainMenu;

        public void InitializeMainMenu(IMenuElement menu)
        {
            mainMenu = new SystemMenuBuilder().BuildMainMenu(menu);
            Menu = mainMenu;
        }

        public void InitializeTrayMenu(IMenuElement menu)
        {
            TrayIcon.ContextMenu = new SystemMenuBuilder().BuildContextMenu(menu);
        }

        public bool MenuVisible
        {
            get { return Menu != null; }
            set { Menu = value ? mainMenu : null; }
        }

        public bool RightToLeftValue { get; set; }

    }
}