using System;

namespace LanExchange
{
    /// <summary>
    /// LanExchange core facade class.
    /// </summary>
    public static class CoreFacade
    {
        /// <summary>
        /// Initializes the DI-container.
        /// </summary>
        /// <returns>The <see cref="IServiceProvider"/> instance.</returns>
        public static IServiceProvider InitializeDIContainer()
        {
            return new ContainerBuilder().Build();
        }
    }
}