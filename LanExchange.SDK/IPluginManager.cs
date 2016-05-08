using System.Collections.Generic;

namespace LanExchange.SDK
{
    public interface IPluginManager
    {
        void LoadPlugins();
        IEnumerable<IPlugin> Items { get; }
        IDictionary<string, string> PluginsAuthors { get; }
    }
}