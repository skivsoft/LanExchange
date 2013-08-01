using System;
using LanExchange.Plugin.Users.Panel;
using LanExchange.SDK;

namespace LanExchange.Plugin.Users
{
    internal class Users : IPlugin
    {
        private IServiceProvider m_Provider;

        public void Initialize(IServiceProvider serviceProvider)
        {
            m_Provider = serviceProvider;

            // Register new panel item types
            var typesManager = (IPanelItemFactoryManager)m_Provider.GetService(typeof(IPanelItemFactoryManager));
            if (typesManager != null)
            {
                typesManager.RegisterPanelItemFactory(typeof(OrgUnitPanelItem), new OrgUnitPanelItemFactory());
                typesManager.RegisterPanelItemFactory(typeof(UserPanelItem), new UserPanelItemFactory());
            }

            // Register new panel fillers
            var fillerManager = (IPanelFillerStrategyManager)m_Provider.GetService(typeof(IPanelFillerStrategyManager));
            if (fillerManager != null)
            {
                fillerManager.RegisterPanelFillerStrategy(new OrgUnitFillerStrategy());
                fillerManager.RegisterPanelFillerStrategy(new UserFillerStrategy());
            }
        }


        public ISettingsTabViewFactory GetSettingsTabViewFactory()
        {
            return new SettingsTabUsersFactory();
        }


        public void OpenDefaultTab()
        {
        }
    }
}
