using System;
using LanExchange.Sdk;

namespace Plugins.Plugin
{
    internal class OrgUnitPanelItem : PanelItemBase
    {
        private string m_Name;

        public OrgUnitPanelItem(PanelItemBase parent)
            : base(parent)
        {
        }

        public OrgUnitPanelItem(string name)
            : base(null)
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
            get { return 2; }
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

        public override PanelColumnHeader CreateColumnHeader(int index)
        {
            var result = new PanelColumnHeader();
            result.Visible = true;
            switch (index)
            {
                case 0:
                    result.Text = "Организационная единица";
                    break;
            }
            return result;
        }
    }
}