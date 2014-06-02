using System;
using LanExchange.Interfaces;
using LanExchange.Misc;
using LanExchange.Misc.Impl;
using LanExchange.Model;
using LanExchange.Plugin.Windows;
using LanExchange.Presenter;
using LanExchange.SDK;
using LanExchange.Plugin.WinForms.Components;
using LanExchange.Plugin.WinForms.Forms;
using LanExchange.Plugin.WinForms;
using LanExchange.Plugin.WinForms.Impl;

namespace LanExchange
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
            container.Register<IServiceProvider, ServiceProviderImpl>();
            container.Register<ILazyThreadPool, LazyThreadPoolImpl>();
            container.Register<IPuntoSwitcherService, PuntoSwitcherServiceEngRus>();
            container.Register<ITranslationService, TranslationServiceImpl>();
            container.Register<IDisposableManager, DisposableManagerImpl>();
            // updater
            container.Register<IPanelUpdater, PanelUpdaterImpl>(LifeCycle.Transient);
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
            container.Register<IEditPresenter, EditPresenter>(LifeCycle.Transient);
            // OS: Windows
            container.Register<IUser32Service, User32Service>();
            container.Register<IShell32Service, Shell32Service>();
            container.Register<IKernel32Service, Kernel32Service>();
            container.Register<IComctl32Service, Comctl32Service>();
            container.Register<IOle32Service, Ole32Service>();
            container.Register<IIPHLPAPISerivice, IPHLPAPISerivce>();
            container.Register<IHotkeysService, HotkeysService>();
            container.Register<ISingleInstanceService, SingleInstanceService>();
            container.Register<ISysImageListService, SysImageListService>(LifeCycle.Transient);
            // UI: WinForms
            container.Register<ICheckAvailabilityWindow, CheckAvailabilityForm>(LifeCycle.Transient);
            container.Register<IAboutView, AboutForm>(LifeCycle.Transient);
            container.Register<IFilterView, FilterView>(LifeCycle.Transient);
            container.Register<IPanelView, PanelView>(LifeCycle.Transient);
            container.Register<IEditView, EditForm>(LifeCycle.Transient);
            container.Register<IMainView, MainForm>();
            container.Register<IPagesView, PagesView>();
            container.Register<IAddonManager, AddonManagerImpl>();
            container.Register<IImageManager, ImageManagerImpl>();
            container.Register<IAppPresenter, AppPresenter>();
            container.Register<IWaitingService, WaitingServiceImpl>();
            container.Register<IClipboardService, ClipboardServiceImpl>();
            container.Register<IScreenService, ScreenImpl>();
            container.Register<IMessageBoxService, MessageBoxServiceImpl>();
            return container;
        }
    }
}
