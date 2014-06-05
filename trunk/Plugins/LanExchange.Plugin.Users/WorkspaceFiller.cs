using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using LanExchange.SDK;

namespace LanExchange.Plugin.Users
{
    public class WorkspaceFiller : IPanelFiller
    {
        public bool IsParentAccepted(PanelItemBase parent)
        {
            return parent is WorkspaceRoot || (parent is UserPanelItem && !(parent.Parent is WorkspacePanelItem));
        }

        public void SyncFill(PanelItemBase parent, ICollection<PanelItemBase> result)
        {
            
        }

        [Localizable(false)]
        public void AsyncFill(PanelItemBase parent, ICollection<PanelItemBase> result)
        {
            string startPath;

            if (parent is UserPanelItem)
                startPath = (parent as UserPanelItem).AdsPath;
            else
                startPath = LdapUtils.GetUserPath(App.Resolve<IScreenService>().UserName);

            using (var searcher = new DirectorySearcher())
            {
                // execute filter query to Active Directory
                searcher.SearchRoot = new DirectoryEntry(startPath);
                searcher.PageSize = Int32.MaxValue;
                searcher.Filter = "(objectCategory=person)"; // lockoutTime
                searcher.PropertiesToLoad.Add(Constants.MEMBER_OF);
                try
                {
                    var row = searcher.FindOne();

                    var list = row.Properties[Constants.MEMBER_OF];
                    foreach (var value in list)
                    {
                        var valueStr = (string) value;

                        var workspace = new WorkspacePanelItem(parent, LdapUtils.GetLdapValue(valueStr));
                        workspace.AdsPath = valueStr;
                        result.Add(workspace);
                    }
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                }
            }
        }
    }
}
