using System;
using LanExchange.Presentation.Interfaces.Menu;
using System.Diagnostics.Contracts;
using System.ComponentModel;

namespace LanExchange.Application.Implementation.Menu
{
    [ImmutableObject(true)]
    internal sealed class MenuElement : IMenuElement
    {
        private readonly string text;
        private readonly string shortcut;

        public MenuElement(string text, string shortcut)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(text));
            Contract.Requires<ArgumentNullException>(shortcut != null);

            this.text = text;
            this.shortcut = shortcut;
        }

        public MenuElement(string text) : this(text, string.Empty)
        {
        }

        public void Accept(IMenuElementVisitor visitor)
        {
            Contract.Requires<ArgumentNullException>(visitor != null);

            visitor.VisitMenuElement(text, shortcut);
        }
    }
}