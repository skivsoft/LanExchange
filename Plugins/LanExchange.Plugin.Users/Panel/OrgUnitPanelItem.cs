using System;
using LanExchange.SDK;

namespace LanExchange.Plugin.Users.Panel
{
    internal class OrgUnitPanelItem : PanelItemBase
    {
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
            PanelColumnHeader result = null;
            switch (index)
            {
                case 0:
                    result = new PanelColumnHeader("Organization unit");
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
                return string.Empty;
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