using System;
using LanExchange.SDK;

namespace LanExchange.Plugin.Network.Panel
{
    public class DomainPanelItem : PanelItemBase
    {
        public const string ID = "{6000DD5C-848F-40FA-A48E-E30C986F365A}";

        private readonly ServerInfo m_SI;

        public DomainPanelItem(PanelItemBase parent, ServerInfo si) : base(parent)
        {
            m_SI = si;
        }

        public DomainPanelItem(PanelItemBase parent, string domain) : base(parent)
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
