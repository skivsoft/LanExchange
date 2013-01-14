using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using LanExchange.Presenter;
using LanExchange.Model;
using LanExchange.View;

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

        public void NewTabFromItemList(PanelItemList info)
        {
            TabPage Tab = new TabPage();
            Tab.Text = Ellipsis(info.TabName, 20);
            Tab.ImageIndex = LanExchangeIcons.Workgroup;
            Tab.ToolTipText = info.GetTabToolTip();
            Pages.Controls.Add(Tab);
        }

        public string SelectedTabText
        {
            get
            {
                if (Pages.TabPages.Count > 0 && Pages.SelectedTab != null)
                    return Pages.SelectedTab.Text;
                return String.Empty;
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
            m_Presenter.NewTab();
        }

        private void mCloseTab_Click(object sender, EventArgs e)
        {
            m_Presenter.CloseTab();
        }

        private void mRenameTab_Click(object sender, EventArgs e)
        {
            m_Presenter.RenameTab();
        }

        private void popPages_Opening(object sender, CancelEventArgs e)
        {
            mCloseTab.Enabled = m_Presenter.CanCloseTab();
            mSelectTab.Enabled = m_Presenter.GetModel().Count > 1;
            if (mSelectTab.Enabled)
            {
                mSelectTab.DropDownItems.Clear();
                m_Presenter.AddTabsToMenuItem(mSelectTab, m_Presenter.mSelectTab_Click, false);
            }
        }

        private void mTabParams_Click(object sender, EventArgs e)
        {
            using (var Form = new TabParamsForm())
            {
                int Index = PopupSelectedIndex;
                PagesModel M = m_Presenter.GetModel();
                PanelItemList Info = M.GetItem(Index);
                if (Info != null)
                {
                    TabPage Tab = Pages.TabPages[Index];
                    Form.Text = String.Format(Form.Text, Tab != null ? Tab.Text : "???");
                    Form.ScanMode = Info.ScanMode;
                    Form.Groups = Info.Groups;
                    if (Form.ShowDialog() == DialogResult.OK)
                    {
                        Info.ScanMode = Form.ScanMode;
                        Info.Groups = Form.Groups;
                        Info.UpdateSubsctiption();
                        m_Presenter.GetModel().SaveSettings();
                    }
                }
            }
        }

        private void Pages_Selected(object sender, TabControlEventArgs e)
        {
            m_Presenter.GetModel().SelectedIndex = e.TabPageIndex;
            m_Presenter.GetModel().SaveSettings();
        }

        public PanelView GetActivePanelView()
        {
            if (Pages.TabCount == 0 || Pages.SelectedTab == null)
                return null;
            ControlCollection ctrls = Pages.SelectedTab.Controls;
            return ctrls.Count > 0 ? ctrls[0] as PanelView : null;
        }

        public void SetTabToolTip(int index, string value)
        {
            if (index >= 0 && index <= Pages.TabCount-1)
                Pages.TabPages[index].ToolTipText = value;
        }

        public void FocusPanelView()
        {
            PanelView pv = GetActivePanelView();
            if (pv != null && ActiveControl != pv)
            {
                ActiveControl = pv;
                pv.FocusListView();
                pv.GetPresenter().UpdateItemsAndStatus();
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

        public void SetSelectedIndex(int value)
        {
            Pages.SelectedIndex = value;
            m_Presenter.GetModel().SelectedIndex = value;
        }

        public int GetSelectedIndex()
        {
            return Pages.SelectedIndex;
        }
    }
}
