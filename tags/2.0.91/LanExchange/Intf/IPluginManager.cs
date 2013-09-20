using System.Collections.Generic;
using LanExchange.SDK;

namespace LanExchange.Intf
{
    public interface IPluginManager
    {
        void LoadPlugins();
        IList<IPlugin> Items { get; }
    }
}