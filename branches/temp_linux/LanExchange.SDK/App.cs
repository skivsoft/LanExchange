using System;
using System.ComponentModel;

namespace LanExchange.SDK
{
    public static class App
    {
        /// <summary>
        /// The IoC container.
        /// </summary>
        private static IIoCContainer s_Ioc;

        // public setters
        public static IMainView MainView { get; set; }
        public static IImageManager Images { get; set; }
        public static IAddonManager Addons { get; set; }
        public static IPanelFillerManager PanelFillers { get; set; }
        // managers
        public static IPanelItemFactoryManager PanelItemTypes { get; private set; }
        public static IPanelColumnManager PanelColumns { get; private set; }
        public static IFolderManager FolderManager { get; private set; }
        // presenters
        public static IPagesPresenter MainPages { get; private set; }
        public static IMainPresenter Presenter { get; private set; }
        // other
        public static IConfigModel Config { get; private set; }
        public static ILazyThreadPool Threads { get; private set; }
        public static ITranslationService TR { get; private set; }

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