using LanExchange.Helpers;
using LanExchange.Interfaces.Services;
using LanExchange.SDK;
using System;
using System.Diagnostics;
using System.IO;

namespace LanExchange.Implementations.Services
{
    internal sealed class ConfigPersistenceService : IConfigPersistenceService
    {
        private readonly IFolderManager folderManager;

        public ConfigPersistenceService(IFolderManager folderManager)
        {
            this.folderManager = folderManager;
        }

        public TConfig Load<TConfig>() where TConfig : class
        {
            var fileName = folderManager.ConfigFileName;
            if (!File.Exists(fileName)) return default(TConfig);

            var temp = default(TConfig);
            try
            {
                var loaded = (TConfig)SerializeUtils.DeserializeObjectFromXmlFile(fileName, typeof(TConfig));
                if (loaded != null)
                    ReflectionUtils.CopyObjectProperties(loaded, temp);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            return temp;
        }

        public void Save<TConfig>(TConfig config) where TConfig : class
        {
            var fileName = folderManager.ConfigFileName;
            try
            {
                SerializeUtils.SerializeObjectToXmlFile(fileName, config);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }
    }
}
