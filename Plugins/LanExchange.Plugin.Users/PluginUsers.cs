using System;
using LanExchange.Plugin.Users.Properties;
using LanExchange.SDK;

namespace LanExchange.Plugin.Users
{
    public sealed class PluginUsers : IPlugin
    {
        public static readonly PanelItemBase ROOT_OF_DNS = new PanelItemRoot("DNSPanelItem");
        public const string LDAP_PREFIX = "LDAP://";

        private IServiceProvider m_Provider;

        public void Initialize(IServiceProvider serviceProvider)
        {
            m_Provider = serviceProvider;

            var translationService = (ITranslationService)m_Provider.GetService(typeof(ITranslationService));
            if (translationService != null)
                translationService.SetResourceManagerTo<Resources>();

            // Register new panel item types
            var typesManager = (IPanelItemFactoryManager)m_Provider.GetService(typeof(IPanelItemFactoryManager));
            if (typesManager != null)
            {
                typesManager.RegisterPanelItemFactory(typeof(UserPanelItem), new UserPanelItemFactory());
            }

            // Register columns for panel item types
            var columnManager = (IPanelColumnManager)m_Provider.GetService(typeof(IPanelColumnManager));
            if (columnManager != null)
            {
                UserPanelItem.RegisterColumns(columnManager);
            }

            // Register new panel fillers
            var fillerManager = (IPanelFillerManager)m_Provider.GetService(typeof(IPanelFillerManager));
            if (fillerManager != null)
            {
                fillerManager.RegisterPanelFiller(new UserFiller());
            }
        }
    }
}
