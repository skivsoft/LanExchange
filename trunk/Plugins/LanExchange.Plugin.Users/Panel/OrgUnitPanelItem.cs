using System;
using LanExchange.SDK;

namespace LanExchange.Plugin.Users.Panel
{
    internal class OrgUnitPanelItem : PanelItemBase
    {
        public const string ID = "{3D00882A-C21F-4920-A8CE-B1F220D4E539}";
        
        private string m_Name;

        public OrgUnitPanelItem(PanelItemBase parent) : base(parent)
        {
        }

        public OrgUnitPanelItem(PanelItemBase parent, string name) : base(parent)
        {
            m_Name = name;
        }

        public override string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        public override int CountColumns
        {
            get { return 1; }
        }

        public override PanelColumnHeader CreateColumnHeader(int index)
        {
            var result = new PanelColumnHeader();
            result.Visible = true;
            switch (index)
            {
                case 0:
                    result.Text = "Organization unit";
                    break;
            }
            return result;
        }

        public override IComparable this[int index]
        {
            get
            {
                if (index == 0)
                    return m_Name;
                return String.Empty;
            }
        }

        public override string ImageName
        {
            get
            {
                if (m_Name == s_DoubleDot)
                    return PanelImageNames.DoubleDot;
                return PanelImageNames.ShareNormal;
            }
        }
    }
}