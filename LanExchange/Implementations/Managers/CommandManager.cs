using LanExchange.SDK;
using LanExchange.SDK.Managers;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace LanExchange.Implementations.Managers
{
    internal sealed class CommandManager : ICommandManager
    {
        private readonly Dictionary<string, ICommand> commands;

        public CommandManager(IEnumerable<ICommand> commands)
        {
            Contract.Requires<ArgumentNullException>(commands != null);

            this.commands = new Dictionary<string, ICommand>();
            foreach (var action in commands)
                RegisterCommand(action);
        }

        private void RegisterCommand(ICommand command)
        {
            Contract.Requires<ArgumentNullException>(command != null);

            commands.Add(command.GetType().Name, command);
        }

        public void ExecuteCommand(string commandName)
        {
            ICommand command;
            if (commands.TryGetValue(commandName, out command))
                if (command.Enabled)
                    command.Execute();
        }

        public void ExecuteCommand<T>() where T : ICommand
        {
            ExecuteCommand(typeof(T).Name);
        }

        public bool IsCommandEnabled(string commandName)
        {
            ICommand command;
            if (commands.TryGetValue(commandName, out command))
                return command.Enabled;

            return false;
        }

        public bool IsCommandEnabled<T>() where T : ICommand
        {
            return IsCommandEnabled(typeof(T).Name);
        }
    }
}