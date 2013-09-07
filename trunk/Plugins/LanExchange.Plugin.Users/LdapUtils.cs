using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.DirectoryServices;
using System.Security.Principal;

namespace LanExchange.Plugin.Users
{
    internal static class LdapUtils
    {
        internal static string GetCurrentUserName()
        {
            var user = WindowsIdentity.GetCurrent();
            if (user != null)
            {
                string[] userName = user.Name.Split('\\');
                return userName.Length > 1 ? userName[1] : userName[0];
            }
            return string.Empty;
        }

        [Localizable(false)]
        internal static string GetUserPath(string userName)
        {
            string result = string.Empty;
            using (var searcher = new DirectorySearcher(Users.LDAP_PREFIX))
            {
                searcher.Filter = string.Format("(&(objectCategory=person)(sAMAccountName={0}))", userName);
                var found = searcher.FindOne();
                if (found != null)
                    result = PathFixer.GetLdapContainer(found.Path);
            }
            return result;
        }

        [Localizable(false)]
        internal static string GetDCNameFromPath(string path)
        {
            var arr = path.Split(',');
            var list = new List<string>();
            for (int index = arr.Length - 1; index >= 0; index--)
                if (arr[index].StartsWith("DC="))
                    list.Insert(0, arr[index]);
                else
                    break;
            return string.Join(",", list.ToArray());
        }
    }
}