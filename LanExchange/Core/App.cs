using System;
using LanExchange.Misc;
using LanExchange.Misc.Impl;
using LanExchange.Misc.Service;
using LanExchange.Model;
using LanExchange.Presenter;
using LanExchange.SDK;
using LanExchange.UI;

namespace LanExchange.Core
{
    public static class App
    {
        // concrete 
        public static ImageManagerImpl Images;
        public static LazyThreadPool LazyThreadPool;
        public static PluginManager Plugins;
        // abstract
        private static IContainer s_Ioc;
        public static IPagesPresenter MainPages;
        public static IPanelItemFactoryManager PanelItemTypes;
        public static IPanelFillerManager PanelFillers;
        public static IPanelColumnManager PanelColumns;
        public static IServiceProvider ServiceProvider;
        public static IFolderManager FolderManager;

        public static void Setup()
        {
            // concrete
            Images = new ImageManagerImpl();
            LazyThreadPool = new LazyThreadPool();
            Plugins = new PluginManager();
            // Ioc
            s_Ioc = new SimpleIocContainer();
            SetupIoc();
            // abstract
            MainPages = s_Ioc.Resolve<IPagesPresenter>();
            TT.Translator = s_Ioc.Resolve<ITranslator>();
            PanelItemTypes = s_Ioc.Resolve<IPanelItemFactoryManager>();
            PanelFillers = s_Ioc.Resolve<IPanelFillerManager>();
            PanelColumns = s_Ioc.Resolve<IPanelColumnManager>();
            ServiceProvider = s_Ioc.Resolve<IServiceProvider>();
            FolderManager = s_Ioc.Resolve<IFolderManager>();
        }

        /// <summary>
        /// Maps interfaces to concrete implementations.
        /// </summary>
        private static void SetupIoc()
        {
            // core singletons
            s_Ioc.Register<ITranslator, TranslatorImpl>();
            s_Ioc.Register<IPanelItemFactoryManager, PanelItemFactoryManagerImpl>();
            s_Ioc.Register<IPanelFillerManager, PanelFillerManagerImpl>();
            s_Ioc.Register<IPanelColumnManager, PanelColumnManagerImpl>();
            s_Ioc.Register<IServiceProvider, ServiceProviderImpl>();
            s_Ioc.Register<IFolderManager, FolderManagerImpl>();
            // services
            s_Ioc.Register<IPuntoSwitcherService, PuntoSwitcherServiceEngRus>();
            // models
            s_Ioc.Register<IAboutModel, AboutModel>();
            s_Ioc.Register<IPagesModel, PagesModel>(LifeCycle.Transient);
            s_Ioc.Register<IPanelModel, PanelModel>(LifeCycle.Transient);
            // views
            s_Ioc.Register<IAboutView, AboutForm>(LifeCycle.Transient);
            s_Ioc.Register<IFilterView, FilterView>(LifeCycle.Transient);
            s_Ioc.Register<IPanelView, PanelView>(LifeCycle.Transient);
            s_Ioc.Register<IPagesView, PagesView>();
            // presenters
            s_Ioc.Register<IAboutPresenter, AboutPresenter>(LifeCycle.Singleton);
            s_Ioc.Register<IFilterPresenter, FilterPresenter>(LifeCycle.Transient);
            s_Ioc.Register<IPagesPresenter, PagesPresenter>(LifeCycle.Singleton);
            s_Ioc.Register<IPanelPresenter, PanelPresenter>(LifeCycle.Transient);
        }

        public static TTypeToResolve Resolve<TTypeToResolve>()
        {
            return s_Ioc.Resolve<TTypeToResolve>();
        }

        public static object Resolve(Type typeToResolve)
        {
            return s_Ioc.Resolve(typeToResolve);
        }
    }
}