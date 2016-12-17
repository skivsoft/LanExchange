using System;

namespace LanExchange.Application.Factories
{
    internal abstract class FactoryBase
    {
        private readonly IServiceProvider serviceProvider;

        /// <exception cref="ArgumentNullException"></exception>
        protected FactoryBase(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null) throw new ArgumentNullException(nameof(serviceProvider));

            this.serviceProvider = serviceProvider;
        }

        protected TService Resolve<TService>()
        {
            return (TService)serviceProvider.GetService(typeof(TService));
        }
    }
}