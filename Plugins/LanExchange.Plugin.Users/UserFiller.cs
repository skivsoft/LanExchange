using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.DirectoryServices;
using System.Windows.Forms;
using LanExchange.SDK;

namespace LanExchange.Plugin.Users
{
    internal sealed class UserFiller : IPanelFiller
    {
        const string CN = "cn";
        const string TITLE = "title";
        const string WORK_PHONE = "telephoneNumber";
        const string OFFICE = "physicalDeliveryOfficeName";
        const string DEPARTMENT = "department";
        const string EMAIL = "mail";
        const string COMPANY = "company";
        const string NICKNAME = "mailNickname";
        const string LEGACY_LOGON = "sAMAccountName";
        const string DESCRIPTION = "description";
        const string ACCOUNT_CONTROL = "userAccountControl";
        const string EMPLOYEE_ID = "employeeID";

        public bool IsParentAccepted(PanelItemBase parent)
        {
            return (parent is PanelItemRoot) && (parent.Name == PluginUsers.ROOT_OF_DNS.Name);
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
            var startPath = LdapUtils.GetUserPath(SystemInformation.UserName); // "u770503350189"
            
            startPath = LdapUtils.GetDCNameFromPath(startPath, 2);
            using (var searcher = new DirectorySearcher())
            {
                // execute filter query to Active Directory
                searcher.SearchRoot = new DirectoryEntry(startPath);
                searcher.PageSize = Int32.MaxValue;
                searcher.Filter = "(objectCategory=person)"; // lockoutTime
                //var filter = "(&(&(|(&(objectCategory=person)(objectSid=*)(!samAccountType:1.2.840.113556.1.4.804:=3))(&(objectCategory=person)(!objectSid=*))(&(objectCategory=group)(groupType:1.2.840.113556.1.4.804:=14)))objectCategory=user)(cn=khmau.isup_builder)))";
                //var filter = "(&(&(&(objectCategory=person)(objectClass=user)(lockoutTime:1.2.840.113556.1.4.804:=4294967295))))";
                //var filter = "(&(objectClass=user)(userAccountControl:1.2.840.113556.1.4.803:=16))";
                //var filter = "((!userAccountControl:1.2.840.113556.1.4.803:=2))";
                //var filter = "(!(userAccountControl:1.2.840.113556.1.4.803:=2))";
                searcher.PropertiesToLoad.Add(CN);
                searcher.PropertiesToLoad.Add(TITLE);
                searcher.PropertiesToLoad.Add(WORK_PHONE);
                searcher.PropertiesToLoad.Add(OFFICE);
                searcher.PropertiesToLoad.Add(DEPARTMENT);
                searcher.PropertiesToLoad.Add(EMAIL);
                searcher.PropertiesToLoad.Add(COMPANY);
                searcher.PropertiesToLoad.Add(NICKNAME);
                searcher.PropertiesToLoad.Add(LEGACY_LOGON);
                searcher.PropertiesToLoad.Add(DESCRIPTION);
                searcher.PropertiesToLoad.Add(ACCOUNT_CONTROL);
                searcher.PropertiesToLoad.Add(EMPLOYEE_ID);
                try
                {
                    var results = searcher.FindAll();
                    foreach (SearchResult row in results)
                    {
                        // skip disabled users
                        var userAccControl = int.Parse(SearchResult_GetString(row, ACCOUNT_CONTROL));
                        if ((userAccControl & 2) != 0) continue;

                        var user = new UserPanelItem(parent, SearchResult_GetString(row, CN));
                        user.Title = SearchResult_GetString(row, TITLE);
                        user.WorkPhone = SearchResult_GetString(row, WORK_PHONE);
                        user.Office = SearchResult_GetString(row, OFFICE);
                        user.Department = SearchResult_GetString(row, DEPARTMENT);
                        user.Email = SearchResult_GetString(row, EMAIL);
                        user.Company = SearchResult_GetString(row, COMPANY);
                        user.Nickname = SearchResult_GetString(row, NICKNAME);
                        user.LegacyLogon = SearchResult_GetString(row, LEGACY_LOGON);
                        user.Description = SearchResult_GetString(row, DESCRIPTION);
                        user.UserAccControl = int.Parse(SearchResult_GetString(row, ACCOUNT_CONTROL));
                        user.EmployeeID = SearchResult_GetString(row, EMPLOYEE_ID);
                        //user.WorkPhone = "0x" + user.UserAccControl.ToString("X");
                        //user.Description = row["lockoutTime"].ToString();
                        result.Add(user);
                    }
                    results.Dispose();
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