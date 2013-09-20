using System;
using System.Collections.Generic;
using LanExchange.Model;
using LanExchange.SDK;

namespace LanExchange.Misc.Impl
{
    public class PanelItemFactoryManagerImpl : IPanelItemFactoryManager
    {
        private readonly IDictionary<Type, PanelItemFactoryBase> m_Types;
        private readonly IList<PanelItemBase> m_DefaultRoots;

        public PanelItemFactoryManagerImpl()
        {
            m_Types = new Dictionary<Type, PanelItemFactoryBase>();
            m_DefaultRoots = new List<PanelItemBase>();
        }

        public void RegisterPanelItemFactory(Type type, PanelItemFactoryBase factory)
        {
            //if (PanelItemBaseFactoryValidator.ValidateFactory(factory))
                m_Types.Add(type, factory);
        }

        public IDictionary<Type, PanelItemFactoryBase> Types
        {
            get { return m_Types; }
        }

        public void CreateDefaultRoots()
        {
            m_DefaultRoots.Clear();
            foreach(var pair in m_Types)
            {
                var root = pair.Value.CreateDefaultRoot();
                if (root != null)
                    m_DefaultRoots.Add(root);
            }
        }

        public IList<PanelItemBase> DefaultRoots
        {
            get { return m_DefaultRoots; }
        }

        public bool Exists(Type type)
        {
            PanelItemFactoryBase factory;
            return m_Types.TryGetValue(type, out factory);
        }

        public Type[] ToArray()
        {
            var result = new Type[m_Types.Count+2];
            int i = 0;
            foreach (var key in m_Types.Keys)
                result[i++] = key;
            result[m_Types.Count] = typeof (PanelItemRoot);
            result[m_Types.Count + 1] = typeof (PanelItemDoubleDot);
            return result;
        }
    }
}
