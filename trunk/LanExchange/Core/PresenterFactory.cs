namespace LanExchange.Core
{
    /// <summary>
    /// This factory creates IPresenter for specified descendant of IView.
    /// </summary>
    public static class PresenterFactory
    {
        private static IContainer s_Container;

        private static IPresenter<TView> CreateForView<TView>(TView view)
            where TView : IView
        {
            var presenter = s_Container.Resolve<IPresenter<TView>>();
            presenter.View = view;
            return presenter;
        }

        public static void SetContainer(IContainer container)
        {
            s_Container = container;
        }
    }

    /// <summary>
    /// Base interface for any Model interface.
    /// </summary>
    public interface IModel
    {

    }

    /// <summary>
    /// Base interface for any View interface.
    /// </summary>
    public interface IView
    {

    }

    /// <summary>
    /// Base interface for any Presenter interface.
    /// </summary>
    /// <typeparam name="TView"></typeparam>
    public interface IPresenter<TView> where TView : IView
    {
        TView View { get; set; }
    }

    public abstract class PresenterBase<TView> : IPresenter<TView> where TView : IView
    {
        public TView View { get; set; }
    }
}