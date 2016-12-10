using System;

namespace LanExchange.Application.Factories
{
    internal abstract class FactoryBase
    {
        protected readonly IServiceProvider serviceProvider;

        protected FactoryBase(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }
    }
}