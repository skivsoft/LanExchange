using System;
using LanExchange.Core;
using LanExchange.Intf;
using LanExchange.Misc;
using LanExchange.Misc.Impl;
using LanExchange.Model;
using LanExchange.Presenter;
using LanExchange.SDK;
using LanExchange.UI;

namespace LanExchange
{
    public static class ContainerBuilder
    {
        /// <summary>
        /// Maps interfaces to concrete implementations.
        /// </summary>
        public static IContainer Build()
        {
            var container = new SimpleIocContainer();
            // core singletons
            container.Register<ITranslator, TranslatorImpl>();
            container.Register<IPanelItemFactoryManager, PanelItemFactoryManagerImpl>();
            container.Register<IPanelFillerManager, PanelFillerManagerImpl>();
            container.Register<IPanelColumnManager, PanelColumnManagerImpl>();
            container.Register<IServiceProvider, ServiceProviderImpl>();
            container.Register<IFolderManager, FolderManagerImpl>();
            container.Register<IPluginManager, PluginManagerImpl>();
            container.Register<ILazyThreadPool, LazyThreadPoolImpl>();
            container.Register<IImageManager, ImageManagerImpl>();
            container.Register<IAddonManager, AddonManagerImpl>();
            // services
            container.Register<IPuntoSwitcherService, PuntoSwitcherServiceEngRus>();
            // models
            container.Register<IAboutModel, AboutModel>();
            container.Register<IPagesModel, PagesModel>(LifeCycle.Transient);
            container.Register<IPanelModel, PanelModel>(LifeCycle.Transient);
            // views
            container.Register<IAboutView, AboutForm>(LifeCycle.Transient);
            container.Register<IFilterView, FilterView>(LifeCycle.Transient);
            container.Register<IPanelView, PanelView>(LifeCycle.Transient);
            container.Register<IPagesView, PagesView>();
            container.Register<IMainView, MainForm>();
            // presenters
            container.Register<IMainPresenter, MainPresenter>(LifeCycle.Singleton);
            container.Register<IAboutPresenter, AboutPresenter>(LifeCycle.Singleton);
            container.Register<IPagesPresenter, PagesPresenter>(LifeCycle.Singleton);
            container.Register<IFilterPresenter, FilterPresenter>(LifeCycle.Transient);
            container.Register<IPanelPresenter, PanelPresenter>(LifeCycle.Transient);
            return container;
        }
    }
}
