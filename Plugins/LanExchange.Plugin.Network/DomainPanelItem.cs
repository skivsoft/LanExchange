using System;
using System.Xml.Serialization;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Plugin.Network
{
    [Serializable]
    public sealed class DomainPanelItem : PanelItemBase
    {
        private readonly ServerInfo serverInfo;

        public DomainPanelItem()
        {
            serverInfo = new ServerInfo();
        }

        public DomainPanelItem(PanelItemBase parent, ServerInfo si) : base(parent)
        {
            serverInfo = si;
        }

        public DomainPanelItem(PanelItemBase parent, string domain) : base(parent)
        {
            serverInfo = new ServerInfo {Name = domain, Comment = string.Empty};
        }

        [XmlAttribute]
        public override string Name
        {
            get { return serverInfo.Name; }
            set { serverInfo.Name = value; }
        }

        public override string FullName
        {
            get { return string.Empty; }
        }

        public override string ImageName
        {
            get { return PanelImageNames.DOMAIN; }
        }

        public override object Clone()
        {
            var result = new DomainPanelItem(Parent, serverInfo);
            result.Name = Name;
            return result;
        }

        public override string ImageLegendText
        {
            get { return string.Empty; }
        }
    }
}
