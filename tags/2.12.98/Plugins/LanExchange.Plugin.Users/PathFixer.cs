using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace LanExchange.Plugin.Users
{
    public static class PathFixer
    {
        private const char PathDelimiter = '\\';

        [Localizable(false)]
        public static string FixLdapPath(string path)
        {
            if (path == null) return string.Empty;

            if (!path.ToUpper(CultureInfo.InvariantCulture).StartsWith("LDAP://"))
                return string.Empty;

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
