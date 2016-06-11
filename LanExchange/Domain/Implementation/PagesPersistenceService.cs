using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using LanExchange.Application.Interfaces;
using LanExchange.Application.Models;
using LanExchange.Domain.Interfaces;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Domain.Implementation
{
    internal sealed class PagesPersistenceService : IPagesPersistenceService
    {
        private readonly IFolderManager folderManager;
        private readonly IPanelItemFactoryManager factoryManager;
        private readonly ISerializeService serializeService;
        private readonly ILogService logService;

        public PagesPersistenceService(
            IFolderManager folderManager, 
            IPanelItemFactoryManager factoryManager,
            ISerializeService serializeService,
            ILogService logService)
        {
            Contract.Requires<ArgumentNullException>(folderManager != null);
            Contract.Requires<ArgumentNullException>(factoryManager != null);
            Contract.Requires<ArgumentNullException>(serializeService != null);
            Contract.Requires<ArgumentNullException>(logService != null);

            this.folderManager = folderManager;
            this.factoryManager = factoryManager;
            this.serializeService = serializeService;
            this.logService = logService;
        }

        private static void ForceCreatePath(string fileName)
        {
            var path = Path.GetDirectoryName(fileName);
            if (path != null && !Directory.Exists(path))
            {
                ForceCreatePath(path);
                Directory.CreateDirectory(path);
            }
        }

        private Type[] GetExtraTypes()
        {
            return factoryManager.ToArray();
        }

        public void LoadSettings(out IPagesModel pages)
        {
            var fileName = folderManager.TabsConfigFileName;
            pages = null;
            if (File.Exists(fileName))
                try
                {
                    var dto = serializeService.DeserializeFromXmlFile<PagesDto>(fileName, GetExtraTypes());
                    //pages = dto.ToModel();
                }
                catch (Exception exception)
                {
                    logService.Log(exception);
                }
        }

        public void SaveSettings(IPagesModel pages)
        {
            try
            {
                var fileName = folderManager.TabsConfigFileName;
                ForceCreatePath(fileName);
                serializeService.SerializeToXmlFile(fileName, pages.ToDto(), GetExtraTypes());
            }
            catch (Exception exception)
            {
                logService.Log(exception);
            }
        }
    }
}