using System;
using System.Collections.Generic;
using System.Globalization;

namespace LanExchange.Plugin.Users
{
    internal class StringEqualityComparer : IEqualityComparer<string>
    {
        public bool Equals(string x, string y)
        {
            return String.Compare(x, y, StringComparison.OrdinalIgnoreCase) == 0;
        }

        public int GetHashCode(string obj)
        {
            return obj.ToUpper(CultureInfo.InvariantCulture).GetHashCode();
        }
    }
}
