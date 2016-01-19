using System;
using System.ComponentModel.Composition;

using LanExchange.Plugin.Network.Properties;
using LanExchange.SDK;

namespace LanExchange.Plugin.Network
{
    [Export(typeof(IPlugin))]
    public sealed class PluginNetwork : IPlugin
    {
        private IServiceProvider m_Provider;
        public static IIPHLPAPISerivice IPHLPAPI { get; private set; }

        public void Initialize(IServiceProvider serviceProvider)
        {
            m_Provider = serviceProvider;
            
            // Setup resource manager
            var translationService = (ITranslationService)m_Provider.GetService(typeof(ITranslationService));
            if (translationService != null)
                translationService.SetResourceManagerTo<Resources>();

            // Register new panel item types
            var factoryManager = (IPanelItemFactoryManager)m_Provider.GetService(typeof (IPanelItemFactoryManager));
            if (factoryManager != null)
            {
                factoryManager.RegisterFactory<DomainRoot>(new PanelItemRootFactory<DomainRoot>());
                factoryManager.RegisterFactory<DomainPanelItem>(new DomainFactory());
                factoryManager.RegisterFactory<ComputerPanelItem>(new ComputerFactory());
                factoryManager.RegisterFactory<SharePanelItem>(new ShareFactory());
            }

            // Register new panel fillers
            var fillerManager = (IPanelFillerManager) m_Provider.GetService(typeof (IPanelFillerManager));
            if (fillerManager != null)
            {
                fillerManager.RegisterFiller<DomainPanelItem>(new DomainFiller());
                fillerManager.RegisterFiller<ComputerPanelItem>(new ComputerFiller());
                fillerManager.RegisterFiller<SharePanelItem>(new ShareFiller());
            }

            IPHLPAPI = (IIPHLPAPISerivice)m_Provider.GetService(typeof (IIPHLPAPISerivice));
        }
    }
}
