using LanExchange.Presentation.Interfaces;
using LanExchange.Presentation.Interfaces.Menu;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace LanExchange.Plugin.Shortcut
{
    internal sealed class FillerMenuVisitor : IMenuElementVisitor
    {
        private readonly PanelItemBase parent;
        private readonly ICollection<PanelItemBase> result;

        public FillerMenuVisitor(PanelItemBase parent, ICollection<PanelItemBase> result)
        {
            Contract.Requires<ArgumentNullException>(parent != null);
            Contract.Requires<ArgumentNullException>(result != null);

            this.parent = parent;
            this.result = result;
        }

        public void VisitMenuElement(string text, string shortcut)
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