using System;
using LanExchange.Presentation.Interfaces;
using LanExchange.Presentation.Interfaces.Extensions;

namespace LanExchange.Application.Factories
{
    internal sealed class WindowFactory : FactoryBase, IWindowFactory
    {
        public WindowFactory(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public IAboutView CreateAboutView()
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

        public IPanelView CreatePanelView()
        {
            return serviceProvider.Resolve<IPanelView>();
        }
    }
}