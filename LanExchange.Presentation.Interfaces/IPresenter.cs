namespace LanExchange.Presentation.Interfaces
{
    /// <summary>
    /// The base interface for any Presenter in Model-View-Presenter pattern.
    /// </summary>
    /// <typeparam name="TView"></typeparam>
    public interface IPresenter<in TView> where TView : IView
    {
        void Initialize(TView view);
    }
}