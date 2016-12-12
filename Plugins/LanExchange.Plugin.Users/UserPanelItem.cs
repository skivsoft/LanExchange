using System;
using System.ComponentModel;
using System.Xml.Serialization;
using LanExchange.Plugin.Users.Properties;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Plugin.Users
{
    public sealed class UserPanelItem : PanelItemBase
    {
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
                //return "CN=" + base.FullName;
                return AdsPath;
            }
        }

        //[XmlAttribute]
        public string AdsPath { get; set; }

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
                return IsAccountDisabled ? PanelImageNames.USER + PanelImageNames.RedPostfix : PanelImageNames.USER;
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