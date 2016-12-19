using System;
using System.IO;
using System.Security;
using System.Xml.Serialization;
using LanExchange.Domain.Interfaces;

namespace LanExchange.Infrastructure
{
    /// <summary>
    /// The serialize service implementation using xml.
    /// </summary>
    /// <seealso cref="LanExchange.Domain.Interfaces.ISerializeService" />
    public sealed class XmlSerializeService : ISerializeService
    {
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="PathTooLongException"></exception>
        /// <exception cref="SecurityException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public void SerializeToFile<T>(string fileName, T dto, Type[] extraTypes)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException(nameof(fileName));
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var serializer = new XmlSerializer(dto.GetType(), extraTypes);
            using (var file = new StreamWriter(fileName))
            {
                serializer.Serialize(file, dto);
            }
        }

        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="IOException"></exception>
        public T DeserializeFromFile<T>(string fileName, Type[] extraTypes)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException(nameof(fileName));

            var ser = new XmlSerializer(typeof(T), extraTypes);
            using (var tr = new StreamReader(fileName))
            {
                return (T)ser.Deserialize(tr);
            }
        }
    }
}