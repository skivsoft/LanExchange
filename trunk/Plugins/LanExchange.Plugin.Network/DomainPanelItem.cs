using System;
using LanExchange.Plugin.Network.Properties;
using LanExchange.SDK;

namespace LanExchange.Plugin.Network
{
    public class DomainPanelItem : PanelItemBase
    {
        private readonly ServerInfo m_SI;

        public static void RegisterColumns(IPanelColumnManager columnManager)
        {
            columnManager.RegisterColumn(typeof(DomainPanelItem), new PanelColumnHeader(Resources.DomainName));
        }

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

        public override string Name
        {
            get { return m_SI.Name; }
            set { m_SI.Name = value; }
        }

        public override int CountColumns
        {
            get { return 1; }
        }

        public override IComparable GetValue(int index)
        {
            return m_SI.Name;
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
