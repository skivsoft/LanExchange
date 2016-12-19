using System;

namespace LanExchange.Application.Interfaces
{
    /// <summary>
    /// The domain services interface.
    /// </summary>
    public interface IDomainServices
    {
        /// <summary>
        /// Serializes to file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="dto">The dto.</param>
        /// <param name="extraTypes">The extra types.</param>
        void SerializeToFile<T>(string fileName, T dto, Type[] extraTypes);

        /// <summary>
        /// Deserializes from file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="extraTypes">The extra types.</param>
        /// <returns></returns>
        T DeserializeFromFile<T>(string fileName, Type[] extraTypes);
    }
}