namespace LanExchange.Presentation.Interfaces.Factories
{
    /// <summary>
    /// The window factory.
    /// </summary>
    public interface IWindowFactory
    {
        /// <summary>
        /// Creates the about view.
        /// </summary>
        /// <returns></returns>
        IWindow CreateAboutView();

        /// <summary>
        /// Creates the main view.
        /// </summary>
        /// <returns></returns>
        IMainView CreateMainView();

        /// <summary>
        /// Creates the check availability window.
        /// </summary>
        /// <returns></returns>
        ICheckAvailabilityWindow CreateCheckAvailabilityWindow();
    }
}