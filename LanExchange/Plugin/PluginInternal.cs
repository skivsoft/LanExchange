using System;
using LanExchange.Plugin.Shortcut;
using LanExchange.Properties;
using LanExchange.SDK;

namespace LanExchange.Plugin
{
    internal class PluginInternal : IPlugin
    {
        public void Initialize(IServiceProvider serviceProvider)
        {
            // register ShortcutPanelItem
            App.Images.RegisterImage(PanelImageNames.SHORTCUT, Resources.keyboard_16, Resources.keyboard_16);

            var factoryManager = App.Resolve<IPanelItemFactoryManager>();
            factoryManager.RegisterFactory<ShortcutRoot>(new PanelItemRootFactory<ShortcutRoot>());
            factoryManager.RegisterFactory<ShortcutPanelItem>(new ShortcutFactory());

            App.PanelFillers.RegisterFiller<ShortcutPanelItem>(new ShortcutFiller());
        }
    }
}
