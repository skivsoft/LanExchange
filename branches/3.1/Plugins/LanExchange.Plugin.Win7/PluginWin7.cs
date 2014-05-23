using System;
using LanExchange.SDK;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace LanExchange.Plugin.Win7
{
    public class PluginWin7 : IPlugin
    {
        private IServiceProvider m_Provider;

        public void Initialize(IServiceProvider serviceProvider)
        {
            m_Provider = serviceProvider;
            var container = (IIoCContainer)m_Provider.GetService(typeof(IIoCContainer));
            if (container == null) return;

            if (TaskbarManager.IsPlatformSupported)
            {
                container.Unregister<IWaitingService>();
                container.Register<IWaitingService, Win7WaitingServiceImpl>();
            }
        }
    }
}
