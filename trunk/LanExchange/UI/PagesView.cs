using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using LanExchange.Presenter;
using LanExchange.Model;
using LanExchange.SDK;

namespace LanExchange.UI
{
    public partial class PagesView : UserControl, IPagesView
    {
        private readonly PagesPresenter m_Presenter;
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

        public PagesView()
        {
            InitializeComponent();
            m_Presenter = new PagesPresenter(this);
            mSelectTab.DropDownDirection = ToolStripDropDownDirection.BelowLeft;
        }

        public PagesPresenter GetPresenter()
        {
            return m_Presenter;
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
        public string Ellipsis(string text, int length)
        {
            if (text.Length > length)
                return text.Substring(0, length) + "…";
            return text;
        }

        public void NewTabFromItemList(IPanelModel info)
        {
            TabPage Tab = new TabPage();
            Tab.Padding = new Padding(0);
            Tab.Text = Ellipsis(info.TabName, 20);
            Tab.ImageIndex = AppPresenter.Images.IndexOf(PanelImageNames.Workgroup);
            Tab.ToolTipText = info.ToolTipText;
            Pages.Controls.Add(Tab);
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

        private void mNewTab_Click(object sender, EventArgs e)
        {
            m_Presenter.CommandNewTab();
        }

        private void mCloseTab_Click(object sender, EventArgs e)
        {
            m_Presenter.CommandCloseTab();
        }

        private void mRenameTab_Click(object sender, EventArgs e)
        {
            m_Presenter.CommandRenameTab();
        }

        private void popPages_Opening(object sender, CancelEventArgs e)
        {
            mCloseTab.Enabled = m_Presenter.CanCloseTab();
            mSelectTab.Enabled = m_Presenter.GetModel().Count > 1;
            if (mSelectTab.Enabled && mSelectTab.DropDownItems.Count == 0)
            {
                var menuItem = new ToolStripSeparator();
                mSelectTab.DropDownItems.Add(menuItem);
            }
        }

        private void mSelectTab_DropDownOpening(object sender, EventArgs e)
        {
            if (mSelectTab.Enabled)
            {
                mSelectTab.DropDownItems.Clear();
                AddTabsToMenuItem(mSelectTab, mSelectTab_Click, false);
            }
        }

        private void Pages_Selected(object sender, TabControlEventArgs e)
        {
            m_Presenter.GetModel().SelectedIndex = e.TabPageIndex;
            m_Presenter.GetModel().SaveSettings();
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
                //TODO !!!NEED UPDATE ITEMS
                //pv.Presenter.UpdateItemsAndStatus();
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
                m_Presenter.GetModel().SelectedIndex = value;
            }
        }

        internal void AddTabsToMenuItem(ToolStripMenuItem menuitem, EventHandler handler, bool bHideActive)
        {
            var M = m_Presenter.GetModel();
            for (int i = 0; i < M.Count; i++)
            {
                if (bHideActive && (!m_Presenter.CanCloseTab() || (i == SelectedIndex)))
                    continue;
                string S = M.GetTabName(i);
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

        public IPanelView CreatePanelView(IPanelModel info)
        {
            var PV = new PanelView();
            ListView LV = PV.Controls[0] as ListView;
            if (LV != null)
            {
                LV.SmallImageList = AppPresenter.Images.SmallImageList;
                LV.LargeImageList = AppPresenter.Images.LargeImageList;
                LV.View = (System.Windows.Forms.View) info.CurrentView;
                if (MainForm.Instance != null)
                    MainForm.Instance.tipComps.SetToolTip(LV, " ");
            }
            PV.FocusedItemChanged += m_Presenter.PV_FocusedItemChanged;
            PV.FilterTextChanged += m_Presenter.PV_FilterTextChanged;
            // add new tab and insert panel into it
            NewTabFromItemList(info);
            AddControl(TabPagesCount - 1, PV);
            return PV;
        }

        public void mSelectTab_Click(object sender, EventArgs e)
        {
            var menu = sender as ToolStripMenuItem;
            if (menu != null)
            {
                int Index = (int)menu.Tag;
                if (SelectedIndex != Index)
                    SelectedIndex = Index;
            }
        }
    }
}
