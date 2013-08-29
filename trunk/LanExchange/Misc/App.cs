using System;
using LanExchange.Core;
using LanExchange.Misc.Impl;
using LanExchange.Misc.Service;
using LanExchange.Model;
using LanExchange.Presenter;
using LanExchange.SDK;
using LanExchange.UI;

namespace LanExchange.Misc
{
    public static class App
    {
        // concrete 
        public static ImageManagerImpl Images;
        public static LazyThreadPool LazyThreadPool;
        public static PluginManager Plugins;
        // abstract
        public static IContainer Ioc;
        public static IPagesPresenter MainPages;
        public static IPanelItemFactoryManager PanelItemTypes;
        public static IPanelFillerManager PanelFillers;
        public static IPanelColumnManager PanelColumns;
        public static IServiceProvider ServiceProvider;

        public static void Setup()
        {
            // concrete
            Images = new ImageManagerImpl();
            LazyThreadPool = new LazyThreadPool();
            Plugins = new PluginManager();
            // Ioc
            Ioc = new SimpleIocContainer();
            SetupIoc();
            // abstract
            MainPages = Ioc.Resolve<IPagesPresenter>();
            TT.Translator = Ioc.Resolve<ITranslator>();
            PanelItemTypes = Ioc.Resolve<IPanelItemFactoryManager>();
            PanelFillers = Ioc.Resolve<IPanelFillerManager>();
            PanelColumns = Ioc.Resolve<IPanelColumnManager>();
            ServiceProvider = Ioc.Resolve<IServiceProvider>();
        }

        /// <summary>
        /// Maps interfaces to concrete implementations.
        /// </summary>
        private static void SetupIoc()
        {
            // core singletons
            Ioc.Register<ITranslator, TranslatorImpl>();
            Ioc.Register<IPanelItemFactoryManager, PanelItemFactoryManagerImpl>();
            Ioc.Register<IPanelFillerManager, PanelFillerManagerImpl>();
            Ioc.Register<IPanelColumnManager, PanelColumnManagerImpl>();
            Ioc.Register<IServiceProvider, ServiceProviderImpl>();
            // services
            Ioc.Register<IPuntoSwitcherService, PuntoSwitcherServiceEngRus>();
            // models
            Ioc.Register<IPagesModel, PagesModel>(LifeCycle.Transient);
            Ioc.Register<IPanelModel, PanelItemList>(LifeCycle.Transient);
            // views
            Ioc.Register<IAboutView, AboutForm>(LifeCycle.Transient);
            Ioc.Register<IFilterView, FilterView>(LifeCycle.Transient);
            Ioc.Register<IPanelView, PanelView>(LifeCycle.Transient);
            Ioc.Register<IPagesView, PagesView>();
            // presenters
            //Ioc.Register<IPresenter<IAboutView>, AboutPresenter>(LifeCycle.Transient);
            //Ioc.Register<IPresenter<IFilterView>, FilterPresenter>(LifeCycle.Transient);
            //Ioc.Register<IPresenter<IPagesView>, PagesPresenter>(LifeCycle.Transient);
            //Ioc.Register<IPresenter<IPanelView>, PanelPresenter>(LifeCycle.Transient);
            Ioc.Register<IAboutPresenter, AboutPresenter>(LifeCycle.Singleton);
            Ioc.Register<IFilterPresenter, FilterPresenter>(LifeCycle.Transient);
            Ioc.Register<IPagesPresenter, PagesPresenter>(LifeCycle.Singleton);
            Ioc.Register<IPanelPresenter, PanelPresenter>(LifeCycle.Transient);
            PresenterFactory.SetContainer(Ioc);
        }
    }
}