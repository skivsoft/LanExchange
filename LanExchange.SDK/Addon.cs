using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace LanExchange.SDK
{
    [XmlType("LanExchangeAddon")]
    public class AddOn
    {
        private readonly Collection<AddonProgram> m_Programs;
        private readonly Collection<AddOnItemTypeRef> m_PanelItemTypes;

        public AddOn()
        {
            m_PanelItemTypes = new Collection<AddOnItemTypeRef>();
            m_Programs = new Collection<AddonProgram>();
        }

        public Collection<AddonProgram> Programs
        {
            get { return m_Programs; }
        }

        public Collection<AddOnItemTypeRef> PanelItemTypes
        {
            get { return m_PanelItemTypes; }
        }
    }
}
