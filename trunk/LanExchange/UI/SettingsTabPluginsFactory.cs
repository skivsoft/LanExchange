using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.SDK;

namespace LanExchange.UI
{
    class SettingsTabPluginsFactory : ISettingsTabViewFactory
    {
        public ISettingsTabView Create()
        {
            return new SettingsTabPlugins();
        }
    }
}
