using System;

namespace LanExchange.SDK.Managers
{
    /// <summary>
    /// The action manager.
    /// </summary>
    public interface ICommandManager
    {
        /// <summary>
        /// Executes the action.
        /// </summary>
        /// <param name="commandName">Name of the command.</param>
        [Obsolete("Consider using typed version: ExecuteCommand<T>.")]
        void ExecuteCommand(string commandName);

        /// <summary>
        /// Determines whether command is enabled found by the specified command name.
        /// </summary>
        /// <param name="commandName">Name of the command.</param>
        /// <returns></returns>
        [Obsolete("Consider using typed version: IsCommandEnabled<T>.")]
        bool IsCommandEnabled(string commandName);

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void ExecuteCommand<T>() where T : ICommand;

        /// <summary>
        /// Determines whether command is enabled.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool IsCommandEnabled<T>() where T : ICommand;
    }
}