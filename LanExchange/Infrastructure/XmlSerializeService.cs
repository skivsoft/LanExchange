using System;
using System.IO;
using System.Xml.Serialization;
using LanExchange.Domain.Interfaces;

namespace LanExchange.Infrastructure
{
    /// <summary>
    /// The serialize service implementation using xml.
    /// </summary>
    /// <seealso cref="LanExchange.Domain.Interfaces.ISerializeService" />
    internal sealed class XmlSerializeService : ISerializeService
    {
        public void SerializeToFile<T>(string fileName, T dto, Type[] extraTypes)
        {
            var serializer = new XmlSerializer(dto.GetType(), extraTypes);
            using (var file = new StreamWriter(fileName))
            {
                serializer.Serialize(file, dto);
            }
        }

        public T DeserializeFromFile<T>(string fileName, Type[] extraTypes)
        {
            var ser = new XmlSerializer(typeof(T), extraTypes);
            using (var tr = new StreamReader(fileName))
            {
                return (T)ser.Deserialize(tr);
            }
        }
    }
}