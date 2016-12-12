using LanExchange.Presentation.Interfaces.Menu;
using System;
using System.ComponentModel;

namespace LanExchange.Application.Implementation.Menu
{
    [ImmutableObject(true)]
    internal sealed class MenuSeparator : IMenuElement
    {
        public void Accept(IMenuElementVisitor visitor)
        {
            if (visitor != null) throw new ArgumentNullException(nameof(visitor));

            visitor.VisitSeparator();
        }
    }
}