using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using LanExchange.SDK;

namespace LanExchange
{
    public class PluginLoader
    {
        private const string PluginMask = "Plugin.*.dll";
        private readonly IList<IPlugin> m_Plugins;

        public PluginLoader()
        {
            m_Plugins = new List<IPlugin>();
        }

        public void LoadPlugins()
        {
            var folder = AppDomain.CurrentDomain.BaseDirectory;
            if (!Directory.Exists(folder)) return;
            var files = Directory.GetFiles(folder, PluginMask, SearchOption.TopDirectoryOnly);
            foreach (string file in files)
            try
            {
                var assembly = Assembly.LoadFile(file);
                //PanelSubscription.Instance.StrategySelector.SearchStrategiesInAssembly(assembly, typeof(PanelStrategyBase));
                foreach (Type type in assembly.GetTypes())
                {
                    Type iface = type.GetInterface("LanExchange.Sdk.IPlugin");
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

        internal void OnMainFormCreated()
        {
            lock (m_Plugins)
            {
                foreach (var plugin in m_Plugins)
                    try
                    {
                        plugin.MainFormCreated();
                    }
                    catch (Exception)
                    {
                    }
            }
        }
    }
}
