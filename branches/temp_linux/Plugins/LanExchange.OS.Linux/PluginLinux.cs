using System;
using LanExchange.SDK;

namespace LanExchange.OS.Linux
{
    public class PluginLinux : IPlugin
    {
        private IServiceProvider m_Provider;

        public void Initialize(IServiceProvider serviceProvider)
        {
            m_Provider = serviceProvider;
        }
    }
}
