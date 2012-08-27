using System;
using System.Collections.Generic;
using System.Text;
using PurePatterns;
using PureInterfaces;
using LanExchange.SDK.SDKModel;
using System.IO;
using System.Reflection;
using LanExchange.SDK;
using LanExchange.SDK.SDKModel.VO;
using LanExchange.Model.VO;

namespace LanExchange.Model
{
    public class PluginProxy : PanelItemProxy, IPluginProxy
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
            m_PluginsDomain = AppDomain.CreateDomain("PluginsDomain");
            //ApplicationFacade.PluginDomain = m_PluginsDomain;

            /*
            INavigatorProxy navigator = (INavigatorProxy)Facade.RetrieveProxy("NavigatorProxy");
            if (navigator != null)
            {
                navigator.AddTransition("PluginProxy", String.Empty);
            }
             */
        }

        public override void OnRemove()
        {
            AppDomain.Unload(m_PluginsDomain);
        }

        public override ColumnVO[] GetColumns()
        {
            return new ColumnVO[] { 
                new ColumnVO(Globals.T("ColumnPluginName"), 120),
                new ColumnVO(Globals.T("ColumnPluginVersion"), 50),
                new ColumnVO(Globals.T("ColumnPluginDesc"), 200),
                new ColumnVO(Globals.T("ColumnPluginAuthor"), 100)
            };
        }

        public override void EnumObjects(string path)
        {
            
            foreach (var pair in m_Plugins)
            {

                PluginAttribute attr = (PluginAttribute)Attribute.GetCustomAttribute(pair.Value.GetType(), typeof(PluginAttribute));
                Objects.Add(new PluginVO(pair.Key, attr.Version, attr.Description, attr.Author));
            }
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
