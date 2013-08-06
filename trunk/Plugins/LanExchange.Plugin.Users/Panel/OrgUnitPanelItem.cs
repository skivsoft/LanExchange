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

        protected override IComparable GetValue(int index)
        {
            return m_Name;
        }

        public override string ImageName
        {
            get
            {
                return PanelImageNames.ShareNormal;
            }
        }
    }
}