//
// TODO Windows API calls (in Utils folder) must be removed in future
//

using System;
using LanExchange.SDK;
using LanExchange.SDK.UI;

namespace LanExchange.UI.WinForms
{
    public class PluginWinForms : IPlugin
    {
        private IServiceProvider m_Provider;

        public void Initialize(IServiceProvider serviceProvider)
        {
            m_Provider = serviceProvider;

            var container = (IIoCContainer) m_Provider.GetService(typeof (IIoCContainer));
            if (container == null) return;

            App.SetContainer(container);
            container.Register<IAppPresenter, AppPresenter>();
            container.Register<IAboutView, AboutForm>();
            container.Register<IFilterView, FilterView>();
            container.Register<IInfoView, InfoView>();
            container.Register<IMainView, MainForm>();
            container.Register<IPagesView, PagesView>();
            container.Register<IPanelView, PanelView>();
            container.Register<IWaitingService, WaitingServiceImpl>();
            container.Register<IAddonManager, AddonManagerImpl>();
            container.Register<IImageManager, ImageManagerImpl>();
        }
    }
}