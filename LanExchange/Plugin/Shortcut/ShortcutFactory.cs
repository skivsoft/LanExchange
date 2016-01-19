using System;
using LanExchange.Properties;
using LanExchange.SDK;

namespace LanExchange.Plugin.Shortcut
{
    public sealed class ShortcutFactory : IPanelItemFactory
    {
        public PanelItemBase CreatePanelItem(PanelItemBase parent, string name)
        {
            return new ShortcutPanelItem(parent, name);
        }

        public PanelItemBase CreateDefaultRoot()
        {
            return null;
        }

        public Func<PanelItemBase, bool> GetAvailabilityChecker()
        {
            return null;
        }

        public void RegisterColumns(IPanelColumnManager columnManager)
        {
            columnManager.RegisterColumn<ShortcutPanelItem>(new PanelColumnHeader(Resources.mHelpKeys_Text, 100));
            columnManager.RegisterColumn<ShortcutPanelItem>(new PanelColumnHeader(Resources.Action, 280));
            columnManager.RegisterColumn<ShortcutPanelItem>(new PanelColumnHeader(Resources.Context, 80));
        }
    }
}