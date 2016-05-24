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

        public ActionManager(IEnumerable<IAction> actions)
        {
            Contract.Requires<ArgumentNullException>(actions != null);

            this.actions = new Dictionary<string, IAction>();
            foreach (var action in actions)
                RegisterAction(action);
        }

        private void RegisterAction(IAction action)
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
