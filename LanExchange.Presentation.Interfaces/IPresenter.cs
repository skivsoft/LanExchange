namespace LanExchange.SDK.Presentation.Interfaces
{
    /// <summary>
    /// The base interface for any Presenter in Model-View-Presenter pattern.
    /// </summary>
    /// <typeparam name="TView"></typeparam>
    public interface IPresenter<TView> where TView : IView
    {
        void Initialize(TView view);
    }
}