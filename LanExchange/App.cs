using System;
using System.ComponentModel;
using LanExchange.Interfaces;
using LanExchange.SDK;
using LanExchange.Extensions;
using LanExchange.Model;

namespace LanExchange
{
    public static class App
    {
        /// <summary>
        /// The IoC container.
        /// </summary>
        private static IServiceProvider s_Ioc;

        // public setters
        public static IMainView MainView { get; set; }
        // managers
        public static IPanelColumnManager PanelColumns { get; private set; }
        // presenters
        public static IPagesPresenter MainPages { get; private set; }
        public static IMainPresenter Presenter { get; private set; }
        // other
        public static ConfigModel Config { get; set; }
        public static ITranslationService TR { get; private set; }

        [Localizable(false)]
        public static void SetContainer(IServiceProvider container)
        {
            s_Ioc = container;
            // init translation service first and replace global resource manager
            TR = Resolve<ITranslationService>();
            // managers
            PanelColumns = Resolve<IPanelColumnManager>();
            // presenters
            Presenter = Resolve<IMainPresenter>();
            MainPages = Resolve<IPagesPresenter>();
        }

        public static TTypeToResolve Resolve<TTypeToResolve>()
        {
            return s_Ioc.Resolve<TTypeToResolve>();
        }
    }
}