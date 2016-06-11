using System;
using System.ComponentModel.Composition;
using LanExchange.Plugin.Shortcut;
using LanExchange.Presentation.Interfaces;
using LanExchange.Presentation.Interfaces.Extensions;
using LanExchange.Properties;

namespace LanExchange.Plugin
{
    [Export(typeof(IPlugin))]
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

            var addonManager = serviceProvider.Resolve<IAddonManager>();
            var translationService = serviceProvider.Resolve<ITranslationService>();
            var panelFillers = serviceProvider.Resolve<IPanelFillerManager>();
            panelFillers.RegisterFiller<ShortcutPanelItem>(new ShortcutFiller(translationService, addonManager));
        }
    }
}
