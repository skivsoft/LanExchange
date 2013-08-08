using System;
using LanExchange.Presenter;
using LanExchange.SDK;
using LanExchange.UI;

namespace LanExchange.Model
{
    internal class ServiceProvider : IServiceProvider
    {
        public object GetService(Type serviceType)
        {
            if (serviceType == typeof (IImageManager))
                return AppPresenter.Images;

            if (serviceType == typeof(IPanelItemFactoryManager))
                return AppPresenter.PanelItemTypes;

            if (serviceType == typeof(IPanelFillerManager))
                return AppPresenter.PanelFillers;

            if (serviceType == typeof (IPanelColumnManager))
                return AppPresenter.PanelColumns;

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
