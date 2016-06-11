using System;

namespace LanExchange.Domain.Interfaces
{
    /// <summary>
    /// The serialize service interface.
    /// </summary>
    public interface ISerializeService
    {
        void SerializeToXmlFile<T>(string fileName, T dto, Type[] extraTypes);
        T DeserializeFromXmlFile<T>(string fileName, Type[] extraTypes);
    }
}