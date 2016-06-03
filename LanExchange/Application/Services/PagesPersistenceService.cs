using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using LanExchange.Application.Interfaces;
using LanExchange.Application.Interfaces.Services;
using LanExchange.Application.Models;
using LanExchange.Presentation.Interfaces;
using LanExchange.SDK;

namespace LanExchange.Application.Services
{
    internal sealed class PagesPersistenceService : IPagesPersistenceService
    {
        private readonly IFolderManager folderManager;
        private readonly IPanelItemFactoryManager factoryManager;

        public PagesPersistenceService(
            IFolderManager folderManager, 
            IPanelItemFactoryManager factoryManager)
        {
            Contract.Requires<ArgumentNullException>(folderManager != null);
            Contract.Requires<ArgumentNullException>(factoryManager != null);

            this.folderManager = folderManager;
            this.factoryManager = factoryManager;
        }

        public void LoadSettings(out IPagesModel pages)
        {
            var fileFName = folderManager.TabsConfigFileName;
            pages = null;
            if (File.Exists(fileFName))
                try
                {
                    pages =
                        (PagesModel)
                        SerializeUtils.DeserializeObjectFromXmlFile(fileFName, typeof(PagesModel),
                                                                    factoryManager.ToArray());
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                }
        }

        public void SaveSettings(IPagesModel pages)
        {
            try
            {
                SerializeUtils.SerializeObjectToXmlFile(folderManager.TabsConfigFileName, pages, factoryManager.ToArray());
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }
    }
}