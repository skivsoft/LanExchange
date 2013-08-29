using System.Collections;
using System.Xml;
using System.Xml.Serialization;

namespace LanExchange.Model.Settings
{
    public class HashtableSerializable : Hashtable, IXmlSerializable
    {
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void WriteXml(XmlWriter writer)
        {
            // Write the root element 
            //writer.WriteStartElement("dictionary");

            // Foreach object in this (as i am a Hashtable)
            foreach (object key in Keys)
            {
                object value = this[key];
                writer.WriteElementString(key.ToString(), value.ToString());
                // Write item, key and value
                ////writer.WriteStartElement("item");
                //writer.WriteAttributeString("name", key.ToString());
                //writer.WriteAttributeString();
                //writer.WriteElementString("key", key.ToString());
                //writer.WriteElementString("value", value.ToString());
                //// write </item>
                //writer.WriteEndAttribute();
                ////writer.WriteEndElement();
            }

            // write </dictionary>
            //writer.WriteEndElement();
        }

        public void ReadXml(XmlReader reader)
        {
            // Start to use the reader.
            reader.Read();
            // Read the first element i.e. root of this object
            //reader.ReadStartElement("dictionary");
            //reader.

            // Read all elements
            while (reader.NodeType != XmlNodeType.EndElement)
            {
                // parsing the item
                reader.ReadStartElement("item");

                // Parsing the key and value 
                string key = reader.ReadElementString("key");
                string value = reader.ReadElementString("value");

                // end reading the item.
                reader.ReadEndElement();
                reader.MoveToContent();

                // add the item
                Add(key, value);
            }

            // Extremely important to read the node to its end.
            // next call of the reader methods will crash if not called.
            //reader.ReadEndElement();
        }
    }
}
