using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace LanExchange.Base
{
    [XmlType("LanExchangeAddon")]
    public class AddOn
    {
        private readonly Collection<AddonProgram> m_Programs;
        private readonly Collection<AddOnItemTypeRef> m_ItemTypes;

        public AddOn()
        {
            m_ItemTypes = new Collection<AddOnItemTypeRef>();
            m_Programs = new Collection<AddonProgram>();
        }

        public Collection<AddonProgram> Programs
        {
            get { return m_Programs; }
        }

        public Collection<AddOnItemTypeRef> ItemTypes
        {
            get { return m_ItemTypes; }
        }
    }
}
