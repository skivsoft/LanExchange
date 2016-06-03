using System;
using LanExchange.Presentation.Interfaces;

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
        public static IContainerWrapper InitializeDIContainer()
        {
            return new ContainerBuilder().Build();
        }
    }
}