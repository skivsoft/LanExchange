using System;

namespace LanExchange.Presentation.Interfaces
{
    public interface IPluginManager
    {
        void LoadPlugins();

        void InitializePlugins(IServiceProvider serviceProvider);
    }
}