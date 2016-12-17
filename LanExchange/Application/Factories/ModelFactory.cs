using System;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Factories
{
    internal sealed class ModelFactory : FactoryBase, IModelFactory
    {
        /// <exception cref="ArgumentNullException"></exception>
        public ModelFactory(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        public IPanelModel CreatePanelModel()
        {
            return Resolve<IPanelModel>();
        }
    }
}