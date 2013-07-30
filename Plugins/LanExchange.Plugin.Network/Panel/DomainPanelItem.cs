using System;
using LanExchange.SDK;

namespace LanExchange.Plugin.Network.Panel
{
    public class DomainPanelItem : PanelItemBase
    {
        private readonly ServerInfo m_SI;

        public DomainPanelItem(PanelItemBase parent, ServerInfo si) : base(parent)
        {
            m_SI = si;
        }

        public DomainPanelItem(string domain) : base(null)
        {
            m_SI = new ServerInfo {Name = domain, Comment = String.Empty};
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
