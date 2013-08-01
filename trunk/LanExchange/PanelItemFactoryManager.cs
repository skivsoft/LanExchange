using System;
using System.Collections.Generic;
using LanExchange.SDK;

namespace LanExchange
{
    public class PanelItemFactoryManager : IPanelItemFactoryManager
    {
        private readonly Dictionary<Type, PanelItemBaseFactory> m_Types;

        public PanelItemFactoryManager()
        {
            m_Types = new Dictionary<Type, PanelItemBaseFactory>();
        }

        public void RegisterPanelItemFactory(Type type, PanelItemBaseFactory factory)
        {
            if (PanelItemBaseFactoryValidator.ValidateFactory(factory))
                m_Types.Add(type, factory);
        }

        public IDictionary<Type, PanelItemBaseFactory> Types
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

        public bool Exists(Type type)
        {
            PanelItemBaseFactory factory;
            return m_Types.TryGetValue(type, out factory);
        }
    }
}
