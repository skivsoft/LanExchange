using System;
using System.Diagnostics.Contracts;
using LanExchange.Domain.Interfaces;
using LanExchange.Presentation.Interfaces.Addons;
using LanExchange.Presentation.Interfaces.Persistence;

namespace LanExchange.Domain.Implementation
{
    internal sealed class AddonPersistenceService : IAddonPersistenceService
    {
        private readonly ISerializeService serializeService;

        public AddonPersistenceService(ISerializeService serializeService)
        {
            Contract.Requires<ArgumentNullException>(serializeService != null);
            this.serializeService = serializeService;
        }

        public AddOn Load(string fileName)
        {
            return serializeService.DeserializeFromFile<AddOn>(fileName, new Type[0]);
        }
    }
}
