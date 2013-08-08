using System;
using System.Collections.Generic;
using LanExchange.SDK;

namespace LanExchange.Model.Impl
{
    public class PanelItemFactoryManagerImpl : IPanelItemFactoryManager
    {
        private readonly IDictionary<Type, PanelItemBaseFactory> m_Types;

        public PanelItemFactoryManagerImpl()
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

        public Type[] ToArray()
        {
            var result = new Type[m_Types.Count];
            int i = 0;
            foreach (var key in m_Types.Keys)
                result[i++] = key;
            return result;
        }
    }
}
