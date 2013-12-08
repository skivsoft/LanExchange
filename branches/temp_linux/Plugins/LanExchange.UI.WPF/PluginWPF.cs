using System;
using LanExchange.SDK;
using LanExchange.SDK.UI;

namespace LanExchange.UI.WPF
{
    public class PluginWPF : IPlugin
    {

        private IServiceProvider m_Provider;

        public void Initialize(IServiceProvider serviceProvider)
        {
            m_Provider = serviceProvider;
            var container = (IIoCContainer)m_Provider.GetService(typeof(IIoCContainer));
            if (container == null) return;

            container.Register<IAppPresenter, App>();
            container.Register<IMainView, MainWindow>();
            //container.Register<IImageManager, ImageManagerImpl>();
        }

       
    }
}
