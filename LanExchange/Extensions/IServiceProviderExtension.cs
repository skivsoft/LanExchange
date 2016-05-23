using System;
using System.Diagnostics.Contracts;

namespace LanExchange.Extensions
{
    /// <summary>
    /// Extensions method for the <see cref="IServiceProvider"/>.
    /// </summary>
    public static class IServiceProviderExtension
    {
        /// <summary>
        /// Resolves the specified type. 
        /// Treats <see cref="IServiceProvider"/> as a DI-container.
        /// </summary>
        /// <typeparam name="TTypeToResolve">The type to resolve.</typeparam>
        /// <param name="serviceProvider">The service provider instance.</param>
        /// <returns>The resolved object.</returns>
        /// <exception cref="System.ArgumentNullException">serviceProvider</exception>
        public static TTypeToResolve Resolve<TTypeToResolve>(this IServiceProvider serviceProvider)
        {
            Contract.Requires<ArgumentNullException>(serviceProvider != null);
                
            return (TTypeToResolve)serviceProvider.GetService(typeof(TTypeToResolve));
        }
    }
}