using System;

namespace LanExchange.Presentation.Interfaces
{
    public interface IContainerWrapper : IServiceProvider
    {
        IContainerWrapper RegisterTransient<TService, TImplementation>() 
            where TService : class 
            where TImplementation : class, TService;
        IContainerWrapper RegisterSingleton<TService, TImplementation>() 
            where TService : class 
            where TImplementation : class, TService;

        IContainerWrapper Verify();
    }
}