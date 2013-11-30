using System;
using System.Xml.Serialization;
using LanExchange.Plugin.Network.Properties;
using LanExchange.SDK;

namespace LanExchange.Plugin.Network
{
    [Serializable]
    public sealed class DomainPanelItem : PanelItemBase
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

        [XmlAttribute]
        public override string Name
        {
            get { return m_SI.Name; }
            set { m_SI.Name = value; }
        }

        public override string FullName
        {
            get { return string.Empty; }
        }

        public override string ImageName
        {
            get { return PanelImageNames.Workgroup; }
        }

        public override object Clone()
        {
            var result = new DomainPanelItem(Parent, m_SI);
            result.Name = Name;
            return result;
        }

        public override string ImageLegendText
        {
            get { return string.Empty; }
        }
    }
}
