namespace LanExchange.Presentation.Interfaces
{
    /// <summary>
    /// The action manager.
    /// </summary>
    public interface ICommandManager
    {
        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <param name="commandName">Name of the command.</param>
        /// <returns></returns>
        ICommand GetCommand(string commandName);
    }
}