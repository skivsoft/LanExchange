using System;
using System.Collections.Generic;
using LanExchange.Presentation.Interfaces;
using LanExchange.Presentation.Interfaces.Menu;

namespace LanExchange.Plugin.Shortcut
{
    internal sealed class FillerMenuVisitor : IMenuElementVisitor
    {
        private readonly PanelItemBase parent;
        private readonly ICollection<PanelItemBase> result;

        public FillerMenuVisitor(PanelItemBase parent, ICollection<PanelItemBase> result)
        {
            if (parent == null) throw new ArgumentNullException(nameof(parent));
            if (result == null) throw new ArgumentNullException(nameof(result));

            this.parent = parent;
            this.result = result;
        }

        public void VisitMenuElement(string text, string shortcut, ICommand command, bool isDefault)
        {
            result.Add(new ShortcutPanelItem(parent, shortcut, text));
        }

        public void VisitMenuGroup(string text)
        {
        }

        public void VisitSeparator()
        {
        }
    }
}