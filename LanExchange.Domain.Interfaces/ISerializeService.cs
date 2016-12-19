using System;
using System.IO;
using System.Security;

namespace LanExchange.Domain.Interfaces
{
    /// <summary>
    /// The serialize service interface.
    /// </summary>
    public interface ISerializeService
    {
        /// <summary>
        /// Serializes to file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="dto">The dto.</param>
        /// <param name="extraTypes">The extra types.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="PathTooLongException"></exception>
        /// <exception cref="SecurityException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        void SerializeToFile<T>(string fileName, T dto, Type[] extraTypes);

        /// <summary>
        /// Deserializes from file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="extraTypes">The extra types.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="IOException"></exception>
        T DeserializeFromFile<T>(string fileName, Type[] extraTypes);
    }
}