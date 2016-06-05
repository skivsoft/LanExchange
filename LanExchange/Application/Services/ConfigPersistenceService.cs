using System;
using System.Diagnostics;
using System.IO;
using LanExchange.Application.Interfaces.Services;
using LanExchange.Presentation.Interfaces;
using LanExchange.Presentation.Interfaces.Models;
using LanExchange.Presentation.WinForms.Helpers;

namespace LanExchange.Application.Services
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
                var loaded = (TConfig)SerializeHelper.DeserializeObjectFromXmlFile(fileName, typeof(TConfig));
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
                SerializeHelper.SerializeObjectToXmlFile(fileName, config);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }
    }
}
