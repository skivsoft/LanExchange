using LanExchange.Presentation.Interfaces.Menu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;

namespace LanExchange.Application.Implementation.Menu
{
    [ImmutableObject(true)]
    internal sealed class MenuRoot : IMenuElement
    {
        private readonly IEnumerable<IMenuElement> elements;

        public MenuRoot(IEnumerable<IMenuElement> elements)
        {
            Contract.Requires<ArgumentNullException>(elements != null);

            this.elements = elements;
        }

        public void Accept(IMenuElementVisitor visitor)
        {
            Contract.Requires<ArgumentNullException>(visitor != null);

            visitor.VisitMenuRoot();
            foreach (var element in elements)
                element.Accept(visitor);
        }
    }
}
