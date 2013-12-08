using System.Collections.Generic;
using LanExchange.SDK;

namespace LanExchange.Intf
{
    public interface IPluginManager
    {
        void LoadPlugins(PluginType type);
        IList<IPlugin> Items { get; }
        IDictionary<string, string> PluginsAuthors { get; }
    }
}