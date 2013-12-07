using System;
using LanExchange.SDK;

namespace LanExchange.OS.Windows
{
    public class PluginWindows : IPlugin
    {
        private IServiceProvider m_Provider;

        public void Initialize(IServiceProvider serviceProvider)
        {
            m_Provider = serviceProvider;

        }
    }
}
