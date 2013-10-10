namespace LanTabs
{
    public interface IPagesView : IView
    {
        int PagesCount { get; }
        int SelectedIndex { get; }

        void AssignTab(int index, IPageView homeTab);
        void AddEmptyTab();

        void CloseTab();

        void CloseOtherTabs();
    }
}
