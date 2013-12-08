namespace LanExchange.SDK.Presenter
{
    public abstract class PresenterBase<TView> : IPresenter<TView> where TView : IView
    {
        public TView View { get; set; }
    }
}