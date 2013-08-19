using System.Collections.Generic;

namespace LanExchange.Model.Addon
{
    public class LanExchangeAddon
    {
        private readonly List<Program> m_Programs;
        private readonly List<PanelItemBaseRef> m_PanelItems;

        public LanExchangeAddon()
        {
            m_PanelItems = new List<PanelItemBaseRef>();
            m_Programs = new List<Program>();
        }

        public List<Program> Programs
        {
            get { return m_Programs; }
        }

        public List<PanelItemBaseRef> PanelItems
        {
            get { return m_PanelItems; }
        }
    }
}
