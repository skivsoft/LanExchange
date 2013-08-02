using System;
using System.Windows.Forms;
using LanExchange.SDK;

namespace LanExchange.Plugin.Network.Panel
{
    public class DomainPanelItem : PanelItemBase
    {
        private readonly ServerInfo m_SI;

        public DomainPanelItem()
        {
            m_SI = new ServerInfo();
        }

        public DomainPanelItem(PanelItemBase parent, ServerInfo si) : base(parent)
        {
            m_SI = si;
        }

        public DomainPanelItem(PanelItemBase parent, string domain) : base(parent)
        {
            m_SI = new ServerInfo {Name = domain, Comment = string.Empty};
        }

        public override bool IsCacheable
        {
            get { return true; }
        }

        public override string Name
        {
            get { return m_SI.Name; }
            set { m_SI.Name = value; }
        }

        public override int CountColumns
        {
            get { return 1; }
        }

        public override PanelColumnHeader CreateColumnHeader(int index)
        {
            return new PanelColumnHeader("Workgroup/Domain");
        }

        public override IComparable this[int index]
        {
            get
            {
                if (index == 0)
                    return Name;
                throw new ArgumentOutOfRangeException("index");
            }
        }

        public override string ToString()
        {
            return string.Empty;
        }

        //public int CompareTo(DomainPanelItem other)
        //{
        //    return String.Compare(Name, other.Name, StringComparison.Ordinal);
        //}

        public override string ImageName
        {
            get { return PanelImageNames.Workgroup; }
        }
    }
}
