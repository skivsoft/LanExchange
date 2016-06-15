using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LanExchange.Presentation.Interfaces;
using LanExchange.Presentation.WinForms.Helpers;
using LanExchange.Presentation.WinForms.Properties;

namespace LanExchange.Presentation.WinForms.Controls
{
    // TODO make it internal
    public sealed partial class PagesView : UserControl, IPagesView, ITranslationable
    {
        private readonly IPagesPresenter presenter;
        private readonly IPanelItemFactoryManager factoryManager;
        private readonly IImageManager imageManager;
        private readonly IPanelFillerManager panelFillers;
        private readonly IModelFactory modelFactory;
        private readonly IViewFactory viewFactory;

        private int popupSelectedIndex = -1;

        /// <summary>
        /// Mouse right click was pressed.
        /// </summary>
        private bool isMouseDown;
        /// <summary>
        /// Popup menu was opened.
        /// </summary>
        private bool isOpened;
        /// <summary>
        /// Menu item in popup menu was clicked.
        /// </summary>
        private bool isClicked;

        [Obsolete("Should be only single depenedcy: Presenter.")]
        public PagesView(
            IPagesPresenter presenter, 
            IPanelItemFactoryManager factoryManager, 
            IImageManager imageManager,
            IPanelFillerManager panelFillers,
            ICommandManager commandManager,
            IModelFactory modelFactory,
            IViewFactory viewFactory)
        {
            Contract.Requires<ArgumentNullException>(presenter != null);
            Contract.Requires<ArgumentNullException>(factoryManager != null);
            Contract.Requires<ArgumentNullException>(imageManager != null);
            Contract.Requires<ArgumentNullException>(panelFillers != null);
            Contract.Requires<ArgumentNullException>(modelFactory != null);
            Contract.Requires<ArgumentNullException>(viewFactory != null);

            InitializeComponent();
            this.presenter = presenter;
            presenter.Initialize(this);

            this.factoryManager = factoryManager;
            this.imageManager = imageManager;
            this.panelFillers = panelFillers;
            this.modelFactory = modelFactory;
            this.viewFactory = viewFactory;

            this.imageManager.SetImagesTo(popPages);
        }

        public void TranslateUI()
        {
            TranslationHelper.TranslateComponents(Resources.ResourceManager, this, components);
            SetupContextMenu(popPages.Items);
            TranslationHelper.TranslateControls(Controls);
        }

        public void SetupContextMenu()
        {
            SetupContextMenu(popPages.Items);
        }

        private void SetupContextMenu(ToolStripItemCollection items)
        {
            while (items[0] != mReRead)
            {
                var menuItem = items[0];
                items.RemoveAt(0);
                menuItem.Dispose();
            }
            factoryManager.CreateDefaultRoots();
            var index = 0;
            foreach (var root in factoryManager.DefaultRoots)
            {
                var menuItem = new ToolStripMenuItem(string.Format(Resources.PagesView_OpenTab, root.Name));
                menuItem.Tag = root;
                menuItem.ImageIndex = imageManager.IndexOf(root.ImageName);
                menuItem.Click += PluginOnClick;
                items.Insert(index, menuItem);
                index++;
            }
            if (index > 0)
            {
                items.Insert(index, new ToolStripSeparator());
            }
        }

        private void PluginOnClick(object sender, EventArgs eventArgs)
        {
            var menuItem = sender as ToolStripMenuItem;
            if (menuItem == null) return;
            var root = menuItem.Tag as PanelItemBase;
            if (root == null) return;
            if (!presenter.SelectTabByName(root.Name))
            {
                var info = modelFactory.CreatePanelModel();
                info.SetDefaultRoot(root);
                var type = panelFillers.GetFillType(root);
                info.DataType = type != null ? Name : string.Empty;
                //TODO hide model
                //presenter.AddTab(info);
            }
        }


        public int TabPagesCount
        {
            get { return Pages.TabPages.Count; }
        }


        public void SetTabText(int index, string title)
        {
            if (index < 0 || index > Pages.TabCount - 1)
                return;
            Pages.TabPages[index].Text = title;
        }

        public void SetTabImage(int index, int imageIndex)
        {
            var tabPage = Pages.TabPages[index];
            tabPage.ImageIndex = imageIndex;
            //tabPage.Invalidate(true);
            //tabPage.Refresh();
        }

        public void RemoveTabAt(int index)
        {
            Pages.TabPages.RemoveAt(index);
        }

        private int GetTabIndexByPoint(Point point)
        {
            for (int i = 0; i < Pages.TabPages.Count; i++)
                if (Pages.GetTabRect(i).Contains(point))
                    return i;
            return -1;
        }

        public TabPage GetTabPageByPoint(Point point)
        {
            for (int i = 0; i < Pages.TabPages.Count; i++)
                if (Pages.GetTabRect(i).Contains(point))
                    return Pages.TabPages[i];
            return null;
        }

