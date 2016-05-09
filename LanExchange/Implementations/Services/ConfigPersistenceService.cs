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

        public TConfig Load<TConfig>() where TConfig : ConfigBase, new()
        {
            var result = new TConfig();
            var fileName = folderManager.ConfigFileName;
            if (!File.Exists(fileName)) return result;

            try
            {
                var loaded = (TConfig)SerializeUtils.DeserializeObjectFromXmlFile(fileName, typeof(TConfig));
                if (loaded != null)
                    ReflectionUtils.CopyObjectProperties(loaded, result);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            return result;
        }

        public void Save<TConfig>(TConfig config) where TConfig : ConfigBase
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
