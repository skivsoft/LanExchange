using System.Data;
using LanExchange.SDK;

namespace LanExchange.Plugin.Users
{
    internal class UserEnumStrategy : PanelStrategyBase
    {
        private const string LdapStartPath = "LDAP://OU=AZF,OU=Users,OU=Aksu,OU=KazChrome,DC=KZ,DC=ENRC,DC=COM";

        public override void Algorithm()
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
            DataTable result = rootEntry.Query(filter, new[] { "description", "title", "useraccountcontrol" });
            if (result == null) return;

            Result.Add(new UserPanelItem(PanelItemBase.s_DoubleDot));

            foreach (DataRow row in result.Rows)
            {
                var name = row["description"].ToString();
                var user = new UserPanelItem(name);
                user.UserAccControl = uint.Parse(row["useraccountcontrol"].ToString());
                user.Description = user.UserAccControl.ToString("X");
                //user.Description = row["lockoutTime"].ToString();
                Result.Add(user);
            }
            rootEntry.Dispose();
        }

        public override bool IsSubjectAccepted(ISubject subject)
        {
            if (subject is UserPanelItem)
                return false;
            return !Equals(Subject, ConcreteSubject.s_Root);
        }
    }
}
