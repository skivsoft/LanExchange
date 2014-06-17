using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using LanExchange.Ioc;
using LanExchange.SDK;

namespace LanExchange.Misc.Impl
{
    public class PluginManagerImpl : IPluginManager
    {
        private const string PLUGIN_MASK = "LanExchange.Plugin.*.dll";
        private const string IPLUGIN_INTERFACE = "LanExchange.SDK.IPlugin";
        private const string PLUGIN_TYPE_PREFIX = "Plugin";
        private readonly IList<IPlugin> m_Plugins;
        private readonly IDictionary<string, string> m_PluginsAuthors;

        public PluginManagerImpl()
        {
            m_Plugins = new List<IPlugin>();
            m_PluginsAuthors = new Dictionary<string, string>();
        }

        public void LoadPlugins()
        {
            var files = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Lib"), PLUGIN_MASK, SearchOption.TopDirectoryOnly);
            foreach (var fileName in files)
                try
                {
                    LoadPlugin(fileName);
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                }
        }

        [Localizable(false)]
        private void LoadPlugin(string fileName)
        {
            var assembly = Assembly.LoadFile(fileName);
            var fileNameWithoutPath = Path.GetFileName(fileName);
            if (fileNameWithoutPath == null) return;
            var attributes = assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
            var author = attributes.Length == 0 ? "" : ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            var name = Path.GetFileNameWithoutExtension(fileNameWithoutPath);
            var parts = name.Split('.');
            if (parts.Length < 3) return;
            name = name + "." + PLUGIN_TYPE_PREFIX + parts[parts.Length - 1];
            var type = assembly.GetType(name);//, false, true);
            if (type == null) return;
            var iface = type.GetInterface(IPLUGIN_INTERFACE);
            if (iface == null) return;
            IPlugin plugin = null;
            try
            {
                plugin = (IPlugin)Activator.CreateInstance(type);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            if (plugin == null) return;
            try
            {
                m_PluginsAuthors.Add(fileNameWithoutPath, author);
                m_Plugins.Add(plugin);
                plugin.Initialize(App.Resolve<IServiceProvider>());
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }

        public IList<IPlugin> Items
        {
            get { return m_Plugins; }
        }

        public IDictionary<string, string> PluginsAuthors
        {
            get { return m_PluginsAuthors; }
        }

    }
}