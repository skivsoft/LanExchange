using System.Data;
using System.IO;
using LanExchange.Model;
using LanExchange.Sdk;

namespace LanExchange.Plugin
{
    internal class UserEnumStrategy : PanelStrategyBase
    {
        private const string LDAP_START_PATH = "LDAP://OU=AZF,OU=Users,OU=Aksu,OU=KazChrome,DC=KZ,DC=ENRC,DC=COM";

        public override void Algorithm()
        {
            //if (Users.Provider == null) return;
            var rootEntry = new ConcreteADExecutor();
            //if (rootEntry == null) return;

            // connect to Active Directory
            rootEntry.Connect(LDAP_START_PATH);
            
            // execute filter query to Active Directory
            var filter = "(&(objectCategory=person)(objectClass=user)(!lockoutTime=0))";
            //var filter = "(&(objectCategory=person)(objectClass=user)(!userAccountControl:1.2.840.113556.1.4.803:=16))";
            //var filter = "((!userAccountControl:1.2.840.113556.1.4.803:=2))";
            //var filter = "(!(userAccountControl:1.2.840.113556.1.4.803:=2))";
            DataTable result = rootEntry.Query(filter, new[] {"description", "title", "useraccountcontrol"});
            if (result == null) return;

            Result.Add(new UserPanelItem(PanelItemBase.DoubleDot));

            foreach (DataRow row in result.Rows)
            {
                var name = row["description"].ToString();
                var user = new UserPanelItem(name);
                user.UserAccountControl = uint.Parse(row["useraccountcontrol"].ToString());
                user.Description = user.UserAccountControl.ToString("X");
                //user.Description = row["lockoutTime"].ToString();
                Result.Add(user);
            }
            rootEntry.Dispose();
        }

        public override bool IsSubjectAccepted(ISubject subject)
        {
            if (subject is UserPanelItem)
                return false;
            return Subject != ConcreteSubject.Root;
        }
    }
}
