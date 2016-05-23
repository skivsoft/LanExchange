using System;
using LanExchange.Interfaces;
using LanExchange.Misc.Impl;
using LanExchange.Model;
using LanExchange.Plugin.Windows;
using LanExchange.Presenter;
using LanExchange.SDK;
using LanExchange.Plugin.WinForms.Components;
using LanExchange.Plugin.WinForms.Forms;
using LanExchange.Plugin.WinForms;
using LanExchange.Plugin.WinForms.Impl;
using SimpleInjector;
using LanExchange.Interfaces.Services;
using LanExchange.Implementations.Services;
using LanExchange.Interfaces.Processes;
using LanExchange.Implementations.Processes;
using LanExchange.Interfaces.Factories;
using LanExchange.Implementations.Factories;
using LanExchange.SDK.Managers;
using LanExchange.Implementations.Managers;
using LanExchange.SDK.Factories;

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
            RegisterProcesses();
            RegisterFactories();
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
            container.RegisterSingleton<IActionManager, ActionManager>();
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
        }

        private void RegisterOSWindows()
        {
            container.RegisterSingleton<IUser32Service, User32Service>();
            container.RegisterSingleton<IShell32Service, Shell32Service>();
            container.RegisterSingleton<IKernel32Service, Kernel32Service>();
            container.RegisterSingleton<IComctl32Service, Comctl32Service>();
            container.RegisterSingleton<IOle32Service, Ole32Service>();
            container.RegisterSingleton<IIPHLPAPISerivice, IPHLPAPISerivce>();
            container.RegisterSingleton<IHotkeysService, HotkeysService>();
            container.Register<ISysImageListService, SysImageListService>();
        }

        private void RegisterWinForms()
        {
            container.Register<ICheckAvailabilityWindow, CheckAvailabilityForm>();
            container.Register<IFilterView, FilterView>();
            container.Register<IPanelView, PanelView>();
            container.Register<IEditView, EditForm>();
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
        }

        private void RegisterProcesses()
        {
            container.Register<ICmdLineProcessor, CmdLineProcessor>();
            container.Register<IGenerateEnglishProcess, GenerateEnglishProcess>();
        }

        private void RegisterFactories()
        {
            container.RegisterSingleton<IAddonProgramFactory, AddonProgramFactory>();
            container.RegisterSingleton<IWindowFactory, WindowFactory>();
        }
    }
}