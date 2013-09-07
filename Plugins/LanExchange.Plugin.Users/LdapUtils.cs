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
            return String.Empty;
        }

        [Localizable(false)]
        internal static string GetUserPath(string userName)
        {
            string result = String.Empty;
            using (var searcher = new DirectorySearcher(PluginUsers.LDAP_PREFIX))
            {
                searcher.Filter = String.Format("(&(objectCategory=person)(sAMAccountName={0}))", userName);
                var found = searcher.FindOne();
                if (found != null)
                    result = found.Path;
            }
            return result;
        }

        [Localizable(false)]
        public static string GetLdapContainer(string ldapPath)
        {
            var index = ldapPath.IndexOf(',');
            if (index == -1)
                return String.Empty;
            return "LDAP://" + ldapPath.Remove(0, index + 1);
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
                {
                    list.Insert(0, arr[index]);
                    break;
                }
            return String.Join(",", list.ToArray());
        }
    }
}