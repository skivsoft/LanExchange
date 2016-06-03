using System;
using LanExchange.Presentation.Interfaces;
using LanExchange.Presentation.Interfaces.Factories;
using LanExchange.SDK.Extensions;

namespace LanExchange.Application.Factories
{
    internal sealed class ViewFactory : FactoryBase, IViewFactory
    {
        public ViewFactory(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public IPanelView CreatePanelView()
        {
            return serviceProvider.Resolve<IPanelView>();
        }

        public IPagesView GetPagesView()
        {
            return serviceProvider.Resolve<IPagesView>();
        }

        public IStatusPanelView CreateStatusPanelView()
        {
            return serviceProvider.Resolve<IStatusPanelView>();
        }
    }
}