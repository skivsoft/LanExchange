using System;
using LanExchange.SDK;

namespace LanExchange.Plugin.Users.Panel
{
    internal class UserPanelItem : PanelItemBase
    {
        public const string ID = "{092A4099-309F-4CE8-BCCF-4F950FC06E54}";

        private string m_Name;
        private string m_Description;

        public UserPanelItem(PanelItemBase parent) : base(parent)
        {
        }

        public UserPanelItem(PanelItemBase parent, string name) : base(parent)
        {
            m_Name = name;
        }

        public uint UserAccControl { get; set; }

        public string LockoutTime { get; set; }

        public bool IsAccountDisabled
        {
            get { return (UserAccControl & 2) != 0; }
        }

        public override string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        public string Description
        {
            get { return m_Description; }
            set { m_Description = value; }
        }

        public override int CountColumns
        {
            get { return 2; }
        }

        public override PanelColumnHeader CreateColumnHeader(int index)
        {
            var result = new PanelColumnHeader();
            result.Visible = true;
            switch (index)
            {
                case 0:
                    result.Text = "User";
                    break;
                case 1:
                    result.Text = "Description";
                    break;
            }
            return result;
        }

        public override IComparable this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return m_Name;
                    case 1:
                        return m_Description;
                    default:
                        throw new ArgumentOutOfRangeException("index");
                }
            }
        }

        public override string ImageName
        {
            get
            {
                if (m_Name == s_DoubleDot)
                    return PanelImageNames.DoubleDot;
                return IsAccountDisabled ? PanelImageNames.UserDisabled : PanelImageNames.UserNormal;
            }
        }
    }
}
