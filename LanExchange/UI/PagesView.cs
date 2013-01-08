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

        public PagesView()
        {
            InitializeComponent();
            m_Presenter = new PagesPresenter(this);
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
            else
                return text;
        }

        public void NewTabFromItemList(PanelItemList Info)
        {
            TabPage Tab = new TabPage();
            Tab.Text = Ellipsis(Info.TabName, 20);
            Tab.ImageIndex = LanExchangeIcons.imgWorkgroup;
            Tab.ToolTipText = Info.GetTabToolTip();
            Pages.Controls.Add(Tab);
        }

        public int SelectedIndex
        {
            get
            {
                return Pages.SelectedIndex;
            }
            set
            {
                Pages.SelectedIndex = value;
                m_Presenter.GetModel().SelectedIndex = value;
            }
        }


        public string SelectedTabText
        {
            get
            {
                if (Pages.TabPages.Count > 0 && Pages.SelectedTab != null)
                    return Pages.SelectedTab.Text;
                else
                    return String.Empty;
            }
            set
            {
                if (Pages.TabPages.Count > 0 && Pages.SelectedTab != null)
                    Pages.SelectedTab.Text = Ellipsis(value, 20);
            }
        }


        public void AddControl(int Index, Control control)
        {
            if (Index < 0 || Index > Pages.TabPages.Count - 1)
                throw new ArgumentOutOfRangeException("Index");
            Pages.TabPages[Index].Controls.Add(control);
        }


        public void RemoveTabAt(int Index)
        {
            Pages.TabPages.RemoveAt(Index);
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

        public TabPage GetTabPageByPoint(Point point)
        {
            for (int i = 0; i < Pages.TabPages.Count; i++)
            {
                TabPage page = Pages.TabPages[i];
                if (Pages.GetTabRect(i).Contains(point))
                    return Pages.TabPages[i];
            }
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
            mSelectTab.DropDownItems.Clear();
            m_Presenter.AddTabsToMenuItem(mSelectTab, m_Presenter.mSelectTab_Click, false);
            mCloseTab.Enabled = m_Presenter.CanCloseTab(m_Presenter.GetModel().SelectedIndex);
        }

        private void mTabParams_Click(object sender, EventArgs e)
        {
            using (TabParamsForm Form = new TabParamsForm())
            {
                PagesModel M = m_Presenter.GetModel();
                PanelItemList Info = M.GetItem(M.SelectedIndex);
                Form.ScanMode = Info.ScanMode;
                Form.Groups = Info.Groups;
                if (Form.ShowDialog() == DialogResult.OK)
                {
                    Info.ScanMode = Form.ScanMode;
                    Info.Groups = Form.Groups;
                    Info.UpdateSubsctiption();
                }
            }
        }

        private void Pages_Selected(object sender, TabControlEventArgs e)
        {
            m_Presenter.GetModel().SelectedIndex = e.TabPageIndex;
        }

        public PanelView GetActivePanelView()
        {
            if (Pages.TabCount == 0 || Pages.SelectedTab == null)
                return null;
            else
            {
                Control.ControlCollection ctrls = Pages.SelectedTab.Controls;
                return ctrls.Count > 0 ? ctrls[0] as PanelView : null;
            }
        }

        public void SetTabToolTip(int Index, string value)
        {
            if (Index >= 0 && Index <= Pages.TabCount-1)
                Pages.TabPages[Index].ToolTipText = value;
        }

        public void FocusPanelView()
        {
            PanelView PV = GetActivePanelView();
            if (PV != null)
            {
                ActiveControl = PV;
                PV.FocusListView();
                PV.GetPresenter().UpdateItemsAndStatus();
            }
        }
    }
}
