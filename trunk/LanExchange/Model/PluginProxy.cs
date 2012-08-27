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
    internal static class PluginManager
    {
        public const string PluginsPath = "plugins";

        //private static AppDomain m_PluginsDomain;
        private static Dictionary<string, IPlugin> m_Plugins = null;

        /*
        public static void DoneAppDomain()
        {
            AppDomain.Unload(m_PluginsDomain);
            m_PluginsDomain = null;
        }
         */

        public static void LoadPlugins()
        {
            AppDomain.CurrentDomain.AppendPrivatePath(PluginsPath);

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
                                Plugins.Add(plugin.GetType().Name, plugin);
                                plugin.Initialize(ApplicationFacade.Instance);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        /*
        public static AppDomain PluginsAppDomain
        {
            get 
            {
                if (m_PluginsDomain == null)
                {
                    AppDomainSetup Setup = new AppDomainSetup();
                    Setup.PrivateBinPath = PluginsPath;
                    m_PluginsDomain = AppDomain.CreateDomain("PluginsDomain");
                }
                return m_PluginsDomain; 
            }
        }
         */

        public static IDictionary<string, IPlugin> Plugins
        {
            get 
            {
                if (m_Plugins == null)
                {
                    m_Plugins = new Dictionary<string, IPlugin>();
                }
                return m_Plugins; 
            }
        }

    }
    
    public class PluginProxy : PanelItemProxy
    {
        public new const string NAME = "PluginProxy";

        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        
        public PluginProxy()
            : base(NAME)
        {
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
        }

        public override void OnRemove()
        {
            //PluginManager.DoneAppDomain();
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
            foreach (var pair in PluginManager.Plugins)
            {
                PluginAttribute attr = (PluginAttribute)Attribute.GetCustomAttribute(pair.Value.GetType(), typeof(PluginAttribute));
                Objects.Add(new PluginVO(pair.Key, attr.Version, attr.Description, attr.Author));
            }
        }

        public void InitializePlugins()
        {
            //PluginManager.PluginsAppDomain.SetD
            //PluginManager.PluginsAppDomain.DoCallBack(new CrossAppDomainDelegate(PluginManager.LoadPlugins));
            PluginManager.LoadPlugins();
            //DomAsmsPrint(AppDomain.CurrentDomain);
            //DomAsmsPrint(PluginManager.PluginsAppDomain);
        }
    }
}
