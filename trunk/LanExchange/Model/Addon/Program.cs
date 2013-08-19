using System.Xml.Serialization;

namespace LanExchange.Model.Addon
{
    public class Program : ObjectId
    {
        public Program()
        {
            
        }

        public Program(string id, string fileName)
        {
            Id = id;
            FileName = fileName;
        }

        [XmlAttribute]
        public string FileName { get; set; }
    }
}
