using System;
using LanExchange.Application.Interfaces.Extensions;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Factories
{
    internal sealed class ModelFactory : FactoryBase, IModelFactory
    {
        public ModelFactory(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public IPanelModel CreatePanelModel()
        {
            return ServiceProvider.Resolve<IPanelModel>();
        }
    }
}