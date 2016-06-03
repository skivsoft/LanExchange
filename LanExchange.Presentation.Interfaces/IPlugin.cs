using System;

namespace LanExchange.Presentation.Interfaces
{
    /// <summary>
    /// Every LanExchange plugin must implement this interface.
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// Initializes the specified service provider.
        /// </summary>
        /// <remarks>This method calls befor main form created.</remarks>
        /// <param name="serviceProvider">The service provider.</param>
        void Initialize(IServiceProvider serviceProvider);
    }
}
