namespace LanTabs
{
    public interface IPageView : IView
    {
        IPagesPresenter Pages { get; set; }

        string Title { get; }
    }
}
