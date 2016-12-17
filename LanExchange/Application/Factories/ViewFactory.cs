using System;
using LanExchange.Presentation.Interfaces;
using LanExchange.Presentation.Interfaces.Extensions;

namespace LanExchange.Application.Factories
{
    internal sealed class ViewFactory : FactoryBase, IViewFactory
    {
        public ViewFactory(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public IPanelView CreatePanelView()
        {
            return ServiceProvider.Resolve<IPanelView>();
        }

        public IPagesView GetPagesView()
        {
            return ServiceProvider.Resolve<IPagesView>();
        }

        public IStatusPanelView CreateStatusPanelView()
        {
            return ServiceProvider.Resolve<IStatusPanelView>();
        }
    }
}