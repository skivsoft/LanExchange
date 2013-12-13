//
// TODO Windows API calls (in Utils folder) must be removed in future
//

using System;
using LanExchange.SDK;
using LanExchange.UI.WinForms.Properties;

namespace LanExchange.UI.WinForms
{
    public class PluginWinForms : IPlugin
    {
        private IServiceProvider m_Provider;

        public void Initialize(IServiceProvider serviceProvider)
        {
            m_Provider = serviceProvider;

            // Setup resource manager
            var translationService = (ITranslationService)m_Provider.GetService(typeof(ITranslationService));
            if (translationService != null)
                translationService.SetResourceManagerTo<Resources>();

            var container = (IIoCContainer) m_Provider.GetService(typeof (IIoCContainer));
            if (container == null) return;

            App.SetContainer(container);
            // views
            container.Register<IAboutView, AboutForm>(LifeCycle.Transient);
            container.Register<IFilterView, FilterView>(LifeCycle.Transient);
            container.Register<IPanelView, PanelView>(LifeCycle.Transient);
            container.Register<IInfoView, InfoView>();
            container.Register<IMainView, MainForm>();
            container.Register<IPagesView, PagesView>();
            // other
            container.Register<IAddonManager, AddonManagerImpl>();
            container.Register<IImageManager, ImageManagerImpl>();
            container.Register<IAppPresenter, AppPresenter>();
            container.Register<IWaitingService, WaitingServiceImpl>();
            container.Register<IClipboardService, ClipboardServiceImpl>();
            container.Register<IScreenService, ScreenImpl>();
            container.Register<IMessageBoxService, MessageBoxServiceImpl>();
        }
    }
}