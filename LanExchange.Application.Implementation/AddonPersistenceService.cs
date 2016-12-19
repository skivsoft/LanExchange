using System;
using LanExchange.Application.Interfaces;
using LanExchange.Presentation.Interfaces.Addons;
using LanExchange.Presentation.Interfaces.Persistence;

namespace LanExchange.Application.Implementation
{
    internal sealed class AddonPersistenceService : IAddonPersistenceService
    {
        private readonly IDomainServices domainServices;

        public AddonPersistenceService(IDomainServices domainServices)
        {
            if (domainServices == null) throw new ArgumentNullException(nameof(domainServices));
            this.domainServices = domainServices;
        }

        public AddOn Load(string fileName)
        {
            return domainServices.DeserializeFromFile<AddOn>(fileName);
        }
    }
}