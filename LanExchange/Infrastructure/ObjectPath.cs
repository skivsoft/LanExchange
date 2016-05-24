using LanExchange.SDK.Domain;
using System;
using System.Collections;
using System.Collections.Generic;

namespace LanExchange.Infrastructure
{
    /// <summary>
    /// Path to nested object.
    /// </summary>
    internal sealed class ObjectPath<T> : IObjectPath<T>
    {
        private readonly LinkedList<T> segments;

        /// <summary>
        /// Occurs when path has changed.
        /// </summary>
        public event EventHandler Changed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectPath{TPanelItemBase}"/> class.
        /// </summary>
        public ObjectPath()
        {
            segments = new LinkedList<T>();
        }

        private void OnChanged()
        {
            if (Changed != null)
                Changed(this, EventArgs.Empty);
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            if (segments.Count > 0)
            {
                segments.Clear();
                OnChanged();
            }
        }

        /// <summary>
        /// Pushes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Push(T item)
        {
            segments.AddLast(item);
            OnChanged();
        }

        /// <summary>
        /// Pops this instance.
        /// </summary>
        public void Pop()
        {
            if (segments.Count > 0)
            {
                segments.RemoveLast();
                OnChanged();
            }
        }

        /// <summary>
        /// Peeks this instance.
        /// </summary>
        /// <returns></returns>
        public Option<T> Peek()
        {
            if (segments.Count == 0)
                return Option<T>.CreateEmpty();

            return Option<T>.Create(segments.Last.Value);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            return segments.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}