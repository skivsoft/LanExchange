using System.Xml.Serialization;

namespace LanExchange.Application.Models
{
    [XmlType("LanExchangeTabs")]
    public sealed class PagesDto
    {
        public int SelectedIndex { get; set; }

        public PanelDto[] Items { get; set; }
    }
}