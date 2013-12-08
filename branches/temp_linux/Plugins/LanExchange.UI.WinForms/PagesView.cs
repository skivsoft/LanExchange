using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using LanExchange.Properties;
using LanExchange.SDK;
using LanExchange.SDK.UI;

namespace LanExchange.UI.WinForms
{
    public partial class PagesView : UserControl, IPagesView, ITranslationable
    {
        private readonly IPagesPresenter m_Presenter;
        private int m_PopupSelectedIndex = -1;

        /// <summary>
        /// Mouse right click was pressed.
        /// </summary>
        private bool m_MouseDown;
        /// <summary>
        /// Popup menu was opened.
        /// </summary>
        private bool m_Opened;
        /// <summary>
        /// Menu item in popup menu was clicked.
        /// </summary>
        private bool m_Clicked;

        public PagesView(IPagesPresenter presenter)
        {
            InitializeComponent();
            m_Presenter = presenter;
            m_Presenter.View = this;
            App.Images.SetImagesTo(popPages);
        }

        public void TranslateUI()
        {
            TranslationUtils.TranslateComponents(Resources.ResourceManager, this, components);
            SetupContextMenu(popPages.Items);
            TranslationUtils.TranslateControls(Controls);
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
            App.PanelItemTypes.CreateDefaultRoots();
            var index = 0;
            foreach (var root in App.PanelItemTypes.DefaultRoots)
            {
                var menuItem = new ToolStripMenuItem(string.Format(Resources.PagesView_OpenTab, root.Name));
                menuItem.Tag = root;
                menuItem.ImageIndex = App.Images.IndexOf(root.GetTabImageName());
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
            if (!m_Presenter.SelectTabByName(root.Name))
            {
                var info = App.Resolve<IPanelModel>();
                info.TabName = root.Name;
                info.SetDefaultRoot(root);
                info.DataType = App.PanelFillers.GetFillType(root).Name;
                m_Presenter.AddTab(info);
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
            m_Presenter.SelectedIndex = e.TabPageIndex;
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
                m_PopupSelectedIndex = GetTabIndexByPoint(e.Location);
                m_MouseDown = true;
                m_Opened = false;
                m_Clicked = false;
            }
            //logger.Info("MouseDown={0}", bMouseDown);
        }

        private void popPages_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            m_Clicked = m_Opened;
            //logger.Info("Clicked={0}", bClicked);
        }

        private void popPages_Opened(object sender, EventArgs e)
        {
            if (m_MouseDown && !m_Opened)
            {
                m_Opened = true;
                m_MouseDown = false;
            }
            else
                m_Opened = false;
            mReRead.Enabled = App.Presenter.IsActionEnabled("ActionReRead");
            mCloseTab.Enabled = App.Presenter.IsActionEnabled("ActionCloseTab");
            mCloseOther.Enabled = App.Presenter.IsActionEnabled("ActionCloseOther");
            //logger.Info("Opened={0}", bOpened);
        }

        private void popPages_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            m_Opened = false;
            //logger.Info("Closed={0}", bClosed);
        }

        public int PopupSelectedIndex
        {
            get
            {
                //logger.Info("MouseDown={0}, Opened={1}, Clicked={2}, Closed={3}", bMouseDown, bOpened, bClicked, bClosed);
                if (m_Clicked && !m_Opened)
                {
                    m_MouseDown = false;
                    m_Opened = false;
                    m_Clicked = false;
                    if (m_PopupSelectedIndex < 0 || m_PopupSelectedIndex > Pages.TabCount - 1)
                        return Pages.SelectedIndex;
                    return m_PopupSelectedIndex;
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
                m_Presenter.SelectedIndex = value;
            }
        }

        [Localizable(false)]
        public IPanelView CreatePanelView(IPanelModel info)
        {
            var panelView = (PanelView) App.Resolve<IPanelView>();
            panelView.GridLines = App.Config.ShowGridLines;
            var listView = panelView.Controls[0] as ListView;
            if (listView != null)
            {
                App.Images.SetImagesTo(listView);
                listView.View = (View) info.CurrentView;
            }
            m_Presenter.SetupPanelViewEvents(panelView);
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
            tabPage.Padding = new Padding(0);
            tabPage.Text = model.TabName;
            tabPage.ImageIndex = App.Images.IndexOf(model.CurrentPath.Item[0].GetTabImageName());
            tabPage.ToolTipText = model.ToolTipText;
            return tabPage;
        }

        public IPagesPresenter Presenter
        {
            get { return m_Presenter; }
        }

        private void mReRead_Click(object sender, EventArgs e)
        {
            App.Presenter.ExecuteAction("ActionReRead");
        }

        private void mCloseTab_Click(object sender, EventArgs e)
        {
            App.Presenter.ExecuteAction("ActionCloseTab");
        }

        private void mCloseOther_Click(object sender, EventArgs e)
        {
            App.Presenter.ExecuteAction("ActionCloseOther");
        }
    }
}
