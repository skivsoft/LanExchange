using System;
using LanExchange.SDK;

namespace LanExchange.Intf
{
    public static class App
    {
        /// <summary>
        /// The IoC container.
        /// </summary>
        private static IIoCContainer s_Ioc;
        /// <summary>
        /// This value has been intialized in MainForm.
        /// </summary>
        public static IMainView MainView;

        public static void SetContainer(IIoCContainer iioCContainer)
        {
            s_Ioc = iioCContainer;
            TranslationResourceManager.Service = Resolve<ITranslationService>();
        }

        public static TTypeToResolve Resolve<TTypeToResolve>()
        {
            return s_Ioc.Resolve<TTypeToResolve>();
        }

        public static object Resolve(Type typeToResolve)
        {
            return s_Ioc.Resolve(typeToResolve);
        }

        public static IPagesPresenter MainPages
        {
            get { return Resolve<IPagesPresenter>(); }
        }

        public static IPanelItemFactoryManager PanelItemTypes
        {
            get { return Resolve<IPanelItemFactoryManager>(); }
        }

        private static IPanelFillerManager s_PanelFillers;

        /// <summary>
        /// Setter added for unit-tests.
        /// </summary>
        /// <value>
        /// The panel fillers.
        /// </value>
        public static IPanelFillerManager PanelFillers
        {
            get
            {
                if (s_PanelFillers == null)
                    s_PanelFillers = Resolve<IPanelFillerManager>();
                return s_PanelFillers;
            }
            set { s_PanelFillers = value; }
        }

        public static IPanelColumnManager PanelColumns
        {
            get { return Resolve<IPanelColumnManager>(); }
        }

        public static IServiceProvider ServiceProvider
        {
            get { return Resolve<IServiceProvider>(); }
        }

        public static IFolderManager FolderManager
        {
            get { return Resolve<IFolderManager>(); }
        }

        public static IPluginManager Plugins
        {
            get { return Resolve<IPluginManager>(); }
        }

        public static ILazyThreadPool Threads
        {
            get { return Resolve<ILazyThreadPool>(); }
        }

        public static IImageManager Images
        {
            get { return Resolve<IImageManager>(); }
        }

        public static IAddonManager Addons
        {
            get { return Resolve<IAddonManager>(); }
        }

        public static IMainPresenter Presenter
        {
            get { return Resolve<IMainPresenter>(); }
        }

        public static ITranslationService TR
        {
            get { return Resolve<ITranslationService>(); }
        }

        public static IConfigModel Config
        {
            get { return Resolve<IConfigModel>(); }
        }
    }
}