using System;
using System.Windows.Forms;

namespace LanTabs
{
    public partial class PagesView : UserControl, IPagesView
    {
        private readonly IPagesPresenter m_Presenter;
        
        public PagesView(IPagesPresenter presenter)
        {
            m_Presenter = presenter;
            m_Presenter.View = this;
            InitializeComponent();
            m_Presenter.CommandNewTab();
        }

        private void mNewTab_Click(object sender, System.EventArgs e)
        {
            m_Presenter.CommandNewTab();
        }

        private void mReload_Click(object sender, System.EventArgs e)
        {
            m_Presenter.CommandReloadTab();
        }

        private void mCloseTab_Click(object sender, System.EventArgs e)
        {
            m_Presenter.CommandCloseTab();
        }

        private void Pages_Selected(object sender, TabControlEventArgs e)
        {
            if (e.Action == TabControlAction.Selected && e.TabPageIndex == Pages.TabCount-1)
            {
                m_Presenter.CommandNewTab();
            }
        }

        public int PagesCount
        {
            get { return Pages.TabCount; }
        }

        public int SelectedIndex
        {
            get { return Pages.SelectedIndex; }
        }

        public void AssignTab(int index, IPageView tabView)
        {
            var control = tabView as Control;
            var tabPage = Pages.TabPages[index];
            if (control != null && tabPage != null)
            {
                tabPage.Text = tabView.Title;
                if (tabPage.Controls.Count > 0)
                    tabPage.Controls.Clear();
                tabPage.Controls.Add(control);
                control.Dock = DockStyle.Fill;
            }
        }

        public void AddEmptyTab()
        {
            // add new tab placeholder
            var tabEmpty = new TabPage();
            Pages.Controls.Add(tabEmpty);
        }

        public void CloseTab()
        {
            var index = Pages.SelectedIndex;
            Pages.Controls.RemoveAt(index);
            Pages.SelectedIndex = Math.Max(0, index-1);
        }

        public void CloseOtherTabs()
        {
            for (int index = Pages.TabCount-1; index >= 0; index--)
                if (index != Pages.TabCount-1 && index != Pages.SelectedIndex)
                {
                    Pages.Controls.RemoveAt(index);
                }
            Pages.SelectedIndex = 0;
        }

        private void mMoveToLeft_Click(object sender, System.EventArgs e)
        {
            m_Presenter.CommandMoveToLeft();
        }

        private void mMoveToRight_Click(object sender, System.EventArgs e)
        {
            m_Presenter.CommandMoveToRight();
        }

        private void mCloseOther_Click(object sender, System.EventArgs e)
        {
            m_Presenter.CommandCloseOtherTabs();
        }
    }
}
