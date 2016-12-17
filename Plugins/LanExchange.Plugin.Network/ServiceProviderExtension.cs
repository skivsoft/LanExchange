using System;

namespace LanExchange.Plugin.Network
{
    internal static class ServiceProviderExtension
    {
        /// <exception cref="ArgumentNullException"></exception>
        public static TService Resolve<TService>(this IServiceProvider serviceProvider)
        {
            if (serviceProvider == null) throw new ArgumentNullException(nameof(serviceProvider));
            return (TService)serviceProvider.GetService(typeof(TService));
        }
    }
}