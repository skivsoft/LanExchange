using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using LanExchange.Ioc;
using LanExchange.SDK;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace LanExchange.Misc.Impl
{
    public class PluginManagerImpl : IPluginManager
    {
        private const string PLUGIN_MASK = "LanExchange.Plugin.*.dll";
        private const string IPLUGIN_INTERFACE = "LanExchange.SDK.IPlugin";
        private const string PLUGIN_TYPE_PREFIX = "Plugin";


        [ImportMany]
        private IList<IPlugin> m_Plugins;

        CompositionContainer compContainer;


        private readonly IDictionary<string, string> m_PluginsAuthors;

        public PluginManagerImpl()
        {
            m_Plugins = new List<IPlugin>();
            m_PluginsAuthors = new Dictionary<string, string>();
        }

        public void LoadPlugins()
        {
            var catalog = GetMefCatalogs();
            compContainer = new CompositionContainer(catalog);
            AttributedModelServices.ComposeParts(compContainer, this);
        }

        public static AggregateCatalog GetMefCatalogs()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            catalog.Catalogs.Add(new DirectoryCatalog(".", PLUGIN_MASK));
            return catalog;
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