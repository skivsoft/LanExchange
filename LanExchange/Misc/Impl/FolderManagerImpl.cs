using System;
using System.IO;
using System.Linq;
using LanExchange.SDK;

namespace LanExchange.Misc.Impl
{
    /// <summary>
    /// This singleton class provides all directory names and config filenames for program.
    /// </summary>
    public class FolderManagerImpl : IFolderManager
    {
        public const string PROGRAM_DIR = "LanExchange";
        public const string CONFIG_DIR = "Config";
        public const string ADDONS_DIR = "Addons";
        public const string TABS_FILE = "Tabs.cfg";
        public const string ADDONS_EXT = ".xml";

        private readonly string m_CurrentPath;
        private readonly string m_ExeFileName;
        private readonly string m_ConfigFileName;
        private readonly string m_TabsConfigFileName;
        private readonly string m_SystemAddonsPath;
        private readonly string m_UserAddonsPath;

        private FolderManagerImpl()
        {
            var args = Environment.GetCommandLineArgs();
            m_ExeFileName = args.Length > 0 ? args[0] : string.Empty;
            m_CurrentPath = Path.GetDirectoryName(m_ExeFileName) ?? string.Empty;
            var fileName = Path.ChangeExtension(Path.GetFileName(m_ExeFileName), ".cfg");
            var configDir = Path.Combine(m_CurrentPath, CONFIG_DIR);
            m_ConfigFileName = Path.Combine(configDir, fileName);
            m_TabsConfigFileName = Path.Combine(configDir, TABS_FILE);
            m_SystemAddonsPath = Path.Combine(m_CurrentPath, ADDONS_DIR);
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            m_UserAddonsPath = Path.Combine(Path.Combine(appData, PROGRAM_DIR), ADDONS_DIR);
        }

        public string CurrentPath
        {
            get { return m_CurrentPath; }
        }

        public string ConfigFileName
        {
            get { return m_ConfigFileName; }
        }

        public string ExeFileName
        {
            get { return m_ExeFileName; }
        }

        public string TabsConfigFileName
        {
            get { return m_TabsConfigFileName; }
        }

        public string SystemAddonsPath
        {
            get { return m_SystemAddonsPath; }
        }

        public string UserAddonsPath
        {
            get { return m_UserAddonsPath; }
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
            if (Directory.Exists(m_SystemAddonsPath))
                sysAddons = Directory.GetFiles(m_SystemAddonsPath, "*" + ADDONS_EXT, SearchOption.AllDirectories);
            if (Directory.Exists(m_UserAddonsPath))
                userAddons = Directory.GetFiles(m_UserAddonsPath, "*" + ADDONS_EXT, SearchOption.AllDirectories);
            return sysAddons.Union(userAddons).ToArray();
        }

        public string GetAddonFileName(bool isSystem, string addonName)
        {
            return Path.Combine(isSystem ? SystemAddonsPath : UserAddonsPath, addonName + ADDONS_EXT);
        }
    }
}
