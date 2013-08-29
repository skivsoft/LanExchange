using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LanExchange.Core;
using LanExchange.Intf;
using LanExchange.Misc.Impl;
using LanExchange.Model;
using LanExchange.Presenter;
using LanExchange.SDK;
using LanExchange.UI;

namespace LanExchange.Misc
{
    public static class AppBold
    {
        // concrete 
        public static ImageManagerImpl Images;
        public static LazyThreadPool LazyThreadPool;
        public static PluginManager Plugins;

        public static void Setup()
        {
            // concrete
            Images = new ImageManagerImpl();
            LazyThreadPool = new LazyThreadPool();
            Plugins = new PluginManager();
            SetupIoc();
        }

        /// <summary>
        /// Maps interfaces to concrete implementations.
        /// </summary>
        private static void SetupIoc()
        {
            var container = new SimpleIocContainer();
            // core singletons
            container.Register<ITranslator, TranslatorImpl>();
            container.Register<IPanelItemFactoryManager, PanelItemFactoryManagerImpl>();
            container.Register<IPanelFillerManager, PanelFillerManagerImpl>();
            container.Register<IPanelColumnManager, PanelColumnManagerImpl>();
            container.Register<IServiceProvider, ServiceProviderImpl>();
            container.Register<IFolderManager, FolderManagerImpl>();
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
            // presenters
            container.Register<IAboutPresenter, AboutPresenter>(LifeCycle.Singleton);
            container.Register<IFilterPresenter, FilterPresenter>(LifeCycle.Transient);
            container.Register<IPagesPresenter, PagesPresenter>(LifeCycle.Singleton);
            container.Register<IPanelPresenter, PanelPresenter>(LifeCycle.Transient);
            App.SetContainer(container);
        }
    }
}
