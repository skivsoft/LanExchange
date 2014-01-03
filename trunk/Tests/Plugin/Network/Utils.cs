using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
