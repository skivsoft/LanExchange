namespace LanExchange.SDK
{
    /// <summary>
    /// Interface IAction
    /// </summary>
    public interface IAction
    {
        /// <summary>
        /// Executes action.
        /// </summary>
        void Execute();

        bool Enabled { get; }
    }
}
