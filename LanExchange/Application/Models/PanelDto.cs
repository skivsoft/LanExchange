using System.Xml.Serialization;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Models
{
    [XmlType("Tab")]
    public sealed class PanelDto
    {
        [XmlAttribute]
        public string DataType { get; set; }

        [XmlAttribute]
        public PanelViewMode View { get; set; }

        [XmlAttribute]
        public string Filter { get; set; }

        public PanelItemBase[] Path { get; set; }

        public PanelItemBase Focused { get; set; }
    }
}