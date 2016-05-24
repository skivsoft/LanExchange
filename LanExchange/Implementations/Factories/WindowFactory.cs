using LanExchange.SDK.Factories;
using System;
using LanExchange.SDK;
using System.Diagnostics.Contracts;
using LanExchange.SDK.Extensions;

namespace LanExchange.Implementations.Factories
{
    internal sealed class WindowFactory : IWindowFactory
    {
        private readonly IServiceProvider serviceProvider;

        public WindowFactory(IServiceProvider serviceProvider)
        {
            Contract.Requires<ArgumentNullException>(serviceProvider != null);

            this.serviceProvider = serviceProvider;
        }

        public IWindow CreateAboutView()
        {
            return serviceProvider.Resolve<IAboutView>();
        }

        public IMainView CreateMainView()
        {
            return serviceProvider.Resolve<IMainView>();
        }
    }
}