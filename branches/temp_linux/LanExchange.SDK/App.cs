using System;
using System.ComponentModel;
using LanExchange.SDK.Model;
using LanExchange.SDK.Presenter;
using LanExchange.SDK.UI;

namespace LanExchange.SDK
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
        // managers
        public static IPanelItemFactoryManager PanelItemTypes;
        public static IPanelFillerManager PanelFillers;
        public static IPanelColumnManager PanelColumns;
        public static IFolderManager FolderManager;
        public static IImageManager Images;
        public static IAddonManager Addons;
        // presenters
        public static IPagesPresenter MainPages;
        public static IMainPresenter Presenter;
        // other
        public static IConfigModel Config;
        public static ILazyThreadPool Threads;
        public static ITranslationService TR;

        [Localizable(false)]
        public static void SetContainer(IIoCContainer container)
        {
            s_Ioc = container;
            // init translation service first and replace global resource manager
            TR = Resolve<ITranslationService>();
            // managers
            PanelItemTypes = Resolve<IPanelItemFactoryManager>();
            PanelFillers = Resolve<IPanelFillerManager>();
            PanelColumns = Resolve<IPanelColumnManager>();
            FolderManager = Resolve<IFolderManager>();
            // presenters
            Presenter = Resolve<IMainPresenter>();
            MainPages = Resolve<IPagesPresenter>();
            // other
            Config = Resolve<IConfigModel>();
            Threads = Resolve<ILazyThreadPool>();
        }

        public static IIoCContainer GetContainer()
        {
            return s_Ioc;
        }

        public static TTypeToResolve Resolve<TTypeToResolve>()
        {
            return (TTypeToResolve)s_Ioc.Resolve(typeof(TTypeToResolve));
        }

        public static object Resolve(Type typeToResolve)
        {
            return s_Ioc.Resolve(typeToResolve);
        }
    }
}