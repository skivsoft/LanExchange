using System;

namespace LanExchange.Presentation.Interfaces
{
    public interface IContainerWrapper : IServiceProvider
    {
        void RegisterTransient<TService, TImplementation>() where TService : class where TImplementation : class, TService;
        void RegisterSingleton<TService, TImplementation>() where TService : class where TImplementation : class, TService;
    }
}