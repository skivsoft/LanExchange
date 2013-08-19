using System.Xml.Serialization;

namespace LanExchange.Model.Addon
{
    public class ObjectId
    {
        public ObjectId()
        {
            
        }

        public ObjectId(string id)
        {
            Id = id;
        }

        [XmlAttribute]
        public string Id { get; set; }
    }
}
