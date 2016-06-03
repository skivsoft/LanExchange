using System;
using System.Collections.Generic;
using System.IO;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Implementation
{
    /// <summary>
    /// This singleton class provides all directory names and config filenames for program.
    /// </summary>
    /// <remarks>
    /// This class public because it uses from GenerateEnglishProcess tool.
    /// </remarks>
    public sealed class FolderManager : IFolderManager
    {
        public const string PROGRAM_DIR = "LanExchange";
        public const string CONFIG_DIR = "Config";
        public const string ADDONS_DIR = "Addons";
        public const string LANGUAGES_DIR = "Languages";
        public const string TABS_FILE = "Tabs.cfg";
        public const string ADDONS_EXT = ".xml";
        public const string LANGUAGES_EXT = ".po";

        private readonly string currentPath;
        private readonly string exeFileName;
        private readonly string configFileName;
        private readonly string tabsConfigFileName;
        private readonly string systemAddonsPath;
        private readonly string userAddonsPath;
        private readonly string languagesPath;

        public FolderManager()
        {
            var args = Environment.GetCommandLineArgs();
            exeFileName = args.Length > 0 ? args[0] : string.Empty;
            currentPath = Path.GetDirectoryName(exeFileName) ?? string.Empty;
            systemAddonsPath = Path.Combine(currentPath, ADDONS_DIR);
            languagesPath = Path.Combine(currentPath, LANGUAGES_DIR);
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var programDir = Path.Combine(appData, PROGRAM_DIR);
            userAddonsPath = Path.Combine(programDir, ADDONS_DIR);
            var configDir = Path.Combine(programDir, CONFIG_DIR);
            var fileName = Path.ChangeExtension(Path.GetFileName(exeFileName), ".cfg");
            configFileName = Path.Combine(configDir, fileName);
            tabsConfigFileName = Path.Combine(configDir, TABS_FILE);
        }

        public string CurrentPath
        {
            get { return currentPath; }
        }

        public string ConfigFileName
        {
            get { return configFileName; }
        }

        public string ExeFileName
        {
            get { return exeFileName; }
        }

        public string TabsConfigFileName
        {
            get { return tabsConfigFileName; }
        }

        public string SystemAddonsPath
        {
            get { return systemAddonsPath; }
        }

        public string UserAddonsPath
        {
            get { return userAddonsPath; }
        }

        /// <summary>
        /// Returns list of addons from system related folder Addons combined 
        /// with addons from user-related folder Addons.
        /// </summary>
        /// <returns>Array of filenames of addons.</returns>
        public string[] GetAddonsFiles()
        {
            var sysAddons = new string[0];
            var userAddons = new string[0];
            if (Directory.Exists(systemAddonsPath))
                sysAddons = Directory.GetFiles(systemAddonsPath, "*" + ADDONS_EXT, SearchOption.AllDirectories);
            if (Directory.Exists(userAddonsPath))
                userAddons = Directory.GetFiles(userAddonsPath, "*" + ADDONS_EXT, SearchOption.AllDirectories);
            var result = new List<string>();
            result.AddRange(sysAddons);
            result.AddRange(userAddons);
            return result.ToArray();
        }

        public string[] GetLanguagesFiles()
        {
            var languages = new string[0];
            if (Directory.Exists(languagesPath))
                languages = Directory.GetFiles(languagesPath, "*" + LANGUAGES_EXT, SearchOption.TopDirectoryOnly);
            return languages;
        }

        public string GetAddonFileName(bool isSystem, string addonName)
        {
            return Path.Combine(isSystem ? SystemAddonsPath : UserAddonsPath, addonName + ADDONS_EXT);
        }
    }
}
