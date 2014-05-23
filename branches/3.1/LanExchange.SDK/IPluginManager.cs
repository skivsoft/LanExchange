using System.Collections.Generic;

namespace LanExchange.SDK
{
    public interface IPluginManager
    {
        void LoadPlugins(PluginType type);
        IList<IPlugin> Items { get; }
        IDictionary<string, string> PluginsAuthors { get; }
    }
}