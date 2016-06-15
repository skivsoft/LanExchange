using LanExchange.Presentation.Interfaces.Menu;
using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;

namespace LanExchange.Application.Implementation.Menu
{
    [ImmutableObject(true)]
    internal sealed class MenuSeparator : IMenuElement
    {
        public void Accept(IMenuElementVisitor visitor)
        {
            Contract.Requires<ArgumentNullException>(visitor != null);

            visitor.VisitSeparator();
        }
    }
}