using System;
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
            if (serializeService != null) throw new ArgumentNullException(nameof(serializeService));
            this.serializeService = serializeService;
        }

        public AddOn Load(string fileName)
        {
            return serializeService.DeserializeFromFile<AddOn>(fileName, new Type[0]);
        }
    }
}
