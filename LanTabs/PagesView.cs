using System;
using System.Drawing;
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



        private void tc_MouseDown(object sender, MouseEventArgs e)
        {
            // store clicked tab
            TabControl tc = (TabControl)sender;
            int hover_index = this.getHoverTabIndex(tc);
            if (hover_index >= 0) { tc.Tag = tc.TabPages[hover_index]; }
        }
        private void tc_MouseUp(object sender, MouseEventArgs e)
        {
            // clear stored tab
            TabControl tc = (TabControl)sender;
            tc.Tag = null;
        }
        private void tc_MouseMove(object sender, MouseEventArgs e)
        {
            // mouse button down? tab was clicked?
            TabControl tc = (TabControl)sender;
            if ((e.Button != MouseButtons.Left) || (tc.Tag == null)) return;
            TabPage clickedTab = (TabPage)tc.Tag;
            int clicked_index = tc.TabPages.IndexOf(clickedTab);

            // start drag n drop
            tc.DoDragDrop(clickedTab, DragDropEffects.All);
        }
        private void tc_DragOver(object sender, DragEventArgs e)
        {
            TabControl tc = (TabControl)sender;

            // a tab is draged?
            if (e.Data.GetData(typeof(TabPage)) == null) return;
            TabPage dragTab = (TabPage)e.Data.GetData(typeof(TabPage));
            int dragTab_index = tc.TabPages.IndexOf(dragTab);

            // hover over a tab?
            int hoverTab_index = this.getHoverTabIndex(tc);
            if (hoverTab_index < 0) { e.Effect = DragDropEffects.None; return; }
            TabPage hoverTab = tc.TabPages[hoverTab_index];
            e.Effect = DragDropEffects.Move;

            // start of drag?
            if (dragTab == hoverTab) return;

            // swap dragTab & hoverTab - avoids toggeling
            Rectangle dragTabRect = tc.GetTabRect(dragTab_index);
            Rectangle hoverTabRect = tc.GetTabRect(hoverTab_index);

            if (dragTabRect.Width < hoverTabRect.Width)
            {
                Point tcLocation = tc.PointToScreen(tc.Location);

                if (dragTab_index < hoverTab_index)
                {
                    if ((e.X - tcLocation.X) > ((hoverTabRect.X + hoverTabRect.Width) - dragTabRect.Width))
                        this.swapTabPages(tc, dragTab, hoverTab);
                }
                else if (dragTab_index > hoverTab_index)
                {
                    if ((e.X - tcLocation.X) < (hoverTabRect.X + dragTabRect.Width))
                        this.swapTabPages(tc, dragTab, hoverTab);
                }
            }
            else this.swapTabPages(tc, dragTab, hoverTab);

            // select new pos of dragTab
            tc.SelectedIndex = tc.TabPages.IndexOf(dragTab);
        }

        private int getHoverTabIndex(TabControl tc)
        {
            for (int i = 0; i < tc.TabPages.Count; i++)
            {
                if (tc.GetTabRect(i).Contains(tc.PointToClient(Cursor.Position)))
                    return i;
            }

            return -1;
        }

        private void swapTabPages(TabControl tc, TabPage src, TabPage dst)
        {
            int index_src = tc.TabPages.IndexOf(src);
            int index_dst = tc.TabPages.IndexOf(dst);
            tc.TabPages[index_dst] = src;
            tc.TabPages[index_src] = dst;
            tc.Refresh();
        }
    }
}
