//
// TODO System.Drawing reference must be removed in future
// TODO System.Windows.Forms reference must be removed in future
//
//

using System;
using LanExchange.SDK;

namespace LanExchange.OS.Windows
{
    public class PluginWindows : IPlugin
    {
        private IServiceProvider m_Provider;

        public void Initialize(IServiceProvider serviceProvider)
        {
            // load plugin if only mono runtime doesn't present
            if (EnvironmentUtils.IsRunningOnMono())
                return;

            m_Provider = serviceProvider;

            var container = (IIoCContainer) m_Provider.GetService(typeof (IIoCContainer));
            if (container == null) return;

            container.Register<IUser32Service, User32Service>();
            container.Register<IShell32Service, Shell32Service>();
            container.Register<IKernel32Service, Kernel32Service>();
            container.Register<IComctl32Service, Comctl32Service>();
            container.Register<IOle32Service, Ole32Service>();

            container.Register<IHotkeysService, HotkeysService>();
            container.Register<ISingleInstanceService, SingleInstanceService>();
            container.Register<ISysImageListService, SysImageListService>(LifeCycle.Transient);
        }
    }
}