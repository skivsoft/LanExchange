namespace LanExchange.Presentation.Interfaces
{
    /// <summary>
    /// The perform Escape KeyDown/KeyUp interface.
    /// </summary>
    public interface IPerformEscape
    {
        /// <summary>
        /// Performs action on Escape key down.
        /// </summary>
        /// <returns>
        /// <c>true</c> if event handled; otherwise <c>false</c>.
        /// </returns>
        bool PerformEscapeDown();

        /// <summary>
        /// Performs action on Escape key up.
        /// </summary>
        /// <returns>
        /// <c>true</c> if event handled; otherwise <c>false</c>.
        /// </returns>
        bool PerformEscapeUp();
    }
}