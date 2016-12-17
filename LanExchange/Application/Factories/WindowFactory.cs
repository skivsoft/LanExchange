using System;
using LanExchange.Application.Interfaces.Extensions;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Factories
{
    internal sealed class WindowFactory : FactoryBase, IWindowFactory
    {
        /// <exception cref="ArgumentNullException"></exception>
        public WindowFactory(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        public IAboutView CreateAboutView()
        {
            return ServiceProvider. Resolve<IAboutView>();
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