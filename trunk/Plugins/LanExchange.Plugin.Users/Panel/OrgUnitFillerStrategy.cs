using System.Collections.Generic;
using System.Data;
using System.IO;
using LanExchange.SDK;

namespace LanExchange.Plugin.Users.Panel
{
    internal class OrgUnitFillerStrategy : IPanelFillerStrategy
    {
        private const string LdapStartPath = "";

        public bool IsParentAccepted(PanelItemBase parent)
        {
            return parent == Users.ROOT_OF_ORGUNITS;
        }

        public void Algorithm(PanelItemBase parent, ICollection<PanelItemBase> result)
        {
            //if (Users.Provider == null) return;
            // get Active Directory executor from main program
            //var rootEntry = Users.Provider.GetService(typeof (IADExecutor)) as IADExecutor;
            var rootEntry = new ConcreteADExecutor();
            //if (rootEntry == null) return;

            // connect to Active Directory
            rootEntry.Connect(LdapStartPath);

            // execute filter query to Active Directory
            const string filter = "(&(objectCategory=person)(objectClass=user))";
            DataTable resultTable = rootEntry.Query(filter, new[] { "description", "title", "mail" });
            if (resultTable == null) return;

            //Result.Add(new UserPanelItem(PanelItemBase.s_DoubleDot));
            result.Add(new PanelItemDoubleDot(parent));

            var fixer = new PathFixer();
            var distinct = new PathDistinct();

            foreach (DataRow row in resultTable.Rows)
                distinct.Add(Path.GetDirectoryName(fixer.FixLdapPath(row["adspath"].ToString())));
            rootEntry.Dispose();

            var prefix = distinct.Prefix;
            if (prefix.Split('\\').Length <= 4)
                prefix = string.Empty;
            foreach (var str in distinct.Items)
            {
                string sUnit = string.IsNullOrEmpty(prefix) ? str : string.Format("..{0}", str.Substring(prefix.Length));
                var unit = new OrgUnitPanelItem(null, sUnit);
                result.Add(unit);
            }
        }
    }
}