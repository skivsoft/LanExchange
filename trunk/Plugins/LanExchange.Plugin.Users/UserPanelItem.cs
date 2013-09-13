using System;
using LanExchange.Plugin.Users.Properties;
using LanExchange.SDK;

namespace LanExchange.Plugin.Users
{
    internal sealed class UserPanelItem : PanelItemBase
    {
        internal static void RegisterColumns(IPanelColumnManager columnManager)
        {
            columnManager.RegisterColumn(typeof(UserPanelItem), new PanelColumnHeader(Resources.UserName));
            columnManager.RegisterColumn(typeof(UserPanelItem), new PanelColumnHeader(Resources.Title));
            columnManager.RegisterColumn(typeof(UserPanelItem), new PanelColumnHeader(Resources.WorkPhone) { Width = 80 });
            columnManager.RegisterColumn(typeof(UserPanelItem), new PanelColumnHeader(Resources.Office));
            columnManager.RegisterColumn(typeof(UserPanelItem), new PanelColumnHeader(Resources.Department));
            columnManager.RegisterColumn(typeof(UserPanelItem), new PanelColumnHeader(Resources.Email));
            columnManager.RegisterColumn(typeof(UserPanelItem), new PanelColumnHeader(Resources.Company));
            columnManager.RegisterColumn(typeof(UserPanelItem), new PanelColumnHeader(Resources.Nickname));
            columnManager.RegisterColumn(typeof(UserPanelItem), new PanelColumnHeader(Resources.Pre2000Logon) { Visible = false });
            columnManager.RegisterColumn(typeof(UserPanelItem), new PanelColumnHeader(Resources.Description) { Visible = false, Width = 200 });
        }

        internal UserPanelItem(PanelItemBase parent, string name) : base(parent)
        {
            Name = name;
        }

        internal uint UserAccControl { get; set; }

        internal string LockoutTime { get; set; }

        internal bool IsAccountDisabled
        {
            get { return (UserAccControl & 2) != 0; }
        }

        public override string Name { get; set; }

        public string Title { get; set; }

        public string WorkPhone { get; set; }

        public string Office { get; set; }

        public string Department { get; set; }

        public string Email { get; set; }

        public string Company { get; set; }

        public string Nickname { get; set; }

        public string LegacyLogon { get; set; }

        public string Description { get; set; }

        public string EmployeeID { get; set; }

        public override int CountColumns
        {
            get { return base.CountColumns + 9; }
        }

        public override IComparable GetValue(int index)
        {
            switch (index)
            {
                case 1: return Title;
                case 2: return WorkPhone;
                case 3: return Office;
                case 4: return Department;
                case 5: return Email;
                case 6: return Company;
                case 7: return Nickname;
                case 8: return LegacyLogon;
                case 9: return Description;
                default:
                    return base.GetValue(index);
            }
        }

        public override string ImageName
        {
            get
            {
                return IsAccountDisabled ? PanelImageNames.UserDisabled : PanelImageNames.UserNormal;
            }
        }

        public override string ImageLegendText
        {
            get { return IsAccountDisabled ? Resources.Legend_UserDisabled : Resources.Legend_UserNormal; }
        }

        public override object Clone()
        {
            var result = new UserPanelItem(Parent, Name);
            result.Title = Title;
            result.WorkPhone = WorkPhone;
            result.Office = Office;
            result.Department = Department;
            result.Email = Email;
            result.Company = Company;
            result.Nickname = Nickname;
            result.LegacyLogon = LegacyLogon;
            result.Description = Description;
            result.UserAccControl = UserAccControl;
            result.EmployeeID = EmployeeID;
            return result;
        }

    }
}