using LanExchange.Model;
using LanExchange.Model.Impl;
using LanExchange.SDK;

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
        public static PluginManager Plugins;

        public static void Setup()
        {
            Images = new ImageManagerImpl();
            PanelItemTypes = new PanelItemFactoryManagerImpl();
            PanelFillers = new PanelFillerManagerImpl();
            PanelColumns = new PanelColumnManagerImpl();
            Plugins = new PluginManager();
        }
    }
}
