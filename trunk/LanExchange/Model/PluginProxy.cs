using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using LanExchange.Model.VO;
using PureMVC.PureInterfaces;
using PureMVC.PurePatterns;

namespace LanExchange.Model
{
    public class PluginProxy : PanelItemProxy
    {
        public new const string NAME = "PluginProxy";

        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        
        
        public const string PluginsPath = "plugins";
        private AppDomain m_PluginsDomain;
        private Dictionary<string, IPlugin> m_Plugins;

        public PluginProxy()
            : base(NAME)
        {
            m_Plugins = new Dictionary<string, IPlugin>();
        }

        private void DomAsmsPrint(AppDomain cur_domain)
        {
            Assembly[] asm_list = cur_domain.GetAssemblies();
            logger.Info("\n");
            logger.Info(String.Format("List of assemblies loaded in domain [{0}] ({1}): ", cur_domain.FriendlyName, asm_list.Length));
            foreach (Assembly item in asm_list)
            {
                logger.Info(String.Format("{0}", item.GetName().Name));
            }
            if (cur_domain.SetupInformation.PrivateBinPath != null && cur_domain.SetupInformation.PrivateBinPath.Trim() != string.Empty)
            {
                string[] s_list = cur_domain.SetupInformation.PrivateBinPath.Split(';');
                logger.Info("\n");
                logger.Info(String.Format("Directories specified in PrivateBinPath property of domain [{0}] ({1}): ", cur_domain.FriendlyName, s_list.Length));
                foreach (string item in s_list)
                {
                    logger.Info(item);
                }
            }
        }

        public override void OnRegister()
        {
            AppDomainSetup Setup = new AppDomainSetup();
            //string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, PluginsPath);
            Setup.PrivateBinPath = PluginsPath ;
            m_PluginsDomain = AppDomain.CreateDomain("PluginsDomain");
            logger.Info(String.Format("Create app domain [{0}]", m_PluginsDomain.FriendlyName));
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
            logger.Info(String.Format("Unload app domain [{0}]", m_PluginsDomain.FriendlyName));
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
            //string path = AppDomain.CurrentDomain.BaseDirectory;
            string[] files = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                try
                {
                    Assembly assembly = Assembly.LoadFile(file);
                    foreach (Type type in assembly.GetTypes())
                    {
                        Type iface = type.GetInterface("LanExchange.IPlugin");
                        if (iface != null)
                        {
                            LanExchange.IPlugin plugin = (LanExchange.IPlugin)Activator.CreateInstance(type);
                            if (plugin != null)
                            {
                                m_Plugins.Add(plugin.GetType().Name, plugin);
                                plugin.Initialize(Facade);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }
            }

            //DomAsmsPrint(AppDomain.CurrentDomain);
            DomAsmsPrint(m_PluginsDomain);
        }
    }
}
