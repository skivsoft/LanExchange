using System;
using System.IO;
using LanExchange.Application.Interfaces;
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
            this.folderManager = folderManager ?? throw new ArgumentNullException(nameof(folderManager));
            this.factoryManager = factoryManager ?? throw new ArgumentNullException(nameof(factoryManager));
            this.serializeService = serializeService ?? throw new ArgumentNullException(nameof(serializeService));
            this.logService = logService ?? throw new ArgumentNullException(nameof(logService));
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
    }
}