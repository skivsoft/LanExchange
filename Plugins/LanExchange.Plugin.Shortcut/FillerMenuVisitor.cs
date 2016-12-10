using LanExchange.Presentation.Interfaces;
using LanExchange.Presentation.Interfaces.Menu;
using System;
using System.Collections.Generic;

namespace LanExchange.Plugin.Shortcut
{
    internal sealed class FillerMenuVisitor : IMenuElementVisitor
    {
        private readonly PanelItemBase parent;
        private readonly ICollection<PanelItemBase> result;

        public FillerMenuVisitor(PanelItemBase parent, ICollection<PanelItemBase> result)
        {
            this.parent = parent ?? throw new ArgumentNullException(nameof(parent));
            this.result = result ?? throw new ArgumentNullException(nameof(result));
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