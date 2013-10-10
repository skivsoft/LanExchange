using System;

namespace LanTabs
{
    public static class App
    {
        private static IIoCContainer s_Ioc;

        public static void SetContainer(IIoCContainer container)
        {
            s_Ioc = container;
        }

        public static TTypeToResolve Resolve<TTypeToResolve>()
        {
            return s_Ioc.Resolve<TTypeToResolve>();
        }

        public static object Resolve(Type typeToResolve)
        {
            return s_Ioc.Resolve(typeToResolve);
        }
    }
}