        private void Pages_Selected(object sender, TabControlEventArgs e)
        {
            presenter.SelectedIndex = e.TabPageIndex;
        }

        public IPanelView ActivePanelView
        {
            get
            {
                if (Pages.TabCount == 0 || Pages.SelectedTab == null)
                    return null;
                ControlCollection ctrls = Pages.SelectedTab.Controls;
                return ctrls.Count > 0 ? ctrls[0] as IPanelView : null;
            }
        }

        public void SetTabToolTip(int index, string value)
        {
            if (index >= 0 && index <= Pages.TabCount-1)
                Pages.TabPages[index].ToolTipText = value;
        }

        public void FocusPanelView()
        {
            var pv = ActivePanelView;
            if (pv != null && ActiveControl != pv)
            {
                ActiveControl = pv as Control;
                pv.Presenter.UpdateItemsAndStatus();
                pv.FocusListView();
            }
        }

        private void Pages_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                popupSelectedIndex = GetTabIndexByPoint(e.Location);
                isMouseDown = true;
                isOpened = false;
                isClicked = false;
            }
        }

        private void popPages_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            isClicked = isOpened;
        }

        [Localizable(false)]
        private void popPages_Opened(object sender, EventArgs e)
        {
            if (isMouseDown && !isOpened)
            {
                isOpened = true;
                isMouseDown = false;
            }
            else
                isOpened = false;
            // TODO seems command in wrong place
            //mReRead.Enabled = commandManager.IsCommandEnabled<PagesReReadCommand>();
            //mCloseTab.Enabled = commandManager.IsCommandEnabled<PagesCloseTabCommand>();
            //mCloseOther.Enabled = commandManager.IsCommandEnabled<PagesCloseOtherCommand>();
        }

        private void popPages_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            isOpened = false;
        }

        public int PopupSelectedIndex
        {
            get
            {
                //logger.Info("MouseDown={0}, Opened={1}, Clicked={2}, Closed={3}", bMouseDown, bOpened, bClicked, bClosed);
                if (isClicked && !isOpened)
                {
                    isMouseDown = false;
                    isOpened = false;
                    isClicked = false;
                    if (popupSelectedIndex < 0 || popupSelectedIndex > Pages.TabCount - 1)
                        return Pages.SelectedIndex;
                    return popupSelectedIndex;
                }
                return Pages.SelectedIndex;
            }
        }

        public int SelectedIndex
        {
            get { return Pages.SelectedIndex; }
            set
            {
                Pages.SelectedIndex = value;
                presenter.SelectedIndex = value;
            }
        }

        [Localizable(false)]
        public IPanelView CreatePanelView(IPanelModel info)
        {
            var panelView = (PanelView) viewFactory.CreatePanelView();
            var listView = panelView.Controls[0] as ListView;
            if (listView != null)
            {
                imageManager.SetImagesTo(listView);
                listView.View = (View) info.CurrentView;
            }
            presenter.SetupPanelViewEvents(panelView);
            // add new tab and insert panel into it
            var tabPage = CreateTabPageFromModel(info);
            panelView.Dock = DockStyle.Fill;
            tabPage.Controls.Add(panelView);
            Pages.Controls.Add(tabPage);
            return panelView;
        }

        public TabPage CreateTabPageFromModel(IPanelModel model)
        {
            var tabPage = new TabPage();
            //if (!SystemInformation.TerminalServerSession)
            //{
            //    System.Reflection.PropertyInfo aProp =
            //        typeof (Control).GetProperty("DoubleBuffered", 
            //            System.Reflection.BindingFlags.NonPublic |
            //            System.Reflection.BindingFlags.Instance);
            //    aProp.SetValue(tabPage, true, null);
            //}

            tabPage.Padding = new Padding(0);
            tabPage.Text = model.TabName;
            tabPage.ImageIndex = imageManager.IndexOf(model.CurrentPath.First().ImageName);
            tabPage.ToolTipText = model.ToolTipText;
            return tabPage;
        }

        [Localizable(false)]
        private void mReRead_Click(object sender, EventArgs e)
        {
            presenter.DoPagesReRead();
        }

        [Localizable(false)]
        private void mCloseTab_Click(object sender, EventArgs e)
        {
            presenter.DoPagesCloseTab();
        }

        [Localizable(false)]
        private void mCloseOther_Click(object sender, EventArgs e)
        {
            presenter.DoPagesCloseOther();
        }

        private void PagesView_RightToLeftChanged(object sender, EventArgs e)
        {
            Pages.RightToLeftLayout = RightToLeft == RightToLeft.Yes;
        }

        private void Pages_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                e.Handled = presenter.PerformEscapeDown();
        }

        private void Pages_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                e.Handled = presenter.PerformEscapeUp();
        }

        public void Clear()
        {
            // TODO clear tabs
        }
    }
}
