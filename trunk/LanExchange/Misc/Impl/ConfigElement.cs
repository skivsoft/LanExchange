using System.Xml.Serialization;
using LanExchange.Intf;

namespace LanExchange.Misc.Impl
{
    [XmlType("Element")]
    public class ConfigElement
    {
        public ConfigElement()
        {
            
        }

        [XmlAttribute]
        public ConfigNames Key { get; set; }

        [XmlAttribute]
        public string Value { get; set; }
    }
}
