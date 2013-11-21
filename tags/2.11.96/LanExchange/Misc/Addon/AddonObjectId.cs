using System.Xml.Serialization;

namespace LanExchange.Misc.Addon
{
    public class AddonObjectId
    {
        public AddonObjectId()
        {
            
        }

        public AddonObjectId(string id)
        {
            Id = id;
        }

        [XmlAttribute]
        public string Id { get; set; }
    }
}
