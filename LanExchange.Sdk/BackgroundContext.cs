namespace LanExchange.Sdk
{
    /// <summary>
    /// Pattern "Strategy" implementation for any background process.
    /// </summary>
    /// <seealso cref="IBackgroundStrategy" />
    public class BackgroundContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BackgroundContext"/> class.
        /// </summary>
        /// <param name="strategy">The strategy.</param>
        public BackgroundContext(IBackgroundStrategy strategy)
        {
            Strategy = strategy;
        }

        /// <summary>
        /// Executes the operation for selected strategy.
        /// </summary>
        public void ExecuteOperation()
        {
            if (Strategy != null)
            {
                //LogUtils.Info("Run algorithm of {0}", Strategy);
                Strategy.Algorithm();
            }
        }

        /// <summary>
        /// Gets or sets the strategy.
        /// </summary>
        /// <value>
        /// The strategy.
        /// </value>
        public IBackgroundStrategy Strategy { get; set; }
    }
}
