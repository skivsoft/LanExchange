using System;
using System.Collections.Generic;
using System.Diagnostics;
using LanExchange.Intf;
using LanExchange.SDK;

namespace LanExchange.Misc.Impl
{
    public class PanelItemFactoryManagerImpl : IPanelItemFactoryManager
    {
        private readonly IDictionary<Type, PanelItemFactoryBase> m_Types;
        private readonly List<PanelItemBase> m_DefaultRoots;

        public PanelItemFactoryManagerImpl()
        {
            m_Types = new Dictionary<Type, PanelItemFactoryBase>();
            m_DefaultRoots = new List<PanelItemBase>();
        }

        public void RegisterFactory<TPanelItem>(PanelItemFactoryBase factory) where TPanelItem : PanelItemBase
        {
            if (factory == null)
                throw new ArgumentNullException("factory");
            m_Types.Add(typeof(TPanelItem), factory);
            factory.RegisterColumns(App.Resolve<IPanelColumnManager>());
        }

        public IDictionary<Type, PanelItemFactoryBase> Types
        {
            get { return m_Types; }
        }

        public PanelItemBase CreateDefaultRoot(string typeName)
        {
            foreach(var pair in m_Types)
                if (pair.Key.Name.Equals(typeName))
                    return pair.Value.CreateDefaultRoot();
            return null;
        }

        public void CreateDefaultRoots()
        {
            m_DefaultRoots.Clear();
            foreach(var pair in m_Types)
            try
            {
                var root = pair.Value.CreateDefaultRoot();
                if (root != null)
                    m_DefaultRoots.Add(root);
                m_DefaultRoots.Sort();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
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

        public bool IsEmpty
        {
            get { return m_Types.Count == 0; }
        }
    }
}
