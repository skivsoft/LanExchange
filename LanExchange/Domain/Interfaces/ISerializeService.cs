using System;

namespace LanExchange.Domain.Interfaces
{
    /// <summary>
    /// The serialize service interface.
    /// </summary>
    public interface ISerializeService
    {
        void SerializeToFile<T>(string fileName, T dto, Type[] extraTypes);
        T DeserializeFromFile<T>(string fileName, Type[] extraTypes);
    }
}