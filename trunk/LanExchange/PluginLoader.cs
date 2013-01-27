using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using LanExchange.Model;
using LanExchange.Sdk;

namespace LanExchange
{
    internal class PluginLoader
    {
        private const string PluginMask = "Plugin.*.dll";

        public void LoadPlugins()
        {
            var folder = AppDomain.CurrentDomain.BaseDirectory;
            if (!Directory.Exists(folder)) return;
            var files = Directory.GetFiles(folder, PluginMask, SearchOption.TopDirectoryOnly);
            foreach (string file in files)
            {
                try
                {
                    Assembly assembly = Assembly.LoadFile(file);
                    foreach (Type type in assembly.GetTypes())
                    {
                        Type iface = type.GetInterface("LanExchange.Sdk.IPlugin");
                        if (iface != null)
                        {
                            var plugin = (IPlugin)Activator.CreateInstance(type);
                            plugin.Initialize(new ServiceProvider());
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
            
        }
    }
}
