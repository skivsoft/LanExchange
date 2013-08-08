using System;
using LanExchange.Plugin.Network.Panel;
using LanExchange.SDK;

namespace LanExchange.Plugin.Network
{
    public class Network : IPlugin
    {
        public static PanelItemBase ROOT_OF_DOMAINS = new PanelItemRoot();
        private IServiceProvider m_Provider;

        public void Initialize(IServiceProvider serviceProvider)
        {
            m_Provider = serviceProvider;

            // Register new panel item types
            var typeManager = (IPanelItemFactoryManager)m_Provider.GetService(typeof (IPanelItemFactoryManager));
            if (typeManager != null)
            {
                typeManager.RegisterPanelItemFactory(typeof(DomainPanelItem), new DomainPanelItemFactory());
                typeManager.RegisterPanelItemFactory(typeof(ComputerPanelItem), new ComputerPanelItemFactory());
                typeManager.RegisterPanelItemFactory(typeof(SharePanelItem), new SharePanelItemFactory());
            }

            // Register columns for panel item types
            var columnManager = (IPanelColumnManager) m_Provider.GetService(typeof(IPanelColumnManager));
            if (columnManager != null)
            {
                DomainPanelItem.RegisterColumns(columnManager);
                ComputerPanelItem.RegisterColumns(columnManager);
                SharePanelItem.RegisterColumns(columnManager);
            }

            // Register new panel fillers
            var fillerManager = (IPanelFillerManager) m_Provider.GetService(typeof (IPanelFillerManager));
            if (fillerManager != null)
            {
                fillerManager.RegisterPanelFiller(new DomainFiller());
                fillerManager.RegisterPanelFiller(new ComputerFiller());
                fillerManager.RegisterPanelFiller(new ShareFiller());
            }
        }
    }
}
