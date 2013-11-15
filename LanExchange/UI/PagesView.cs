using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using LanExchange.Intf;
using LanExchange.Presenter.Action;
using LanExchange.Properties;
using LanExchange.SDK;

namespace LanExchange.UI
{
    public partial class PagesView : UserControl, IPagesView
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
            SetupContextMenu(popPages.Items);
        }

        private void SetupContextMenu(ToolStripItemCollection items)
        {
            App.PanelItemTypes.CreateDefaultRoots();
            var index = 0;
            foreach (var root in App.PanelItemTypes.DefaultRoots)
            {
                var imageName = App.PanelFillers.GetFillType(root).Name + PanelImageNames.NORMAL_POSTFIX;
                var menuItem = new ToolStripMenuItem(string.Format(Resources.PagesView_OpenTab, root.Name));
                menuItem.Tag = root;
                menuItem.ImageIndex = App.Images.IndexOf(imageName);
                menuItem.Click += PluginOnClick;
                items.Insert(index, menuItem);
                index++;
            }
        }

        private void PluginOnClick(object sender, EventArgs eventArgs)
        {
            var menuItem = sender as ToolStripMenuItem;
            if (menuItem == null) return;
            if (menuItem.Checked)
            {
                return;
            }
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

        /// <summary>
        /// Simple solution for long strings. 
        /// TODO: Changes needed here. Max length must be in pixels instead number of chars.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        [Localizable(false)]
        public string Ellipsis(string text, int length)
        {
            if (text.Length > length)
                return text.Substring(0, length) + "…";
            return text;
        }

        public string SelectedTabText
        {
            get
            {
                if (Pages.TabPages.Count > 0 && Pages.SelectedTab != null)
                    return Pages.SelectedTab.Text;
                return string.Empty;
            }
            set
            {
                if (Pages.TabPages.Count > 0 && Pages.SelectedTab != null)
                    Pages.SelectedTab.Text = Ellipsis(value, 20);
            }
        }


        public void AddControl(int index, Control control)
        {
            if (index < 0 || index > Pages.TabPages.Count - 1)
                throw new ArgumentOutOfRangeException("index");
            control.Dock = DockStyle.Fill;
            Pages.TabPages[index].Controls.Add(control);
        }


        public void RemoveTabAt(int index)
        {
            Pages.TabPages.RemoveAt(index);
        }

        //public PanelItemList GetPanelItemListByPoint(Point point)
        //{
        //    int Index = -1;
        //    for (int i = 0; i < Pages.TabPages.Count; i++)
        //    {
        //        TabPage page = Pages.TabPages[i];
        //        if (Pages.GetTabRect(i).Contains(point))
        //        {
        //            Index = i;
        //            break;
        //        }
        //    }
        //    if (Index == -1) return null;
        //    return m_Presenter.GetModel().GetItem(Index);
        //}

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
            mReread.Enabled = App.Presenter.IsActionEnabled<ReReadAction>();
            mCloseTab.Enabled = App.Presenter.IsActionEnabled<CloseTabAction>();
            mCloseOtherTabs.Enabled = App.Presenter.IsActionEnabled<CloseOtherAction>();
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

        internal void AddTabsToMenuItem(ToolStripMenuItem menuitem, EventHandler handler, bool bHideActive)
        {
            for (int i = 0; i < m_Presenter.Count; i++)
            {
                if (bHideActive && (i == SelectedIndex))
                    continue;
                string S = m_Presenter.GetTabName(i);
                var Item = new ToolStripMenuItem
                {
                    Checked = (i == SelectedIndex),
                    Text = Ellipsis(S, 20),
                    ToolTipText = S,
                    Tag = i
                };
                Item.Click += handler;
                menuitem.DropDownItems.Add(Item);
            }
        }

        public void NewTabFromItemList(IPanelModel info)
        {
            var tab = new TabPage();
            tab.Padding = new Padding(0);
            tab.Text = Ellipsis(info.TabName, 20);
            tab.ImageIndex = App.Images.IndexOf(info.GetImageName());
            tab.ToolTipText = info.ToolTipText;
            Pages.Controls.Add(tab);
        }

        [Localizable(false)]
        public IPanelView CreatePanelView(IPanelModel info)
        {
            var panelView = (PanelView) App.Resolve<IPanelView>();
            var listView = panelView.Controls[0] as ListView;
            if (listView != null)
            {
                App.Images.SetImagesTo(listView);
                listView.View = (View) info.CurrentView;
            }
            m_Presenter.SetupPanelViewEvents(panelView);
            // add new tab and insert panel into it
            NewTabFromItemList(info);
            AddControl(TabPagesCount - 1, panelView);
            return panelView;
        }

        public IPagesPresenter Presenter
        {
            get { return m_Presenter; }
        }

        private void mReread_Click(object sender, EventArgs e)
        {
            App.Presenter.ExecuteAction<ReReadAction>();
        }

        private void mCloseTab_Click(object sender, EventArgs e)
        {
            App.Presenter.ExecuteAction<CloseTabAction>();
        }

        private void mCloseOtherTabs_Click(object sender, EventArgs e)
        {
            App.Presenter.ExecuteAction<CloseOtherAction>();
        }
    }
}
