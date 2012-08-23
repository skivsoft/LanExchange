using System;
using System.Collections.Generic;
using System.Text;
using PurePatterns;
using PureInterfaces;
using LanExchange.SDK.SDKModel;
using System.IO;
using System.Reflection;
using LanExchange.SDK;

namespace LanExchange.Model
{
    public class PluginProxy : Proxy, IPluginProxy, IProxy
    {
        public new const string NAME = "PluginProxy";
        public const string PluginsPath = "plugins";
        private AppDomain m_PluginsDomain;
        private Dictionary<string, IPlugin> m_Plugins;

        public PluginProxy()
            : base(NAME)
        {
            m_Plugins = new Dictionary<string, IPlugin>();
        }

        public override void OnRegister()
        {
            AppDomainSetup Setup = new AppDomainSetup();
            //Setup.PrivateBinPath = PluginsPath;
            m_PluginsDomain = AppDomain.CreateDomain("PluginsDomain");
        }

        public override void OnRemove()
        {
            AppDomain.Unload(m_PluginsDomain);
        }

        public void InitializePlugins()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, PluginsPath);
            string[] files = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                try
                {
                    Assembly assembly = Assembly.LoadFile(file);
                    foreach (Type type in assembly.GetTypes())
                    {
                        Type iface = type.GetInterface("LanExchange.SDK.IPlugin");
                        if (iface != null)
                        {
                            LanExchange.SDK.IPlugin plugin = (LanExchange.SDK.IPlugin)Activator.CreateInstance(type);
                            if (plugin != null)
                            {
                                m_Plugins.Add(plugin.GetType().Name, plugin);
                                plugin.Initialize(Facade);
                            }
                        }
                    }
                }
                catch(Exception)
                {
                }
            }
        }
    }
}
