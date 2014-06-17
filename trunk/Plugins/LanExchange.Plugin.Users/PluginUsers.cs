using System;
using LanExchange.Plugin.Users.Properties;
using LanExchange.SDK;

namespace LanExchange.Plugin.Users
{
    public sealed class PluginUsers : IPlugin
    {
        public const string LDAP_PREFIX = "LDAP://";

        public static IScreenService ScreenService;

        public void Initialize(IServiceProvider serviceProvider)
        {
            var provider = serviceProvider;

            ScreenService = (IScreenService) provider.GetService(typeof (IScreenService));

            // Setup resource manager
            var translationService = (ITranslationService)provider.GetService(typeof(ITranslationService));
            if (translationService != null)
                translationService.SetResourceManagerTo<Resources>();

            // Register new panel item types
            var factoryManager = (IPanelItemFactoryManager)provider.GetService(typeof(IPanelItemFactoryManager));
            if (factoryManager != null)
            {
                factoryManager.RegisterFactory<UserRoot>(new PanelItemRootFactory<UserRoot>());
                factoryManager.RegisterFactory<UserPanelItem>(new UserFactory());

                factoryManager.RegisterFactory<WorkspaceRoot>(new PanelItemRootFactory<WorkspaceRoot>());
                factoryManager.RegisterFactory<WorkspacePanelItem>(new WorkspaceFactory());
            }

            // Register new panel fillers
            var fillerManager = (IPanelFillerManager)provider.GetService(typeof(IPanelFillerManager));
            if (fillerManager != null)
            {
                fillerManager.RegisterFiller<UserPanelItem>(new UserFiller());
                fillerManager.RegisterFiller<WorkspacePanelItem>(new WorkspaceFiller());
            }
        }
    }
}