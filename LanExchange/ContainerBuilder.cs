using LanExchange.Implementations.Factories;
using LanExchange.Implementations.Managers;
using LanExchange.Implementations.Services;
using LanExchange.Interfaces;
using LanExchange.Interfaces.Factories;
using LanExchange.Interfaces.Services;
using LanExchange.Misc.Impl;
using LanExchange.Model;
using LanExchange.Plugin.Windows;
using LanExchange.Plugin.WinForms;
using LanExchange.Plugin.WinForms.Impl;
using LanExchange.Presenter;
using LanExchange.SDK;
using LanExchange.SDK.Factories;
using LanExchange.SDK.Managers;
using SimpleInjector;
using SimpleInjector.Diagnostics;
using System;
using LanExchange.Commands;
using LanExchange.Presentation.Interfaces;
using LanExchange.Presentation.Interfaces.Factories;
using LanExchange.Presentation.WinForms;
using LanExchange.Presentation.WinForms.Controls;
using LanExchange.Presentation.WinForms.Forms;

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
        public IServiceProvider Build()
        {
            container = new Container();

            RegisterCoreSingletons();
            RegisterPanelUpdater();
            RegisterModels();
            RegisterPresenters();
            RegisterOSWindows();
            RegisterWinForms();
            RegisterServices();
            RegisterFactories();
            RegisterCommands();

            VerifyContainer();
            return container;
        }

        private void RegisterCoreSingletons()
        {
            container.RegisterSingleton<IServiceProvider>(container);
            container.RegisterSingleton<LanExchangeApp>();
            container.RegisterSingleton<IPanelItemFactoryManager, PanelItemFactoryManagerImpl>();
            container.RegisterSingleton<IPanelFillerManager, PanelFillerManagerImpl>();
            container.RegisterSingleton<IPanelColumnManager, PanelColumnManagerImpl>();
            container.RegisterSingleton<IFolderManager, FolderManagerImpl>();
            container.RegisterSingleton<IPluginManager, PluginManagerImpl>();
            container.RegisterSingleton<ILazyThreadPool, LazyThreadPoolImpl>();
            container.RegisterSingleton<IPuntoSwitcherService, PuntoSwitcherServiceEngRus>();
            container.RegisterSingleton<ITranslationService, TranslationServiceImpl>();
            container.RegisterSingleton<IDisposableManager, DisposableManagerImpl>();
            container.RegisterSingleton<ICommandManager, CommandManager>();
        }

        private void RegisterPanelUpdater()
        {
            container.Register<IPanelUpdater, PanelUpdaterImpl>();
        }

        private void RegisterModels()
        {
            container.RegisterSingleton<IAboutModel, AboutModel>();
            container.RegisterSingleton<IPagesModel, PagesModel>();
            container.Register<IPanelModel, PanelModel>();
        }

        private void RegisterPresenters()
        {
            container.RegisterSingleton<IMainPresenter, MainPresenter>();
            container.RegisterSingleton<IAboutPresenter, AboutPresenter>();
            container.RegisterSingleton<IPagesPresenter, PagesPresenter>();
            container.Register<IFilterPresenter, FilterPresenter>();
            container.Register<IPanelPresenter, PanelPresenter>();
            container.Register<IEditPresenter, EditPresenter>();
            container.Register<ICheckAvailabilityPresenter, CheckAvailabilityPresenter>();
            container.Register<IStatusPanelPresenter, StatusPanelPresenter>();
        }

        private void RegisterOSWindows()
        {
            container.RegisterSingleton<IUser32Service, User32Service>();
            container.RegisterSingleton<IShell32Service, Shell32Service>();
            container.RegisterSingleton<IMACAddressSerivice, MACAddressService>();
            container.RegisterSingleton<IHotkeyService, HotkeysService>();
        }

        private void RegisterWinForms()
        {
            container.Register<ICheckAvailabilityWindow, CheckAvailabilityForm>();
            container.Register<IAboutView, AboutForm>();
            container.Register<IFilterView, FilterView>();
            container.Register<IPanelView, PanelView>();
            container.Register<IEditView, EditForm>();
            container.Register<IStatusPanelView, StatusPanel>();
            container.RegisterSingleton<IMainView, MainForm>();
            container.RegisterSingleton<IPagesView, PagesView>();
            container.RegisterSingleton<IAddonManager, AddonManagerImpl>();
            container.RegisterSingleton<IImageManager, ImageManagerImpl>();
            container.RegisterSingleton<IAppPresenter, AppPresenter>();
            container.RegisterSingleton<IWaitingService, WaitingServiceImpl>();
            container.RegisterSingleton<IClipboardService, ClipboardServiceImpl>();
            container.RegisterSingleton<IScreenService, ScreenImpl>();
            container.RegisterSingleton<IMessageBoxService, MessageBoxServiceImpl>();
        }

        private void RegisterServices()
        {
            container.RegisterSingleton<IConfigPersistenceService, ConfigPersistenceService>();
            container.RegisterSingleton<IPagesPersistenceService, PagesPersistenceService>();
            container.Register<ISystemInformationService, SystemInformationService>();
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

        private void SuppressDisposableTransientComponentWarning<T>()
        {
            container.GetRegistration(typeof(T)).Registration
                .SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "manual");
        }

        private void VerifyContainer()
        {
            SuppressDisposableTransientComponentWarning<IAboutView>();
            SuppressDisposableTransientComponentWarning<IFilterView>();
            SuppressDisposableTransientComponentWarning<IEditView>();
            SuppressDisposableTransientComponentWarning<IPanelView>();
            SuppressDisposableTransientComponentWarning<IPanelUpdater>();
            SuppressDisposableTransientComponentWarning<IPanelModel>();
            SuppressDisposableTransientComponentWarning<ICheckAvailabilityWindow>();
            SuppressDisposableTransientComponentWarning<IStatusPanelView>();

            container.Verify();
        }
    }
}