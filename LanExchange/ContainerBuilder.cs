using LanExchange.Plugin.Windows;
using SimpleInjector;
using System;
using LanExchange.Application;
using LanExchange.Application.Commands;
using LanExchange.Application.Factories;
using LanExchange.Application.Implementation;
using LanExchange.Application.Interfaces;
using LanExchange.Application.Managers;
using LanExchange.Application.Models;
using LanExchange.Application.Presenters;
using LanExchange.Domain.Implementation;
using LanExchange.Presentation.Interfaces;
using LanExchange.Domain.Interfaces;
using LanExchange.Infrastructure;

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
            RegisterDomain();
            RegisterInfrastucture();

            return new ContainerWrapper(container);
        }

        private void RegisterCoreSingletons()
        {
            container.RegisterSingleton<IServiceProvider>(container);
            container.RegisterSingleton<IPanelItemFactoryManager, PanelItemFactoryManager>();
            container.RegisterSingleton<IPanelFillerManager, PanelFillerManager>();
            container.RegisterSingleton<IPanelColumnManager, PanelColumnManager>();
            container.RegisterSingleton<IFolderManager, FolderManager>();
            container.RegisterSingleton<IPluginManager, PluginManager>();
            container.RegisterSingleton<ILazyThreadPool, LazyThreadPool>();
            container.RegisterSingleton<IPuntoSwitcherService, PuntoSwitcherServiceEngRus>();
            container.RegisterSingleton<ITranslationService, TranslationService>();
            container.RegisterSingleton<IDisposableManager, DisposableManager>();
            container.RegisterSingleton<ICommandManager, CommandManager>();
        }

        private void RegisterModels()
        {
            container.RegisterSingleton<IAboutModel, AboutModel>();
            container.RegisterSingleton<Application.Interfaces.IPagesModel, PagesModel>();
            container.Register<IPanelModel, PanelModel>();
        }

        private void RegisterPresenters()
        {
            container.Register<IAppBootstrap, AppBootstrap>();
            container.RegisterSingleton<IAppPresenter, AppPresenter>();
            container.Register<IMainPresenter, MainPresenter>();
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
            container.Register<IProcessService, ProcessService>();
            container.RegisterSingleton<ILogService, EmptyLogService>();
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

        private void RegisterDomain()
        {
            // TODO: can became transient when dependecy on PagesPresenter will be removed from commands
            container.RegisterSingleton<IPagesPersistenceService, PagesPersistenceService>();
        }

        private void RegisterInfrastucture()
        {
            container.RegisterSingleton<ISerializeService, XmlSerializeService>();
        }
    }
}