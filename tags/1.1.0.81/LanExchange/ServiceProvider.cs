using System;
using LanExchange.Model;
using LanExchange.Sdk;
using LanExchange.UI;

namespace LanExchange
{
    internal class ServiceProvider : IServiceProvider
    {
        public object GetService(Type serviceType)
        {
            if (serviceType == typeof (IPanelImageManager))
                return LanExchangeIcons.Instance;
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
