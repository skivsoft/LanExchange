using System;
using LanExchange.Model;
using LanExchange.SDK;
using LanExchange.UI;

namespace LanExchange
{
    internal class ServiceProvider : IServiceProvider
    {
        public object GetService(Type serviceType)
        {
            // return existing instances

            if (serviceType == typeof (IPanelImageManager))
                return LanExchangeIcons.Instance;

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

            // create new instances
            //if (serviceType == typeof(IADExecutor))
            //    return new ConcreteADExecutor();

            return null;
        }
    }
}
