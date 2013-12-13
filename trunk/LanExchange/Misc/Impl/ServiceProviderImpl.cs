using System;
using System.Diagnostics;
using LanExchange.SDK;

namespace LanExchange.Misc.Impl
{
    internal class ServiceProviderImpl : IServiceProvider
    {
        public object GetService(Type serviceType)
        {
            object result = null;

            if (serviceType == typeof (IIoCContainer))
                return App.GetContainer();
            if (serviceType == typeof(IPagesPresenter))
                return App.MainPages;
            if (serviceType == typeof(IMainView))
                return App.MainView;
            if (serviceType == typeof(IPagesView))
                return App.MainPages.View;
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