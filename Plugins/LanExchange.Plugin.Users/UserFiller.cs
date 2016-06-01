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
            return parent is UserRoot || parent is WorkspacePanelItem;
        }

        private void SetupPropertiesToLoad(DirectorySearcher searcher)
        {
            searcher.PropertiesToLoad.Add(Constants.CN);
            searcher.PropertiesToLoad.Add(Constants.TITLE);
            searcher.PropertiesToLoad.Add(Constants.WORK_PHONE);
            searcher.PropertiesToLoad.Add(Constants.OFFICE);
            searcher.PropertiesToLoad.Add(Constants.DEPARTMENT);
            searcher.PropertiesToLoad.Add(Constants.EMAIL);
            searcher.PropertiesToLoad.Add(Constants.COMPANY);
            searcher.PropertiesToLoad.Add(Constants.NICKNAME);
            searcher.PropertiesToLoad.Add(Constants.LEGACY_LOGON);
            searcher.PropertiesToLoad.Add(Constants.DESCRIPTION);
            searcher.PropertiesToLoad.Add(Constants.ACCOUNT_CONTROL);
            searcher.PropertiesToLoad.Add(Constants.EMPLOYEE_ID);
        }

        private UserPanelItem BuildUserFromSearchResult(PanelItemBase parent, SearchResult row)
        {
            var user = new UserPanelItem(parent, LdapUtils.SearchResult_GetString(row, Constants.CN));
            user.AdsPath = LdapUtils.SearchResult_GetString(row, Constants.ADSPATH);
            user.Title = LdapUtils.SearchResult_GetString(row, Constants.TITLE);
            user.WorkPhone = LdapUtils.SearchResult_GetString(row, Constants.WORK_PHONE);
            user.Office = LdapUtils.SearchResult_GetString(row, Constants.OFFICE);
            user.Department = LdapUtils.SearchResult_GetString(row, Constants.DEPARTMENT);
            user.Email = LdapUtils.SearchResult_GetString(row, Constants.EMAIL);
            user.Company = LdapUtils.SearchResult_GetString(row, Constants.COMPANY);
            user.Nickname = LdapUtils.SearchResult_GetString(row, Constants.NICKNAME);
            user.LegacyLogon = LdapUtils.SearchResult_GetString(row, Constants.LEGACY_LOGON);
            user.Description = LdapUtils.SearchResult_GetString(row, Constants.DESCRIPTION);
            user.UserAccControl = int.Parse(LdapUtils.SearchResult_GetString(row, Constants.ACCOUNT_CONTROL));
            user.EmployeeID = LdapUtils.SearchResult_GetString(row, Constants.EMPLOYEE_ID);
            //user.WorkPhone = "0x" + user.UserAccControl.ToString("X");
            //user.Description = row["lockoutTime"].ToString();
            return user;
        }

        [Localizable(false)]
        private void FillUsers(ref PanelItemBase parent, ref ICollection<PanelItemBase> result, string startPath)
        {
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
                SetupPropertiesToLoad(searcher);
                try
                {
                    var results = searcher.FindAll();
                    foreach (SearchResult row in results)
                    {
                        // skip disabled users
                        var userAccControl = int.Parse(LdapUtils.SearchResult_GetString(row, Constants.ACCOUNT_CONTROL));
                        if ((userAccControl & 2) != 0) continue;

                        result.Add(BuildUserFromSearchResult(parent, row));
                    }
                    results.Dispose();
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                }
            }
        }

        [Localizable(false)]
        private void FillUsersWithSameGroup(ref PanelItemBase parent, ref ICollection<PanelItemBase> result, string startPath, string group)
        {
            using (var searcher = new DirectorySearcher())
            {
                searcher.SearchRoot = new DirectoryEntry(startPath);
                searcher.PageSize = Int32.MaxValue;
                searcher.Filter = "(objectCategory=person)";
                SetupPropertiesToLoad(searcher);
                searcher.PropertiesToLoad.Add(Constants.MEMBER_OF);
                try
                {
                    var results = searcher.FindAll();
                    foreach (SearchResult row in results)
                    {
                        // skip disabled users
                        var userAccControl = int.Parse(LdapUtils.SearchResult_GetString(row, Constants.ACCOUNT_CONTROL));
                        if ((userAccControl & 2) != 0) continue;

                        var list = row.Properties[Constants.MEMBER_OF];
                        if (list.Contains(group))
                            result.Add(BuildUserFromSearchResult(parent, row));
                    }
                    results.Dispose();
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                }
            }
        }


        [Localizable(false)]
        public void AsyncFill(PanelItemBase parent, ICollection<PanelItemBase> result)
        {
            var startPath = LdapUtils.GetDCNameFromPath(LdapUtils.GetUserPath(PluginUsers.sysInfoService.UserName), 2);
            var workspace = parent as WorkspacePanelItem;
            if (workspace != null)
                FillUsersWithSameGroup(ref parent, ref result, startPath, workspace.AdsPath);
            else
                FillUsers(ref parent, ref result, startPath);
        }

        public void SyncFill(PanelItemBase parent, ICollection<PanelItemBase> result)
        {
        }
    }
}