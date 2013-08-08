using System;
using System.Collections.Generic;
using LanExchange.SDK;

namespace LanExchange.Model.Impl
{
    class PanelColumnManagerImpl : IPanelColumnManager
    {
        private readonly IDictionary<Type, IList<PanelColumnHeader>> m_Types;

        public PanelColumnManagerImpl()
        {
            m_Types = new Dictionary<Type, IList<PanelColumnHeader>>();
        }

        public void RegisterColumn(Type type, PanelColumnHeader header)
        {
            IList<PanelColumnHeader> found;
            if (!m_Types.TryGetValue(type, out found))
            {
                found = new List<PanelColumnHeader>();
                m_Types.Add(type, found);
            }
            found.Add(header);
        }

        public IEnumerable<PanelColumnHeader> GetColumns(Type type)
        {
            IList<PanelColumnHeader> result;
            m_Types.TryGetValue(type, out result);
            return result;
        }
    }
}
