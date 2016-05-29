using System.Collections;
using System.Collections.Generic;

namespace LanExchange.SDK.Domain
{
    /// <summary>
    /// The Option class implementation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="System.Collections.Generic.IEnumerable{T}" />
    public class Option<T> : IEnumerable<T>
    {
        private readonly T[] data;

        private Option(T[] data)
        {
            this.data = data;
        }

        /// <summary>
        /// Creates the Option instance with single element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        public static Option<T> Create(T element)
        {
            return new Option<T>(new[] { element });
        }

        /// <summary>
        /// Creates the Option instance with no elements.
        /// </summary>
        /// <returns></returns>
        public static Option<T> CreateEmpty()
        {
            return new Option<T>(new T[0]);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)data).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
