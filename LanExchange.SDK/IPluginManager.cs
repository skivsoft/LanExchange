using System.Collections.Generic;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.SDK
{
    public interface IPluginManager
    {
        void LoadPlugins();
        IEnumerable<IPlugin> Items { get; }
    }
}