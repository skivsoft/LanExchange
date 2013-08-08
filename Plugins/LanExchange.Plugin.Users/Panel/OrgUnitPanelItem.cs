using System;
using LanExchange.SDK;

namespace LanExchange.Plugin.Users.Panel
{
    internal class OrgUnitPanelItem : PanelItemBase
    {
        private string m_Name;

        public static void RegisterColumns(IPanelColumnManager columnManager)
        {
            columnManager.RegisterColumn(typeof(OrgUnitPanelItem), new PanelColumnHeader("Organization unit"));
        }

        public OrgUnitPanelItem(PanelItemBase parent)
            : base(parent)
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