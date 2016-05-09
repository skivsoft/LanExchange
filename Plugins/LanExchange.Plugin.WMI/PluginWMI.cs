using LanExchange.SDK;
using System;
using System.ComponentModel.Composition;

namespace LanExchange.Plugin.WMI
{
    [Export(typeof(IPlugin))]
    public class PluginWMI : IPlugin
    {
        public void Initialize(IServiceProvider serviceProvider)
        {
            
        }
    }
}
