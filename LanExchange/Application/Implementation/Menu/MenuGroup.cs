using LanExchange.Presentation.Interfaces.Menu;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace LanExchange.Application.Implementation.Menu
{
    [ImmutableObject(true)]
    internal sealed class MenuGroup : IMenuElement
    {
        private readonly string text;
        private readonly IEnumerable<IMenuElement> elements;

        public MenuGroup(string text, params IMenuElement[] elements) 
        {
            this.text = text ?? throw new ArgumentNullException(nameof(text));
            this.elements = elements ?? throw new ArgumentNullException(nameof(elements));
        }

        public MenuGroup(params IMenuElement[] elements) : this(string.Empty, elements)
        {
        }

        public void Accept(IMenuElementVisitor visitor)
        {
            if (visitor == null) throw new ArgumentNullException(nameof(visitor));

            if (!string.IsNullOrEmpty(text))
                visitor.VisitMenuGroup(text);

            foreach (var element in elements)
                element.Accept(visitor);
        }
    }
}