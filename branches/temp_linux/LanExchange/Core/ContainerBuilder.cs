using System;
using LanExchange.Intf;
using LanExchange.Misc;
using LanExchange.Misc.Impl;
using LanExchange.Model;
using LanExchange.Presenter;
using LanExchange.SDK;
using LanExchange.SDK.UI;

namespace LanExchange.Core
{
    public static class ContainerBuilder
    {
        /// <summary>
        /// Maps interfaces to concrete implementations.
        /// </summary>
        public static IIoCContainer Build()
        {
            var container = new SimpleIocContainer();
            // core singletons
            container.Register<IPanelItemFactoryManager, PanelItemFactoryManagerImpl>();
            container.Register<IPanelFillerManager, PanelFillerManagerImpl>();
            container.Register<IPanelColumnManager, PanelColumnManagerImpl>();
            container.Register<IFolderManager, FolderManagerImpl>();
            container.Register<IPluginManager, PluginManagerImpl>();
            container.Register<IImageManager, ImageManagerImpl>();
            container.Register<IAddonManager, AddonManagerImpl>();
            container.Register<IServiceProvider, ServiceProviderImpl>();
            container.Register<ILazyThreadPool, LazyThreadPoolImpl>();
            container.Register<IPuntoSwitcherService, PuntoSwitcherServiceEngRus>();
            container.Register<ITranslationService, TranslationServiceImpl>();
            container.Register<IDisposableManager, DisposableManagerImpl>();
            // models
            container.Register<IAboutModel, AboutModel>();
            container.Register<IConfigModel, ConfigModel>();
            container.Register<IPagesModel, PagesModel>(LifeCycle.Transient);
            container.Register<IPanelModel, PanelModel>(LifeCycle.Transient);
            // presenters
            container.Register<IMainPresenter, MainPresenter>();
            container.Register<IAboutPresenter, AboutPresenter>();
            container.Register<IPagesPresenter, PagesPresenter>();
            container.Register<IFilterPresenter, FilterPresenter>(LifeCycle.Transient);
            container.Register<IPanelPresenter, PanelPresenter>(LifeCycle.Transient);
            return container;
        }
    }
}
