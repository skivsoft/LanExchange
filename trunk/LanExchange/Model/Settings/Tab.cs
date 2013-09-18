using System.Xml.Serialization;
using LanExchange.Intf;
using LanExchange.SDK;

namespace LanExchange.Model.Settings
{
    public class Tab
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public PanelViewMode View { get; set; }

        [XmlAttribute]
        public string Filter { get; set; }

        public ObjectPath<PanelItemBase> Path { get; set; }
        
        public PanelItemBase Focused { get; set; }
        
        public PanelItemBase[] Items;

        public Tab()
        {
            View = PanelViewMode.Details;
            Items = new PanelItemBase[0];
        }
    }
}
