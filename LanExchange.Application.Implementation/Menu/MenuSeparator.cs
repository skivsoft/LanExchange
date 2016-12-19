using System;
using System.ComponentModel;
using LanExchange.Presentation.Interfaces.Menu;

namespace LanExchange.Application.Implementation.Menu
{
    [ImmutableObject(true)]
    public sealed class MenuSeparator : IMenuElement
    {
        public void Accept(IMenuElementVisitor visitor)
        {
            if (visitor == null) throw new ArgumentNullException(nameof(visitor));

            visitor.VisitSeparator();
        }
    }
}