using System;

namespace LanExchange.Sdk
{
    /// <summary>
    /// Every LanExchange plugin must implement this interface.
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// Initializes the specified service provider.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        void Initialize(IServiceProvider serviceProvider);
    }
}
