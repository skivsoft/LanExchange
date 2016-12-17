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
            return ServiceProvider.Resolve<IAboutView>();
        }

        public IMainView CreateMainView()
        {
            return ServiceProvider.Resolve<IMainView>();
        }

        public ICheckAvailabilityWindow CreateCheckAvailabilityWindow()
        {
            return ServiceProvider.Resolve<ICheckAvailabilityWindow>();
        }

        public IPanelView CreatePanelView()
        {
            return ServiceProvider.Resolve<IPanelView>();
        }
    }
}