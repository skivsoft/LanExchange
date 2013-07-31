using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using LanExchange.SDK;

namespace LanExchange
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
            var folder = AppDomain.CurrentDomain.BaseDirectory;
            var files = Directory.GetFiles(folder, PluginMask, SearchOption.TopDirectoryOnly);
            foreach (string file in files)
            try
            {
                var assembly = Assembly.LoadFile(file);
                //PanelSubscription.Instance.StrategySelector.SearchStrategiesInAssembly(assembly, typeof(PanelStrategyBase));
                foreach (Type type in assembly.GetTypes())
                {
                    Type iface = type.GetInterface("LanExchange.SDK.IPlugin");
                    if (iface == null) continue;
                    IPlugin plugin = null;
                    try
                    {
                        plugin = (IPlugin)Activator.CreateInstance(type);
                        plugin.Initialize(new ServiceProvider());
                    }
                    catch (Exception)
                    {
                    }
                    if (plugin == null) continue;
                    // save plugin's interface
                    m_Plugins.Add(plugin);
                }
            }
            catch (Exception)
            {
            }
        }

        public IList<IPlugin> Items
        {
            get { return m_Plugins; }
        }

        internal void OpenDefaultTab()
        {
            lock (m_Plugins)
            {
                foreach (var plugin in m_Plugins)
                    plugin.OpenDefaultTab();
            }
        }
    }
}
