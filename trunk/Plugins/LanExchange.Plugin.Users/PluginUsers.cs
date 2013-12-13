using System;
using LanExchange.Plugin.Users.Properties;
using LanExchange.SDK;

namespace LanExchange.Plugin.Users
{
    public sealed class PluginUsers : IPlugin
    {
        public const string LDAP_PREFIX = "LDAP://";

        private IServiceProvider m_Provider;

        public void Initialize(IServiceProvider serviceProvider)
        {
            m_Provider = serviceProvider;

            // Setup resource manager
            var translationService = (ITranslationService)m_Provider.GetService(typeof(ITranslationService));
            if (translationService != null)
                translationService.SetResourceManagerTo<Resources>();

            // Register new panel item types
            var factoryManager = (IPanelItemFactoryManager)m_Provider.GetService(typeof(IPanelItemFactoryManager));
            if (factoryManager != null)
            {
                factoryManager.RegisterFactory<UserRoot>(new PanelItemRootFactory<UserRoot>());
                factoryManager.RegisterFactory<UserPanelItem>(new UserFactory());
            }

            // Register new panel fillers
            var fillerManager = (IPanelFillerManager)m_Provider.GetService(typeof(IPanelFillerManager));
            if (fillerManager != null)
            {
                fillerManager.RegisterFiller<UserPanelItem>(new UserFiller());
            }
        }
    }
}