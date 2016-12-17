using System;
using LanExchange.Application.Interfaces.Extensions;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Factories
{
    internal sealed class ViewFactory : FactoryBase, IViewFactory
    {
        /// <exception cref="ArgumentNullException"></exception>
        public ViewFactory(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        /// <exception cref="ArgumentNullException"></exception>
        public IPanelView CreatePanelView()
        {
            return ServiceProvider.Resolve<IPanelView>();
        }

        /// <exception cref="ArgumentNullException"></exception>
        public IPagesView GetPagesView()
        {
            return ServiceProvider.Resolve<IPagesView>();
        }

        /// <exception cref="ArgumentNullException"></exception>
        public IStatusPanelView CreateStatusPanelView()
        {
            return ServiceProvider.Resolve<IStatusPanelView>();
        }
    }
}