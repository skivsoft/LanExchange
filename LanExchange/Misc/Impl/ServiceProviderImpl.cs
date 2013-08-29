using System;
using LanExchange.SDK;
using LanExchange.UI;

namespace LanExchange.Misc.Impl
{
    internal class ServiceProviderImpl : IServiceProvider
    {
        public object GetService(Type serviceType)
        {
            if (serviceType == typeof (ITranslator))
                return TT.Translator;

            if (serviceType == typeof (IImageManager))
                return App.Images;

            if (serviceType == typeof(IPanelItemFactoryManager))
                return App.PanelItemTypes;

            if (serviceType == typeof(IPanelFillerManager))
                return App.PanelFillers;

            if (serviceType == typeof (IPanelColumnManager))
                return App.PanelColumns;

            //if (PanelSubscription.Instance != null)
            //    if (serviceType == typeof (IBackgroundStrategySelector))
            //        return PanelSubscription.Instance.StrategySelector;

            if (MainForm.Instance != null)
            {
                if (serviceType == typeof (IPagesView))
                    return MainForm.Instance.Pages;
                if (serviceType == typeof (IInfoView))
                    return MainForm.Instance.pInfo;
            }

            return null;
        }
    }
}
