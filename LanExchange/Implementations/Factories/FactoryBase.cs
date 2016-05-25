using System;
using System.Diagnostics.Contracts;

namespace LanExchange.Implementations.Factories
{
    internal abstract class FactoryBase
    {
        protected readonly IServiceProvider serviceProvider;

        protected FactoryBase(IServiceProvider serviceProvider)
        {
            Contract.Requires<ArgumentNullException>(serviceProvider != null);

            this.serviceProvider = serviceProvider;
        }
    }
}