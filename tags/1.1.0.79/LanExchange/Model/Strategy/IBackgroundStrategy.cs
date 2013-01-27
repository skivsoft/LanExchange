namespace LanExchange.Model.Strategy
{
    /// <summary>
    /// Pattern "Strategy" implementation for any background process.
    /// </summary>
    /// <seealso cref="BackgroundContext"/>
    public interface IBackgroundStrategy
    {
        void Algorithm();
    }
}