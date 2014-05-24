using System;
using LanExchange.OS.Windows;
using LanExchange.SDK;

namespace LanExchange.Plugin.Network
{
    internal static class Utils
    {
        internal static void InitPlugins()
        {
            App.SetContainer(ContainerBuilder.Build());
            (new PluginWindows()).Initialize(App.Resolve<IServiceProvider>());
            (new PluginNetwork()).Initialize(App.Resolve<IServiceProvider>());
        }
    }
}