using System;
using LanExchange.Plugin.Users.Panel;
using LanExchange.SDK;

namespace LanExchange.Plugin.Users
{
    internal class Users : IPlugin
    {
        public static PanelItemBase ROOT_OF_ORGUNITS = new PanelItemRoot();
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

            // Register columns for panel item types
            var columnManager = (IPanelColumnManager)m_Provider.GetService(typeof(IPanelColumnManager));
            if (columnManager != null)
            {
                OrgUnitPanelItem.RegisterColumns(columnManager);
                UserPanelItem.RegisterColumns(columnManager);
            }

            // Register new panel fillers
            var fillerManager = (IPanelFillerManager)m_Provider.GetService(typeof(IPanelFillerManager));
            if (fillerManager != null)
            {
                fillerManager.RegisterPanelFiller(new OrgUnitFiller());
                fillerManager.RegisterPanelFiller(new UserFiller());
            }
        }


        //public ISettingsTabViewFactory GetSettingsTabViewFactory()
        //{
        //    return new SettingsTabUsersFactory();
        //}


        public void OpenDefaultTab()
        {
        }
    }
}
