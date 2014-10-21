using System;
using System.Diagnostics;
using LanExchange.Ioc;
using LanExchange.SDK;

namespace LanExchange.Misc.Impl
{
    internal class ServiceProviderImpl : IServiceProvider
    {
        public object GetService(Type serviceType)
        {
            object result = null;
            try
            {
                result = App.Resolve(serviceType);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            return result;
        }
    }
}