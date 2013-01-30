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
        /// <remarks>This method calls befor main form created.</remarks>
        /// <param name="serviceProvider">The service provider.</param>
        void Initialize(IServiceProvider serviceProvider);
        /// <summary>
        /// Afters the main form create.
        /// </summary>
        void MainFormCreated();
    }
}
