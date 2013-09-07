using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.DirectoryServices;
using LanExchange.SDK;

namespace LanExchange.Plugin.Users
{
    internal sealed class UserFiller : IPanelFiller
    {
        public bool IsParentAccepted(PanelItemBase parent)
        {
            return (parent != null) && (parent == PluginUsers.ROOT_OF_DNS);
        }

        private string SearchResult_GetString(SearchResult sr, string index)
        {
            string result = string.Empty;
            try
            {
                if (sr.Properties.Contains(index))
                    result = sr.Properties[index][0].ToString();
            }
            catch(Exception ex)
            {
                Debug.Print(ex.Message);
            }
            return result;
        }

        [Localizable(false)]
        public void Fill(PanelItemBase parent, ICollection<PanelItemBase> result)
        {
            var startPath = LdapUtils.GetUserPath(LdapUtils.GetCurrentUserName());
            startPath = LdapUtils.GetDCNameFromPath(startPath);
            using (var searcher = new DirectorySearcher())
            {
                // execute filter query to Active Directory
                searcher.SearchRoot = new DirectoryEntry(startPath);
                //searcher.PageSize = 10000;
                searcher.Filter = "(objectCategory=person)"; // lockoutTime
                //var filter = "(&(&(|(&(objectCategory=person)(objectSid=*)(!samAccountType:1.2.840.113556.1.4.804:=3))(&(objectCategory=person)(!objectSid=*))(&(objectCategory=group)(groupType:1.2.840.113556.1.4.804:=14)))objectCategory=user)(cn=khmau.isup_builder)))";
                //var filter = "(&(&(&(objectCategory=person)(objectClass=user)(lockoutTime:1.2.840.113556.1.4.804:=4294967295))))";
                //var filter = "(&(objectClass=user)(userAccountControl:1.2.840.113556.1.4.803:=16))";
                //var filter = "((!userAccountControl:1.2.840.113556.1.4.803:=2))";
                //var filter = "(!(userAccountControl:1.2.840.113556.1.4.803:=2))";
                searcher.PropertiesToLoad.Add("cn");
                searcher.PropertiesToLoad.Add("description");
                searcher.PropertiesToLoad.Add("company");
                searcher.PropertiesToLoad.Add("department");
                searcher.PropertiesToLoad.Add("title");
                searcher.PropertiesToLoad.Add("sAMAccountName");
                searcher.PropertiesToLoad.Add("mail");
                searcher.PropertiesToLoad.Add("telephoneNumber");
                searcher.PropertiesToLoad.Add("userAccountControl");

                try
                {
                    foreach (SearchResult row in searcher.FindAll())
                    {
                        var name = SearchResult_GetString(row, "cn");

                        var user = new UserPanelItem(parent, name);
                        user.UserAccControl = uint.Parse(SearchResult_GetString(row, "useraccountcontrol"));
                        user.Description = SearchResult_GetString(row, "description");
                        user.Company = SearchResult_GetString(row, "company");
                        user.Department = SearchResult_GetString(row, "department");
                        user.Title = SearchResult_GetString(row, "title");
                        user.Account = SearchResult_GetString(row, "sAMAccountName");
                        user.Email = SearchResult_GetString(row, "mail");
                        user.WorkPhone = SearchResult_GetString(row, "telephoneNumber");
                        //user.WorkPhone = "0x" + user.UserAccControl.ToString("X");
                        //user.Description = row["lockoutTime"].ToString();
                        result.Add(user);
                    }
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                }
            }
        }

        public Type GetFillType()
        {
            return typeof (UserPanelItem);
        }
    }
}