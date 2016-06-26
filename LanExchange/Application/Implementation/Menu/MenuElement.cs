using System;
using LanExchange.Presentation.Interfaces.Menu;
using System.Diagnostics.Contracts;
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
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(text));
            Contract.Requires<ArgumentNullException>(shortcut != null);
            Contract.Requires<ArgumentNullException>(command != null);

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
            Contract.Requires<ArgumentNullException>(visitor != null);

            visitor.VisitMenuElement(text, shortcut, command, kind == MenuElementKind.Default);
        }
    }
}