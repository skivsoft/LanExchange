using System;
using System.ComponentModel.Composition;
using LanExchange.Application.Interfaces;
using LanExchange.Plugin.Shortcut;
using LanExchange.Plugin.Shortcut.Properties;
using LanExchange.Presentation.Interfaces;
using LanExchange.Presentation.Interfaces.Menu;

namespace LanExchange.Plugin
{
    [Export(typeof(IPlugin))]
    internal class PluginShortcut : IPlugin
    {
        public void Initialize(IServiceProvider serviceProvider)
        {
            var imageManager = serviceProvider.Resolve<IImageManager>();
            imageManager.RegisterImage(PanelImageNames.SHORTCUT, Resources.keyboard_16, Resources.keyboard_16);

            var factoryManager = serviceProvider.Resolve<IPanelItemFactoryManager>();
            factoryManager.RegisterFactory<ShortcutRoot>(new PanelItemRootFactory<ShortcutRoot>());
            factoryManager.RegisterFactory<ShortcutPanelItem>(new ShortcutFactory());

            var panelFillers = serviceProvider.Resolve<IPanelFillerManager>();
            var filler = new ShortcutFiller(
                serviceProvider.Resolve<ITranslationService>(),
                serviceProvider.Resolve<IAddonManager>(),
                serviceProvider.Resolve<IMenuProducer>());
            panelFillers.RegisterFiller<ShortcutPanelItem>(filler);
        }
    }
}