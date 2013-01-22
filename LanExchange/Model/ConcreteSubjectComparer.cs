using System;
using System.Collections.Generic;
using System.Text;

namespace LanExchange.Model
{
    public class ConcreteSubjectComparer : IEqualityComparer<ISubject>
    {
        public bool Equals(ISubject x, ISubject y)
        {
            return String.Compare(x.Subject, y.Subject, StringComparison.Ordinal) == 0;
        }

        public int GetHashCode(ISubject obj)
        {
            return obj.Subject.GetHashCode();
        }
    }
}
