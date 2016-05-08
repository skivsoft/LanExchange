using System;

namespace LanExchange
{
    /// <summary>
    /// LanExchange core facade class.
    /// </summary>
    public static class CoreFacade
    {
        /// <summary>
        /// Initializes the IoC-container.
        /// </summary>
        /// <returns></returns>
        public static IServiceProvider InitializeIoCContainer()
        {
            var container = new ContainerBuilder().Build();
            App.SetContainer(container);
            return container;
        }
    }
}