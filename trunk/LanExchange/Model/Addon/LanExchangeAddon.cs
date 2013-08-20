using System.Collections.Generic;
using System.Xml.Serialization;

namespace LanExchange.Model.Addon
{
    public class LanExchangeAddon
    {
        private readonly List<AddonProgram> m_Programs;
        private readonly List<PanelItemBaseRef> m_PanelItems;

        public LanExchangeAddon()
        {
            m_PanelItems = new List<PanelItemBaseRef>();
            m_Programs = new List<AddonProgram>();
        }

        public List<AddonProgram> Programs
        {
            get { return m_Programs; }
        }

        public List<PanelItemBaseRef> PanelItems
        {
            get { return m_PanelItems; }
        }
    }
}
