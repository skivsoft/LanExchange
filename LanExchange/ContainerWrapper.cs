using System;
using LanExchange.Presentation.Interfaces;
using SimpleInjector;

namespace LanExchange
{
    internal sealed class ContainerWrapper : IContainerWrapper
    {
        private readonly Container container;

        public ContainerWrapper(Container container)
        {
            this.container = container;
        }

        public object GetService(Type serviceType)
        {
            return ((IServiceProvider) container).GetService(serviceType);
        }

        public void RegisterTransient<TService, TImplementation>() where TService : class where TImplementation : class, TService
        {
            container.Register<TService, TImplementation>();
        }

        public void RegisterSingleton<TService, TImplementation>() where TService : class where TImplementation : class, TService
        {
            container.RegisterSingleton<TService, TImplementation>();
        }
    }
}