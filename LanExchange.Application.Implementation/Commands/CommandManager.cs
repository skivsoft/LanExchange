﻿using System;
using System.Collections.Generic;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Implementation.Commands
{
    public sealed class CommandManager : ICommandManager
    {
        private readonly Dictionary<string, ICommand> commands;

        public CommandManager(IEnumerable<ICommand> commands)
        {
            if (commands == null) throw new ArgumentNullException(nameof(commands));

            this.commands = new Dictionary<string, ICommand>();
            foreach (var command in commands)
                this.commands.Add(command.GetType().Name, command);
        }

        public ICommand GetCommand(string commandName)
        {
            ICommand command;
            if (commands.TryGetValue(commandName, out command))
                return command;

            return NullCommand.Instance;
        }
    }
}