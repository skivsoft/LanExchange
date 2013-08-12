using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.Presenter;
using LanExchange.SDK;
using LanExchange.UI;
using System.Threading;

namespace LanExchange.Model.Impl
{
    class PanelColumnManagerImpl : IPanelColumnManager
    {
        private readonly IDictionary<Type, IList<PanelColumnHeader>> m_Types;
        private int m_MaxColumns;

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
            if (found.Count > m_MaxColumns)
                m_MaxColumns = found.Count;
        }

        public IList<PanelColumnHeader> GetColumns(Type type)
        {
            IList<PanelColumnHeader> result;
            m_Types.TryGetValue(type, out result);
            return result;
        }


        public IEnumerable<PanelColumnHeader> EnumAllColumns()
        {
            foreach (var pair in m_Types)
                foreach (var column in pair.Value)
                    yield return column;
        }

        public int MaxColumns
        {
            get { return m_MaxColumns; }
        }
    }
}
