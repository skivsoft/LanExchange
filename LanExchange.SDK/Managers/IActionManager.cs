using System;

namespace LanExchange.SDK.Managers
{
    /// <summary>
    /// The action manager.
    /// </summary>
    public interface IActionManager
    {
        /// <summary>
        /// Registers the action.
        /// </summary>
        /// <param name="action">The action.</param>
        void RegisterAction(IAction action);

        /// <summary>
        /// Executes the action.
        /// </summary>
        /// <param name="actionName">Name of the action.</param>
        [Obsolete("Consider using typed version: ExecuteAction<T>.")]
        void ExecuteAction(string actionName);

        /// <summary>
        /// Determines whether action is enabled found by the specified action name.
        /// </summary>
        /// <param name="actionName">Name of the action.</param>
        /// <returns></returns>
        [Obsolete("Consider using typed version: IsActionEnabled<T>.")]
        bool IsActionEnabled(string actionName);

        /// <summary>
        /// Executes the action.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void ExecuteAction<T>() where T : IAction;

        /// <summary>
        /// Determines whether action is enabled.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool IsActionEnabled<T>() where T : IAction;
    }
}