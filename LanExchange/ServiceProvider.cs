using System;
using LanExchange.Model;
using LanExchange.Sdk;

namespace LanExchange
{
    internal class ServiceProvider : IServiceProvider
    {
        public object GetService(Type serviceType)
        {
            if (serviceType == typeof (IPanelImageManager))
                return LanExchangeIcons.Instance;
            return null;
        }
    }
}
