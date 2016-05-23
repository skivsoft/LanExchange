using System;
using System.ComponentModel;
using LanExchange.SDK;
using LanExchange.Model;
using LanExchange.SDK.Extensions;

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
        // other
        public static ConfigModel Config { get; set; }
        public static ITranslationService TR { get; private set; }

        [Localizable(false)]
        public static void SetContainer(IServiceProvider container)
        {
            s_Ioc = container;
            // init translation service first and replace global resource manager
            TR = Resolve<ITranslationService>();
        }

        public static TTypeToResolve Resolve<TTypeToResolve>()
        {
            return s_Ioc.Resolve<TTypeToResolve>();
        }
    }
}