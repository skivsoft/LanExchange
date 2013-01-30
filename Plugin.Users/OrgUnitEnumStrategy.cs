//using System.Data;
//using System.IO;
//using LanExchange.Model;
//using LanExchange.Sdk;

//namespace LanExchange.Plugin
//{
//    internal class OrgUnitEnumStrategy : PanelStrategyBase
//    {
//        private const string LDAP_START_PATH = "LDAP://OU=AZF,OU=Users,OU=Aksu,OU=KazChrome,DC=KZ,DC=ENRC,DC=COM";

//        public override void Algorithm()
//        {
//            //if (Users.Provider == null) return;
//            // get Active Directory executor from main program
//            //var rootEntry = Users.Provider.GetService(typeof (IADExecutor)) as IADExecutor;
//            var rootEntry = new ConcreteADExecutor();
//            //if (rootEntry == null) return;

//            // connect to Active Directory
//            rootEntry.Connect(LDAP_START_PATH);

//            // execute filter query to Active Directory
//            var filter = "(&(objectCategory=person)(objectClass=user))";
//            DataTable result = rootEntry.Query(filter, new string[] { "description", "title", "mail" });
//            if (result == null) return;

//            //Result.Add(new UserPanelItem(PanelItemBase.DoubleDot));
//            Result.Add(new OrgUnitPanelItem(PanelItemBase.DoubleDot));

//            var fixer = new PathFixer();
//            var distinct = new PathDistinct();

//            foreach (DataRow row in result.Rows)
//                distinct.Add(Path.GetDirectoryName(fixer.FixLdapPath(row["adspath"].ToString())));
//            rootEntry.Dispose();

//            var prefix = distinct.Prefix;
//            if (prefix.Split('\\').Length <= 4)
//                prefix = string.Empty;
//            foreach (var str in distinct.Items)
//            {
//                string sUnit = string.IsNullOrEmpty(prefix) ? str : string.Format("..{0}", str.Substring(prefix.Length));
//                var unit = new OrgUnitPanelItem(sUnit);
//                Result.Add(unit);
//            }
//        }

//        public override bool IsSubjectAccepted(ISubject subject)
//        {
//            return false;
//            if (subject is UserPanelItem)
//                return false;
//            return Subject != ConcreteSubject.Root;
//        }
//    }
//}