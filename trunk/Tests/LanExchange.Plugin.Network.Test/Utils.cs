using System;
using LanExchange.Ioc;
using LanExchange.SDK;

namespace LanExchange.Plugin.Network
{
    internal static class Utils
    {
        internal static void InitPlugins()
        {
            App.SetContainer(ContainerBuilder.Build());
            (new PluginNetwork()).Initialize(App.Resolve<IServiceProvider>());
        }
    }
}