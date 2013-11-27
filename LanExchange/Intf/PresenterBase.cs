using LanExchange.SDK;

namespace LanExchange.Intf
{
    public abstract class PresenterBase<TView> : IPresenter<TView> where TView : IView
    {
        public TView View { get; set; }
    }
}