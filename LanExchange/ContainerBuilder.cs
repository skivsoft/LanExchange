using LanExchange.Misc.Impl;
using LanExchange.Plugin.Windows;
using SimpleInjector;
using System;
using LanExchange.Application;
using LanExchange.Application.Commands;
using LanExchange.Application.Factories;
using LanExchange.Application.Implementation;
using LanExchange.Application.Interfaces;
using LanExchange.Application.Interfaces.Services;
using LanExchange.Application.Managers;
using LanExchange.Application.Models;
using LanExchange.Application.Presenters;
using LanExchange.Application.Services;
using LanExchange.Presentation.Interfaces;

namespace LanExchange
{
    /// <summary>
    /// Internal application DI-container builder.
    /// </summary>
    internal sealed class ContainerBuilder
    {
        private Container container;

        /// <summary>
        /// Maps interfaces to concrete implementations in DI-container.
        /// </summary>
        public IContainerWrapper Build()
        {
            container = new Container();

            RegisterCoreSingletons();
            RegisterModels();
            RegisterPresenters();
            RegisterOSWindows();
            RegisterServices();
            RegisterFactories();
            RegisterCommands();

            return new ContainerWrapper(container);
        }

        private void RegisterCoreSingletons()
        {
            container.RegisterSingleton<IServiceProvider>(container);
            container.RegisterSingleton<IAppBootstrap, AppBootstrap>();
            container.RegisterSingleton<IPanelItemFactoryManager, PanelItemFactoryManagerImpl>();
            container.RegisterSingleton<IPanelFillerManager, PanelFillerManagerImpl>();
            container.RegisterSingleton<IPanelColumnManager, PanelColumnManagerImpl>();
            container.RegisterSingleton<IFolderManager, FolderManager>();
            container.RegisterSingleton<IPluginManager, PluginManagerImpl>();
            container.RegisterSingleton<ILazyThreadPool, LazyThreadPoolImpl>();
            container.RegisterSingleton<IPuntoSwitcherService, PuntoSwitcherServiceEngRus>();
            container.RegisterSingleton<ITranslationService, TranslationService>();
            container.RegisterSingleton<IDisposableManager, DisposableManager>();
            container.RegisterSingleton<ICommandManager, CommandManager>();
        }

        private void RegisterModels()
        {
            container.RegisterSingleton<IAboutModel, AboutModel>();
            container.RegisterSingleton<IPagesModel, PagesModel>();
            container.Register<IPanelModel, PanelModel>();
        }

        private void RegisterPresenters()
        {
            container.RegisterSingleton<IAppPresenter, AppPresenter>();
            container.RegisterSingleton<IMainPresenter, MainPresenter>();
            container.Register<IAboutPresenter, AboutPresenter>();
            container.RegisterSingleton<IPagesPresenter, PagesPresenter>();
            container.Register<IFilterPresenter, FilterPresenter>();
            container.Register<IPanelPresenter, PanelPresenter>();
            container.Register<IEditPresenter, EditPresenter>();
            container.Register<ICheckAvailabilityPresenter, CheckAvailabilityPresenter>();
            container.Register<IInfoPresenter, InfoPresenter>();
            container.Register<IStatusPanelPresenter, StatusPanelPresenter>();
        }

        private void RegisterOSWindows()
        {
            container.RegisterSingleton<IUser32Service, User32Service>();
            container.RegisterSingleton<IShell32Service, Shell32Service>();
            container.RegisterSingleton<IMACAddressSerivice, MACAddressService>();
            container.RegisterSingleton<IHotkeyService, HotkeysService>();
        }

        private void RegisterServices()
        {
            container.RegisterSingleton<IConfigPersistenceService, ConfigPersistenceService>();
            container.RegisterSingleton<IPagesPersistenceService, PagesPersistenceService>();
            container.Register<IProcessService, ProcessService>();
        }

        private void RegisterFactories()
        {
            container.RegisterSingleton<IAddonProgramFactory, AddonProgramFactory>();
            container.RegisterSingleton<IWindowFactory, WindowFactory>();
            container.RegisterSingleton<IViewFactory, ViewFactory>();
            container.RegisterSingleton<IModelFactory, ModelFactory>();
            container.RegisterSingleton<IServiceFactory, ServiceFactory>();
        }

        private void RegisterCommands()
        {
            container.RegisterCollection<ICommand>(new []
            {
                typeof(AboutCommand),
                typeof(PagesReReadCommand),
                typeof(PagesCloseTabCommand),
                typeof(PagesCloseOtherCommand),
                typeof(ShortcutKeysCommand)
            });
        }

    }
}