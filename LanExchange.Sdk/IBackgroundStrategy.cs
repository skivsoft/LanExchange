namespace LanExchange.SDK
{
    /// <summary>
    /// Pattern "Strategy" implementation for any background process.
    /// </summary>
    /// <seealso cref="BackgroundContext" />
    public interface IBackgroundStrategy
    {
        /// <summary>
        /// Run strategy.
        /// </summary>
        void Algorithm();
    }
}