using System;
using System.ComponentModel;
using System.Xml.Serialization;
using LanExchange.Plugin.Users.Properties;
using LanExchange.SDK;

namespace LanExchange.Plugin.Users
{
    public sealed class UserPanelItem : PanelItemBase
    {
        internal static void RegisterColumns(IPanelColumnManager columnManager)
        {
            var typeName = typeof (UserPanelItem).Name;
            columnManager.RegisterColumn(typeName, new PanelColumnHeader(Resources.UserName));
            columnManager.RegisterColumn(typeName, new PanelColumnHeader(Resources.Title));
            columnManager.RegisterColumn(typeName, new PanelColumnHeader(Resources.WorkPhone) { Width = 80 });
            columnManager.RegisterColumn(typeName, new PanelColumnHeader(Resources.Office));
            columnManager.RegisterColumn(typeName, new PanelColumnHeader(Resources.Department));
            columnManager.RegisterColumn(typeName, new PanelColumnHeader(Resources.Email));
            columnManager.RegisterColumn(typeName, new PanelColumnHeader(Resources.Company));
            columnManager.RegisterColumn(typeName, new PanelColumnHeader(Resources.Nickname));
            columnManager.RegisterColumn(typeName, new PanelColumnHeader(Resources.Pre2000Logon) { Visible = false });
            columnManager.RegisterColumn(typeName, new PanelColumnHeader(Resources.Description) { Visible = false, Width = 200 });
        }

        public UserPanelItem()
        {
        }

        public UserPanelItem(PanelItemBase parent, string name) : base(parent)
        {
            Name = name;
        }

        [XmlAttribute]
        public override string Name { get; set; }

        [Localizable(false)]
        public override string FullName
        {
            get
            {
                return "CN=" + base.FullName;
            }
        }

        [XmlAttribute]
        public string Title { get; set; }

        [XmlAttribute]
        public string WorkPhone { get; set; }

        [XmlAttribute]
        public string Office { get; set; }

        [XmlAttribute]
        public string Department { get; set; }

        [XmlAttribute]
        public string Email { get; set; }

        [XmlAttribute]
        public string Company { get; set; }

        [XmlAttribute]
        public string Nickname { get; set; }

        [XmlAttribute]
        public string LegacyLogon { get; set; }

        [XmlAttribute]
        public string Description { get; set; }

        [XmlAttribute]
        public string EmployeeID { get; set; }

        [XmlAttribute]
        public int UserAccControl { get; set; }

        public string LockoutTime { get; set; }

        public bool IsAccountDisabled
        {
            get { return (UserAccControl & 2) != 0; }
        }

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