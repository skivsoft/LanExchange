using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using LanExchange.SDK;

namespace LanExchange.Model
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
                    }
                    catch (Exception)
                    {
                    }
                    if (plugin == null) continue;
                    try
                    {
                        m_Plugins.Add(plugin);
                        plugin.Initialize(new ServiceProvider());
                    }
                    catch (Exception)
                    {
                    }
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

        //internal void OpenDefaultTab()
        //{
        //    foreach (var plugin in m_Plugins)
        //        plugin.OpenDefaultTab();
        //}
    }
}
