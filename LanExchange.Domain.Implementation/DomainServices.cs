using System;
using LanExchange.Application.Interfaces;
using LanExchange.Domain.Interfaces;

namespace LanExchange.Domain.Implementation
{
    internal sealed class DomainServices : IDomainServices
    {
        private readonly ISerializeService serializeService;

        public DomainServices(ISerializeService serializeService)
        {
            if (serializeService == null) throw new ArgumentNullException(nameof(serializeService));
            this.serializeService = serializeService;
        }

        public void SerializeToFile<T>(string fileName, T dto, Type[] extraTypes)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException(nameof(fileName));
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            serializeService.SerializeToFile(fileName, dto, extraTypes);
        }

        public T DeserializeFromFile<T>(string fileName, Type[] extraTypes)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException(nameof(fileName));

            return serializeService.DeserializeFromFile<T>(fileName, extraTypes);
        }
    }
}