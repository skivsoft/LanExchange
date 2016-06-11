namespace LanExchange.Presentation.Interfaces
{
    /// <summary>
    /// The view container interface.
    /// </summary>
    public interface IViewContainer
    {
        /// <summary>
        /// Adds the view into container and docks it in appliance with the specified dock style.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="dockStyle">The dock style.</param>
        void AddView(IView view, ViewDockStyle dockStyle);
    }
}
