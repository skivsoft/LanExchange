using LanExchange.SDK;
using LanExchange.SDK.Managers;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace LanExchange.Implementations.Managers
{
    internal sealed class ActionManager : IActionManager
    {
        private readonly Dictionary<string, IAction> actions;

        public ActionManager()
        {
            actions = new Dictionary<string, IAction>();
        }

        public void RegisterAction(IAction action)
        {
            Contract.Requires<ArgumentNullException>(action != null);

            actions.Add(action.GetType().Name, action);
        }

        public void ExecuteAction(string actionName)
        {
            IAction action;
            if (actions.TryGetValue(actionName, out action))
                if (action.Enabled)
                    action.Execute();
        }

        public void ExecuteAction<T>() where T : IAction
        {
            ExecuteAction(typeof(T).Name);
        }

        public bool IsActionEnabled(string actionName)
        {
            IAction action;
            if (actions.TryGetValue(actionName, out action))
                return action.Enabled;

            return false;
        }

        public bool IsActionEnabled<T>() where T : IAction
        {
            return IsActionEnabled(typeof(T).Name);
        }
    }
}
