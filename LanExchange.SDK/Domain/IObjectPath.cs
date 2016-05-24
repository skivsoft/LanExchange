using System;
using System.Collections.Generic;

namespace LanExchange.SDK.Domain
{
    /// <summary>
    /// The ObjectPath interface.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IObjectPath<T> : IEnumerable<T>
    {
        /// <summary>
        /// Occurs when path has changed.
        /// </summary>
        event EventHandler Changed;

        /// <summary>
        /// Clears this instance.
        /// </summary>
        void Clear();

        /// <summary>
        /// Pushes the specified segment.
        /// </summary>
        /// <param name="segment">The segment.</param>
        void Push(T segment);

        /// <summary>
        /// Pops this instance.
        /// </summary>
        void Pop();

        /// <summary>
        /// Peeks this instance.
        /// </summary>
        /// <returns></returns>
        Option<T> Peek();
    }
}