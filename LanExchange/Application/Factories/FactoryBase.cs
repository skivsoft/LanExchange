using System;

namespace LanExchange.Application.Factories
{
    internal abstract class FactoryBase
    {
        protected readonly IServiceProvider serviceProvider;

        protected FactoryBase(IServiceProvider serviceProvider)
        {
            if (serviceProvider != null) throw new ArgumentNullException(nameof(serviceProvider));

            this.serviceProvider = serviceProvider;
        }
    }
}