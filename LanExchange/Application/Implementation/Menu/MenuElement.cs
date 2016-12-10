using System;
using LanExchange.Presentation.Interfaces.Menu;
using System.ComponentModel;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Implementation.Menu
{
    [ImmutableObject(true)]
    internal sealed class MenuElement : IMenuElement
    {
        private readonly string text;
        private readonly string shortcut;
        private readonly ICommand command;
        private readonly MenuElementKind kind;

        public MenuElement(string text, string shortcut, ICommand command, MenuElementKind kind = MenuElementKind.Normal)
        {
            if (string.IsNullOrEmpty(text)) throw new ArgumentNullException(nameof(text));

            this.text = text;
            this.shortcut = shortcut ?? throw new ArgumentNullException(nameof(shortcut));
            this.command = command ?? throw new ArgumentNullException(nameof(command));
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