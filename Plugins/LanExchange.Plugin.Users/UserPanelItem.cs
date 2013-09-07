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
            columnManager.RegisterColumn(typeof(UserPanelItem), new PanelColumnHeader(Resources.Description) {Width = 200});
            columnManager.RegisterColumn(typeof(UserPanelItem), new PanelColumnHeader(Resources.Company));
            columnManager.RegisterColumn(typeof(UserPanelItem), new PanelColumnHeader(Resources.Department));
            columnManager.RegisterColumn(typeof(UserPanelItem), new PanelColumnHeader(Resources.Title));
            columnManager.RegisterColumn(typeof(UserPanelItem), new PanelColumnHeader(Resources.WorkPhone) {Width = 80});
            columnManager.RegisterColumn(typeof(UserPanelItem), new PanelColumnHeader(Resources.Email) {Visible = false} );
            columnManager.RegisterColumn(typeof(UserPanelItem), new PanelColumnHeader(Resources.Account) {Visible = false} );
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

        public string Description { get; set; }

        public string Company { get; set; }

        public string Department { get; set; }

        public string Title { get; set; }

        public string Account { get; set; }

        public string Email { get; set; }

        public string WorkPhone { get; set; }

        public override int CountColumns
        {
            get { return base.CountColumns + 7; }
        }

        public override IComparable GetValue(int index)
        {
            switch (index)
            {
                case 1: return Description;
                case 2: return Company;
                case 3: return Department;
                case 4: return Title;
                case 5: return WorkPhone;
                case 6: return Email;
                case 7: return Account;
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
            result.Description = Description;
            result.Company = Company;
            result.Department = Department;
            result.Title = Title;
            result.UserAccControl = UserAccControl;
            result.WorkPhone = WorkPhone;
            result.Email = Email;
            return result;
        }

    }
}
