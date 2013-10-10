namespace LanTabs
{
    public class PagesPresenter : PresenterBase<IPagesView>, IPagesPresenter
    {
        public void CommandNewTab()
        {
            var homeTab = App.Resolve<IPageHomeView>();
            homeTab.Pages = this;
            AssignTab(View.PagesCount-1, homeTab);
            AddEmptyTab();
        }

        public void CommandReloadTab()
        {
        }

        public void CommandCloseTab()
        {
            View.CloseTab();
        }

        public void AssignTab(int index, IPageView pageView)
        {
            View.AssignTab(index, pageView);
        }

        public void AddEmptyTab()
        {
            View.AddEmptyTab();
        }

        public int SelectedIndex
        {
            get { return View.SelectedIndex; }
        }

        public void CommandMoveToLeft()
        {
            throw new System.NotImplementedException();
        }

        public void CommandMoveToRight()
        {
            throw new System.NotImplementedException();
        }

        public void CommandCloseOtherTabs()
        {
            View.CloseOtherTabs();
        }
    }
}
