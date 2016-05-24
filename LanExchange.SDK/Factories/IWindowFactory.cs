namespace LanExchange.SDK.Factories
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
        IMainView CreateMainView();
    }
}