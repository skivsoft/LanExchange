using System;
using LanExchange.Model;
using LanExchange.Model.Impl;
using LanExchange.SDK;
using LanExchange.UI;

namespace LanExchange.Presenter
{
    public static class AppPresenter
    {
        /// <summary>
        /// MainPages initializes in MainForm()
        /// </summary>
        public static PagesPresenter MainPages;

        public static IImageManager Images;
        public static IPanelItemFactoryManager PanelItemTypes;
        public static IPanelFillerManager PanelFillers;
        public static IPanelColumnManager PanelColumns;
        public static LazyThreadPool LazyThreadPool;
        public static PluginManager Plugins;
        public static IServiceProvider ServiceProvider;

        public static void Setup()
        {
            Images = new ImageManagerImpl();
            PanelItemTypes = new PanelItemFactoryManagerImpl();
            PanelFillers = new PanelFillerManagerImpl();
            PanelColumns = new PanelColumnManagerImpl();
            ServiceProvider = new ServiceProviderImpl();
            LazyThreadPool = new LazyThreadPool();
            Plugins = new PluginManager();
        }
    }
}
