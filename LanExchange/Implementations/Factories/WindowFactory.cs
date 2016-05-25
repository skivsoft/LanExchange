using LanExchange.SDK.Factories;
using System;
using LanExchange.SDK;
using LanExchange.SDK.Extensions;

namespace LanExchange.Implementations.Factories
{
    internal sealed class WindowFactory : FactoryBase, IWindowFactory
    {
        public WindowFactory(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public IWindow CreateAboutView()
        {
            return serviceProvider.Resolve<IAboutView>();
        }

        public IMainView CreateMainView()
        {
            return serviceProvider.Resolve<IMainView>();
        }

        public ICheckAvailabilityWindow CreateCheckAvailabilityWindow()
        {
            return serviceProvider.Resolve<ICheckAvailabilityWindow>();
        }
    }
}