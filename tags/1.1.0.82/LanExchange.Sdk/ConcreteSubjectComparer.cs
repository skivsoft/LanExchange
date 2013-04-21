using System;
using System.Collections.Generic;

namespace LanExchange.Sdk
{
    /// <summary>
    /// IEqualityComparer for ISubject for searching it in Dictionary&lt;ISubject,...&gt;
    /// </summary>
    public class ConcreteSubjectComparer : IEqualityComparer<ISubject>
    {
        /// <summary>
        /// Equalses the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public bool Equals(ISubject x, ISubject y)
        {
            if ((object)x == null)
                return (object)y == null;
            else if ((object) y == null)
                return false;
            return String.Compare(x.Subject, y.Subject, StringComparison.Ordinal) == 0;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public int GetHashCode(ISubject obj)
        {
            if ((object) obj == null) return 0;
            return obj.Subject.GetHashCode();
        }
    }
}
