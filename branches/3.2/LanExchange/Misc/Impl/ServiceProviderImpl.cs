using System;
using System.Diagnostics;

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