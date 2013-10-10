namespace LanTabs
{
    public abstract class PresenterBase<TView> : IPresenter<TView> where TView : IView
    {
        public TView View { get; set; }
    }
}