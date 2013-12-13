using System;
using LanExchange.Plugin.Network.Properties;
using LanExchange.SDK;

namespace LanExchange.Plugin.Network
{
    public sealed class PluginNetwork : IPlugin
    {
        public static readonly PanelItemRoot ROOT_OF_DOMAINS = new PanelItemRoot();
        private IServiceProvider m_Provider;

        public void Initialize(IServiceProvider serviceProvider)
        {
            if (EnvironmentUtils.IsRunningOnMono())
                return;
            
            m_Provider = serviceProvider;
            
            // Setup resource manager
            var translationService = (ITranslationService)m_Provider.GetService(typeof(ITranslationService));
            if (translationService != null)
                translationService.SetResourceManagerTo<Resources>();

            // Register new panel item types
            var factoryManager = (IPanelItemFactoryManager)m_Provider.GetService(typeof (IPanelItemFactoryManager));
            if (factoryManager != null)
            {
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
        }
    }
}
