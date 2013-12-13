using System;
using LanExchange.Misc;
using LanExchange.Properties;
using LanExchange.SDK;

namespace LanExchange.Action
{
    internal class PluginInternal : IPlugin
    {
        public void Initialize(IServiceProvider serviceProvider)
        {
            // register ShortcutPanelItem
            App.Images.RegisterImage(PanelImageNames.SHORTCUT, Resources.keyboard_16, Resources.keyboard_16);
            App.PanelItemTypes.RegisterFactory<ShortcutPanelItem>(new ShortcutFactory());
            App.PanelFillers.RegisterFiller<ShortcutPanelItem>(new ShortcutFiller());
        }
    }
}
