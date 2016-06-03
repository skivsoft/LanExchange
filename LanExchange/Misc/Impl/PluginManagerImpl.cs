using System;
using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Misc.Impl
{
    public class PluginManagerImpl : IPluginManager
    {
        private const string PLUGIN_MASK = "LanExchange.Plugin.*.dll";

        private readonly IServiceProvider serviceProvider;
        private readonly IDictionary<string, string> pluginsAuthors;

        [ImportMany]
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private IEnumerable<IPlugin> plugins;

        private CompositionContainer compContainer;

        public PluginManagerImpl(IServiceProvider serviceProvider)
        {
            Contract.Requires<ArgumentNullException>(serviceProvider != null);

            this.serviceProvider = serviceProvider;

            plugins = new List<IPlugin>();
            pluginsAuthors = new Dictionary<string, string>();
        }

        public void LoadPlugins()
        {
            var catalog = GetMefCatalogs();
            compContainer = new CompositionContainer(catalog);
            AttributedModelServices.ComposeParts(compContainer, this);

            foreach(var plugin in plugins)
            try
            {
                plugin.Initialize(serviceProvider);
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
            get { return plugins; }
        }

        public IDictionary<string, string> PluginsAuthors
        {
            get { return pluginsAuthors; }
        }
    }
}