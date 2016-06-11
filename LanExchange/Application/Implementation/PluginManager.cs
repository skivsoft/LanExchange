using LanExchange.Presentation.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using LanExchange.Application.Interfaces;

namespace LanExchange.Application.Implementation
{
    /// <summary>
    /// The plugin manager implementation using MEF.
    /// </summary>
    /// <seealso cref="LanExchange.Presentation.Interfaces.IPluginManager" />
    internal sealed class PluginManager : IPluginManager
    {
        private const string PLUGIN_MASK = "LanExchange.Plugin.*.dll";
        private readonly ILogService logService;

        [ImportMany]
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private IEnumerable<IPlugin> plugins;

        public PluginManager(ILogService logService)
        {
            Contract.Requires<ArgumentNullException>(logService != null);

            this.logService = logService;
            plugins = Enumerable.Empty<IPlugin>();
        }

        public void LoadPlugins()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            catalog.Catalogs.Add(new DirectoryCatalog(".", PLUGIN_MASK));

            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }

        public void InitializePlugins(IServiceProvider serviceProvider)
        {
            foreach (var plugin in plugins)
            {
                logService.Log("plugin initialization: {0}", plugin.GetType().Name);
                try
                {
                    plugin.Initialize(serviceProvider);
                }
                catch (Exception exception)
                {
                    logService.Log(exception);
                }
            }
        }
    }
}