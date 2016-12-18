using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.DirectoryServices;

namespace LanExchange.Plugin.Users
{
    internal static class LdapUtils
    {
        [Localizable(false)]
        internal static string GetUserPath(string userName)
        {
            string result = string.Empty;
            using (var searcher = new DirectorySearcher(PluginUsers.LDAP_PREFIX))
            {
                searcher.Filter = string.Format("(&(objectCategory = person)(sAMAccountName={0}))", userName);
                var found = searcher.FindOne();
                if (found != null)
                    result = found.Path;
            }

            return result;
        }

        [Localizable(false)]
        internal static string GetLdapContainer(string ldapPath)
        {
            var index = ldapPath.IndexOf(',');
            if (index == -1)
                return string.Empty;
            return "LDAP:// " + ldapPath.Remove(0, index + 1);
        }

        /// <summary>
        /// Split distinguished name(AdsPath) to array of string.
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

            return "LDAP:// " + string.Join(",", list.ToArray());
        }

        [Localizable(false)]
        internal static string GetLdapValue(string path)
        {
            var arr = DNSplit(path);
            var result = arr[0].Split('=')[1];
            return result;
        }

        internal static string SearchResult_GetString(SearchResult sr, string index)
        {
            string result = string.Empty;
            try
            {
                if (sr.Properties.Contains(index))
                    result = sr.Properties[index][0].ToString();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }

            return result;
        }

        internal static string UnescapeName(string name)
        {
            string result = string.Empty;
            bool visible = true;
            foreach (var ch in name)
            {
                if (ch == '\\')
                {
                    visible = false;
                    continue;
                }

                result += ch;
                if (!visible) visible = true;
            }

            return result;
        }
    }
}