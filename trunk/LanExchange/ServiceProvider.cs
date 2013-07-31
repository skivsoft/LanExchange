using System;
using LanExchange.Model;
using LanExchange.Presenter;
using LanExchange.SDK;
using LanExchange.UI;

namespace LanExchange
{
    internal class ServiceProvider : IServiceProvider
    {
        public object GetService(Type serviceType)
        {
            if (serviceType == typeof (IPanelImageManager))
                return LanExchangeIcons.Instance;

            if (serviceType == typeof(IPanelItemFactoryManager))
                return AppPresenter.PanelItemTypes;

            if (serviceType == typeof(IPanelFillerStrategyManager))
                return AppPresenter.PanelFillers;

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
