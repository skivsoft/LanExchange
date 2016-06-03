using System;
using System.ComponentModel.Composition;
using LanExchange.Presentation.Interfaces;

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
