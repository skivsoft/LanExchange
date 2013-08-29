using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using LanExchange.SDK;

namespace LanExchange.Misc
{
    public class PluginManager
    {
        private const string PluginMask = "LanExchange.Plugin.*.dll";
        private readonly IList<IPlugin> m_Plugins;

        public PluginManager()
        {
            m_Plugins = new List<IPlugin>();
        }

        public void LoadPlugins()
        {
            var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, PluginMask, SearchOption.TopDirectoryOnly);
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

        public void LoadPlugin(string fileName)
        {
            var assembly = Assembly.LoadFile(fileName);
            foreach (var type in assembly.GetExportedTypes())
            {
                var iface = type.GetInterface("LanExchange.SDK.IPlugin");
                if (iface == null) continue;
                IPlugin plugin = null;
                try
                {
                    plugin = (IPlugin)Activator.CreateInstance(type);
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                }
                if (plugin == null) continue;
                try
                {
                    m_Plugins.Add(plugin);
                    plugin.Initialize(App.ServiceProvider);
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                }
            }
        }

        public IList<IPlugin> Items
        {
            get { return m_Plugins; }
        }
    }
}
