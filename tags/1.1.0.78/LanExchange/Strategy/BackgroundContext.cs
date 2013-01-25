using LanExchange.Utils;
namespace LanExchange.Strategy
{
    /// <summary>
    /// Pattern "Strategy" implementation for any background process.
    /// </summary>
    /// <seealso cref="IBackgroundStrategy"/>
    public class BackgroundContext
    {
        public BackgroundContext(IBackgroundStrategy strategy)
        {
            Strategy = strategy;
        }

        public void ExecuteOperation()
        {
            if (Strategy != null)
            {
                LogUtils.Info("Run algorithm of {0}", Strategy);
                Strategy.Algorithm();
            }
        }

        public IBackgroundStrategy Strategy { get; set; }
    }
}
