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

        /// <summary>
        /// Split distinguished name (DN) to array of string.
        /// This function skips chars after '\'.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        internal static string[] DNSplit(string path)
        {
            path += ',';
            var list = new List<string>();
            string word = string.Empty;
            bool masked = false;
            for (int i = 0; i < path.Length; i++)
            {
                if (!masked && path[i] == '\\')
                {
                    word += path[i];
                    masked = true;
                    continue;
                }
                if ((path[i] == ',' || path[i] == ';') && !masked)
                {
                    list.Add(word);
                    word = string.Empty;
                }
                else
                {
                    word += path[i];
                    masked = false;
                }
            }
            return list.ToArray();
        }

        [Localizable(false)]
        internal static string GetDCNameFromPath(string path, int numOrgUnit)
        {
            var arr = DNSplit(path);
            var list = new List<string>();
            for (int index = arr.Length - 1; index >= 0; index--)
                if (arr[index].StartsWith("DC="))
                    list.Insert(0, arr[index]);
                else
                {
                    if (numOrgUnit > 0)
                    {
                        list.Insert(0, arr[index]);
                        numOrgUnit--;
                    }
                    if (numOrgUnit == 0)
                        break;
                }
            return "LDAP://" + String.Join(",", list.ToArray());
        }

        [Localizable(false)]
        internal static string GetLdapValue(string path)
        {
            var arr = DNSplit(path);
            var result = arr[0].Split('=')[1];
            return result;
        }
    }
}