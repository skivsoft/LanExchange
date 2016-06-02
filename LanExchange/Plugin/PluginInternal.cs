using System;
using LanExchange.Application.Interfaces;
using LanExchange.Plugin.Shortcut;
using LanExchange.Properties;
using LanExchange.SDK;
using LanExchange.SDK.Extensions;
using LanExchange.Interfaces;

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

            var addonManager = serviceProvider.Resolve<IAddonManager>();
            var translationService = serviceProvider.Resolve<ITranslationService>();
            var panelFillers = serviceProvider.Resolve<IPanelFillerManager>();
            panelFillers.RegisterFiller<ShortcutPanelItem>(new ShortcutFiller(translationService, addonManager));
        }
    }
}
