namespace LanExchange.Sdk
{
    /// <summary>
    /// The logger interface.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Infoes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The args.</param>
        void Info(string message, params object[] args);
        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The args.</param>
        void Error(string message, params object[] args);
    }
}
