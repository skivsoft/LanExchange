using System;
using System.Collections.Generic;
using System.ComponentModel;
using LanExchange.Presentation.Interfaces.Menu;

namespace LanExchange.Application.Implementation.Menu
{
    [ImmutableObject(true)]
    public sealed class MenuGroup : IMenuElement
    {
        private readonly string text;
        private readonly IEnumerable<IMenuElement> elements;

        public MenuGroup(string text, params IMenuElement[] elements) 
        {
            if (text == null) throw new ArgumentNullException(nameof(text));
            if (elements == null) throw new ArgumentNullException(nameof(elements));

            this.text = text;
            this.elements = elements;
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