using System.Xml.Serialization;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Interfaces
{
    [XmlType("LanExchangeTabs")]
    public struct PagesDto
    {
        public static readonly PagesDto Empty = new PagesDto();

        public int SelectedIndex { get; set; }

        public PanelDto[] Items { get; set; }
    }
}