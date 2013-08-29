namespace LanExchange.Intf
{
    /// <summary>
    /// Base interface for any Presenter interface.
    /// </summary>
    /// <typeparam name="TView"></typeparam>
    public interface IPresenter<TView> where TView : IView
    {
        TView View { get; set; }
    }
}