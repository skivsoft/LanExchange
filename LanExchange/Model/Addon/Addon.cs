using System.Collections.Generic;
using System.Xml.Serialization;

namespace LanExchange.Model.Addon
{
    [XmlType("LanExchangeAddon")]
    public class Addon
    {
        private readonly List<AddonProgram> m_Programs;
        private readonly List<AddonItemTypeRef> m_PanelItemTypes;

        public Addon()
        {
            m_PanelItemTypes = new List<AddonItemTypeRef>();
            m_Programs = new List<AddonProgram>();
        }

        public List<AddonProgram> Programs
        {
            get { return m_Programs; }
        }

        public List<AddonItemTypeRef> PanelItemTypes
        {
            get { return m_PanelItemTypes; }
        }
    }
}
