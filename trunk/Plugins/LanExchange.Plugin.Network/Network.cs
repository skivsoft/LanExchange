using System;
using LanExchange.Plugin.Network.Panel;
using LanExchange.SDK;

namespace LanExchange.Plugin.Network
{
    class Network : IPlugin
    {
        private IServiceProvider m_Provider;

        public void Initialize(IServiceProvider serviceProvider)
        {
            m_Provider = serviceProvider;

            // Register new panel item types
            var typeManager = (IPanelItemFactoryManager)m_Provider.GetService(typeof (IPanelItemFactoryManager));
            if (typeManager != null)
            {
                typeManager.RegisterPanelItemFactory(new Guid(DomainPanelItem.ID), new DomainPanelItemFactory());
                typeManager.RegisterPanelItemFactory(new Guid(ComputerPanelItem.ID), new ComputerPanelItemFactory());
                typeManager.RegisterPanelItemFactory(new Guid(SharePanelItem.ID), new SharePanelItemFactory());
            }

            // Register new panel fillers
            var fillerManager = (IPanelFillerStrategyManager) m_Provider.GetService(typeof (IPanelFillerStrategyManager));
            if (fillerManager != null)
            {
                fillerManager.RegisterPanelFillerStrategy(new DomainFillerStrategy());
                fillerManager.RegisterPanelFillerStrategy(new ComputerFillerStrategy());
                fillerManager.RegisterPanelFillerStrategy(new ShareFillerStrategy());
            }
        }

        public ISettingsTabViewFactory GetSettingsTabViewFactory()
        {
            return new SettingsTabNetworkFactory();
        }

        public void OpenDefaultTab()
        {
            //var domain = NetApi32Utils.Instance.GetMachineNetBiosDomain(null);
            //var Info = new PanelItemList(domain);
            //Info.Groups.Add(new DomainPanelItem(domain));
            //AddTab(Info);
        }
    }
}
