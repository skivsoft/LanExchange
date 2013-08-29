using System;
using LanExchange.SDK;

namespace LanExchange.Intf
{
    public static class App
    {
        // abstract
        private static IContainer s_Ioc;
        public static IPagesPresenter MainPages;
        public static IPanelItemFactoryManager PanelItemTypes;
        public static IPanelFillerManager PanelFillers;
        public static IPanelColumnManager PanelColumns;
        public static IServiceProvider ServiceProvider;
        public static IFolderManager FolderManager;

        public static void SetContainer(IContainer container)
        {
            s_Ioc = container;
            // abstract
            MainPages = Resolve<IPagesPresenter>();
            TT.Translator = Resolve<ITranslator>();
            PanelItemTypes = Resolve<IPanelItemFactoryManager>();
            PanelFillers = Resolve<IPanelFillerManager>();
            PanelColumns = Resolve<IPanelColumnManager>();
            ServiceProvider = Resolve<IServiceProvider>();
            FolderManager = Resolve<IFolderManager>();
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