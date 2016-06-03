using System.Collections.Generic;

namespace LanExchange.Presentation.Interfaces
{
    public interface IPluginManager
    {
        void LoadPlugins();
        IEnumerable<IPlugin> Items { get; }
    }
}