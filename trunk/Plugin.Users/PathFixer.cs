using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LanExchange.Plugin
{
    public class PathFixer
    {
        public const char PathDelimiter = '\\';

        public string FixLdapPath(string path)
        {
            if (path == null) return String.Empty;

            if (!path.ToUpper(CultureInfo.InvariantCulture).StartsWith("LDAP://"))
                return String.Empty;

            var list = path.Remove(0, 7).Split(',');
            var result = new StringBuilder();
            for (int i = list.Length - 1; i >= 0; i--)
            {
                if (result.Length > 0)
                    result.Append(PathDelimiter);
                string str = list[i];
                var listEQ = str.Split('=');
                if (listEQ.Length >= 2)
                    str = listEQ[1];
                result.Append(str);
            }
            return result.ToString();
        }
    }
}
