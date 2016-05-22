using System;
using LanExchange.Plugin.Shortcut;
using LanExchange.Properties;
using LanExchange.SDK;
using LanExchange.Extensions;

namespace LanExchange.Plugin
{
    internal class PluginInternal : IPlugin
    {
        public void Initialize(IServiceProvider serviceProvider)
        {
            // register ShortcutPanelItem
            var imageManager = serviceProvider.Resolve<IImageManager>();
            imageManager.RegisterImage(PanelImageNames.SHORTCUT, Resources.keyboard_16, Resources.keyboard_16);

            var factoryManager = serviceProvider.Resolve<IPanelItemFactoryManager>();
            factoryManager.RegisterFactory<ShortcutRoot>(new PanelItemRootFactory<ShortcutRoot>());
            factoryManager.RegisterFactory<ShortcutPanelItem>(new ShortcutFactory());

            var panelFillers = serviceProvider.Resolve<IPanelFillerManager>();
            panelFillers.RegisterFiller<ShortcutPanelItem>(new ShortcutFiller());
        }
    }
}
