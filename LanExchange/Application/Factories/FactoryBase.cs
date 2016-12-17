using System;

namespace LanExchange.Application.Factories
{
    internal abstract class FactoryBase
    {
        /// <exception cref="ArgumentNullException"></exception>
        protected FactoryBase(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null) throw new ArgumentNullException(nameof(serviceProvider));

            ServiceProvider = serviceProvider;
        }

        protected IServiceProvider ServiceProvider { get; private set; }
    }
}