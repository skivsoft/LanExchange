namespace LanTabs
{
    public interface IPagesPresenter : IPresenter<IPagesView>
    {
        void CommandNewTab();
        void CommandReloadTab();
        void CommandCloseTab();

        void AssignTab(int index, IPageView pageView);

        int SelectedIndex { get; }

        void CommandMoveToLeft();

        void CommandMoveToRight();

        void CommandCloseOtherTabs();
    }
}
