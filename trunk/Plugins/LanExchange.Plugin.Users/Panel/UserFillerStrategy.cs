using System.Collections.Generic;
using System.Data;
using LanExchange.SDK;

namespace LanExchange.Plugin.Users.Panel
{
    internal class UserFillerStrategy : IPanelFillerStrategy
    {
        private const string LdapStartPath = "";

        public bool IsParentAccepted(PanelItemBase parent)
        {
            return (parent != null) && (parent.GetType() == typeof (OrgUnitPanelItem));
        }

        public void Algorithm(PanelItemBase parent, ICollection<PanelItemBase> result)
        {
            //if (Users.Provider == null) return;
            var rootEntry = new ConcreteADExecutor();
            //if (rootEntry == null) return;

            // connect to Active Directory
            rootEntry.Connect(LdapStartPath);

            // execute filter query to Active Directory
            const string filter = "(&(objectCategory=person)(objectClass=user))"; // lockoutTime
            //var filter = "(&(objectCategory=person)(objectClass=user)(!userAccountControl:1.2.840.113556.1.4.803:=16))";
            //var filter = "((!userAccountControl:1.2.840.113556.1.4.803:=2))";
            //var filter = "(!(userAccountControl:1.2.840.113556.1.4.803:=2))";
            DataTable resultTable = rootEntry.Query(filter, new[] { "description", "title", "useraccountcontrol" });
            if (resultTable == null) return;

            result.Add(new UserPanelItem(null, PanelItemBase.s_DoubleDot));

            foreach (DataRow row in resultTable.Rows)
            {
                var name = row["description"].ToString();
                var user = new UserPanelItem(null, name);
                user.UserAccControl = uint.Parse(row["useraccountcontrol"].ToString());
                user.Description = user.UserAccControl.ToString("X");
                //user.Description = row["lockoutTime"].ToString();
                result.Add(user);
            }
            rootEntry.Dispose();
        }
    }
}
