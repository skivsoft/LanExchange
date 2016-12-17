using System;
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
            return  Resolve<IAboutView>();
        }

        public IMainView CreateMainView()
        {
            return Resolve<IMainView>();
        }

        public ICheckAvailabilityWindow CreateCheckAvailabilityWindow()
        {
            return Resolve<ICheckAvailabilityWindow>();
        }

        public IPanelView CreatePanelView()
        {
            return Resolve<IPanelView>();
        }
    }
}