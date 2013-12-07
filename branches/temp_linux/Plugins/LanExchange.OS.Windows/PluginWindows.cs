using System;
using LanExchange.SDK;
using LanExchange.SDK.OS;

namespace LanExchange.OS.Windows
{
    public class PluginWindows : IPlugin
    {
        private IServiceProvider m_Provider;

        public void Initialize(IServiceProvider serviceProvider)
        {
            m_Provider = serviceProvider;

            var container = (IIoCContainer) m_Provider.GetService(typeof (IIoCContainer));
            if (container == null) return;

            container.Register<IHotkeysService, HotkeysService>();
            container.Register<IListViewService, ListViewService>();
            container.Register<IUser32Service, User32Service>();
            container.Register<IShell32Service, Shell32Service>();
            container.Register<IKernel32Service, Kernel32Service>();
            container.Register<IComctl32Service, Comctl32Service>();
            container.Register<IOle32Service, Ole32Service>();
        }
    }
}