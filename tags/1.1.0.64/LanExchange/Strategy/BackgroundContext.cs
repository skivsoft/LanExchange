using NLog;
namespace LanExchange.Strategy
{
    /// <summary>
    /// Pattern "Strategy" implementation for any background process.
    /// </summary>
    /// <seealso cref="IBackgroundStrategy"/>
    public class BackgroundContext
    {
        private readonly static Logger logger = LogManager.GetCurrentClassLogger();

        public BackgroundContext(IBackgroundStrategy strategy)
        {
            Strategy = strategy;
        }

        public void ExecuteOperation()
        {
            if (Strategy != null)
            {
                logger.Info("Run algorithm of {0}", Strategy);
                Strategy.Algorithm();
            }
        }

        public IBackgroundStrategy Strategy { get; set; }
    }
}
