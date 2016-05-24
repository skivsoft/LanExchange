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

        // other
        public static ConfigModel Config { get; set; }

        [Localizable(false)]
        public static void SetContainer(IServiceProvider container)
        {
            s_Ioc = container;
        }

        public static TTypeToResolve Resolve<TTypeToResolve>()
        {
            return s_Ioc.Resolve<TTypeToResolve>();
        }
    }
}