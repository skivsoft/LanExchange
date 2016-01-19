using System;
using System.Collections.Generic;
using System.Reflection;

using LanExchange.SDK;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;

namespace LanExchange.Misc.Impl
{
    public class PluginManagerImpl : IPluginManager
    {
        private const string PLUGIN_MASK = "LanExchange.Plugin.*.dll";


        [ImportMany]
        private IEnumerable<IPlugin> m_Plugins;

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

            foreach(var plugin in m_Plugins)
            try
            {
                plugin.Initialize(App.Resolve<IServiceProvider>());
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }

        public static AggregateCatalog GetMefCatalogs()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            catalog.Catalogs.Add(new DirectoryCatalog(".", PLUGIN_MASK));
            return catalog;
        }

        public IEnumerable<IPlugin> Items
        {
            get { return m_Plugins; }
        }

        public IDictionary<string, string> PluginsAuthors
        {
            get { return m_PluginsAuthors; }
        }

    }
}