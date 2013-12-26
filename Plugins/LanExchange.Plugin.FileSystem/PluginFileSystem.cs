using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.SDK;

namespace LanExchange.Plugin.FileSystem
{
    public class PluginFileSystem : IPlugin
    {
        private IServiceProvider m_Provider;

        public void Initialize(IServiceProvider serviceProvider)
        {
            m_Provider = serviceProvider;

            // Setup resource manager
            //var translationService = (ITranslationService)m_Provider.GetService(typeof(ITranslationService));
            //if (translationService != null)
            //    translationService.SetResourceManagerTo<Resources>();

            // Register new panel item types
            var factoryManager = (IPanelItemFactoryManager)m_Provider.GetService(typeof(IPanelItemFactoryManager));
            if (factoryManager != null)
            {
                factoryManager.RegisterFactory<FileRoot>(new PanelItemRootFactory<FileRoot>());
                factoryManager.RegisterFactory<FilePanelItem>(new FileFactory());
            }

            // Register new panel fillers
            var fillerManager = (IPanelFillerManager)m_Provider.GetService(typeof(IPanelFillerManager));
            if (fillerManager != null)
            {
                fillerManager.RegisterFiller<FilePanelItem>(new FileFiller());
            }
        }
    }
}
