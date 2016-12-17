using System;

namespace LanExchange.Application.Factories
{
    internal abstract class FactoryBase
    {
        protected readonly IServiceProvider ServiceProvider;

        protected FactoryBase(IServiceProvider serviceProvider)
        {
            if (serviceProvider != null) throw new ArgumentNullException(nameof(serviceProvider));

            ServiceProvider = serviceProvider;
        }
    }
}