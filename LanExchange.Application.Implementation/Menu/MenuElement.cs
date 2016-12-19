﻿using System;
using System.ComponentModel;
using LanExchange.Presentation.Interfaces;
using LanExchange.Presentation.Interfaces.Menu;

namespace LanExchange.Application.Implementation.Menu
{
    [ImmutableObject(true)]
    public sealed class MenuElement : IMenuElement
    {
        private readonly string text;
        private readonly string shortcut;
        private readonly ICommand command;
        private readonly MenuElementKind kind;

        public MenuElement(string text, string shortcut, ICommand command, MenuElementKind kind = MenuElementKind.Normal)
        {
            if (string.IsNullOrEmpty(text)) throw new ArgumentNullException(nameof(text));
            if (shortcut == null) throw new ArgumentNullException(nameof(shortcut));
            if (command == null) throw new ArgumentNullException(nameof(command));

            this.text = text;
            this.shortcut = shortcut;
            this.command = command;
            this.kind = kind;
        }

        public MenuElement(string text, ICommand command) : this(text, string.Empty, command)
        {
        }

        public void Accept(IMenuElementVisitor visitor)
        {
            if (visitor == null) throw new ArgumentNullException(nameof(visitor));

            visitor.VisitMenuElement(text, shortcut, command, kind == MenuElementKind.Default);
        }
    }
}