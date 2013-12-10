//
// TODO Hot keys for menu items "&Panel"
// TODO show F1
// 
//


using System;
using LanExchange.SDK;
using LanExchange.SDK.UI;
using LanExchange.SDK.Presenter;
using LanExchange.UI.WPF.Impl;

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

            container.Register<IImageManager, ImageManagerImpl>();
            container.Register<IAddonManager, AddonManagerImpl>();

            container.Register<IAppPresenter, AppPresenter>();
            container.Register<IMainView, MainWindow>();

            container.Register<IPagesView, PagesView>();
            container.Register<IPanelView, PanelView>(LifeCycle.Transient);

            
        }

       
    }
}
