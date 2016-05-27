using LanExchange.SDK.Presentation.Interfaces;

namespace LanExchange.SDK
{
    public abstract class PresenterBase<TView> : IPresenter<TView> where TView : IView
    {
        public TView View { get; set; }
    }
}