using System;
using System.IO;
using LanExchange.Application.Interfaces;
using LanExchange.Domain.Interfaces;

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
            if (folderManager == null) throw new ArgumentNullException(nameof(folderManager));
            if (factoryManager == null) throw new ArgumentNullException(nameof(factoryManager));
            if (serializeService == null) throw new ArgumentNullException(nameof(serializeService));
            if (logService == null) throw new ArgumentNullException(nameof(logService));

            this.folderManager = folderManager;
            this.factoryManager = factoryManager;
            this.serializeService = serializeService;
            this.logService = logService;
        }

        public PagesDto LoadPages()
        {
            var fileName = folderManager.TabsConfigFileName;
            if (File.Exists(fileName))
                try
                {
                    return serializeService.DeserializeFromFile<PagesDto>(fileName, GetExtraTypes());
                }
                catch (Exception exception)
                {
                    logService.Log(exception);
                }

            return PagesDto.Empty;
        }

        public void SavePages(PagesDto pages)
        {
            try
            {
                var fileName = folderManager.TabsConfigFileName;
                ForceCreatePath(fileName);
                serializeService.SerializeToFile(fileName, pages, GetExtraTypes());
            }
            catch (Exception exception)
            {
                logService.Log(exception);
            }
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
    }
}