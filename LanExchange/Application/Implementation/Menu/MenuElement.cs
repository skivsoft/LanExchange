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

        public MenuElement(string text)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(text));

            this.text = text;
        }

        public void Accept(IMenuElementVisitor visitor)
        {
            Contract.Requires<ArgumentNullException>(visitor != null);

            visitor.VisitMenuElement(text);
        }
    }
}