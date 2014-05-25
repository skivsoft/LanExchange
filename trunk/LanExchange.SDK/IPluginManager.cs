using System.Collections.Generic;

namespace LanExchange.SDK
{
    public interface IPluginManager
    {
        void LoadPlugins();
        IList<IPlugin> Items { get; }
        IDictionary<string, string> PluginsAuthors { get; }
    }
}