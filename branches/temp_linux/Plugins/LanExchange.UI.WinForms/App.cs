using System;
using System.ComponentModel;
using LanExchange.SDK;

namespace LanExchange.UI.WinForms
{
    public static class App
    {
        /// <summary>
        /// The IoC container.
        /// </summary>
        private static IIoCContainer s_Ioc;

        [Localizable(false)]
        public static void SetContainer(IIoCContainer container)
        {
            s_Ioc = container;
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