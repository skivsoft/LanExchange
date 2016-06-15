using LanExchange.Presentation.Interfaces.Menu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;

namespace LanExchange.Application.Implementation.Menu
{
    [ImmutableObject(true)]
    internal sealed class MenuGroup : IMenuElement
    {
        private readonly string text;
        private readonly IEnumerable<IMenuElement> elements;

        public MenuGroup(string text, IEnumerable<IMenuElement> elements)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(text));
            Contract.Requires<ArgumentNullException>(elements != null);

            this.text = text;
            this.elements = elements;
        }

        public void Accept(IMenuElementVisitor visitor)
        {
            Contract.Requires<ArgumentNullException>(visitor != null);

            visitor.VisitMenuGroup(text);
            foreach (var element in elements)
                element.Accept(visitor);
        }
    }
}
