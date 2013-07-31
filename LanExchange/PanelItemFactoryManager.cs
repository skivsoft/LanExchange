using System;
using System.Collections.Generic;
using LanExchange.SDK;

namespace LanExchange
{
    public class PanelItemFactoryManager : IPanelItemFactoryManager
    {
        private readonly Dictionary<Guid, PanelItemBaseFactory> m_Types;

        public PanelItemFactoryManager()
        {
            m_Types = new Dictionary<Guid, PanelItemBaseFactory>();
        }

        public void RegisterPanelItemFactory(Guid guid, PanelItemBaseFactory factory)
        {
            m_Types.Add(guid, factory);
        }

        public IDictionary<Guid, PanelItemBaseFactory> Types
        {
            get { return m_Types; }
        }

        public IEnumerable<PanelItemBase> CreateDefaultRoots()
        {
            foreach(var pair in m_Types)
            {
                var root = pair.Value.CreateDefaultRoot();
                if (root != null)
                    yield return root;
            }
        }
    }
}
