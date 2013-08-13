using System;
using System.Collections.Generic;
using LanExchange.SDK;

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
            header.Index = found.Count;
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

        // TODO Column reorder
        public bool ReorderColumns(Type type, int oldIndex, int newIndex)
        {
            //// lock reorder for 0-column
            //if (oldIndex == 0 || newIndex == 0)
            //    return false;
            //IList<PanelColumnHeader> result;
            //if (m_Types.TryGetValue(type, out result))
            //{
            //    var temp = result[oldIndex];
            //    result[oldIndex] = result[newIndex];
            //    result[newIndex] = temp;
            //    //m_Types.Remove(type);
            //    //m_Types.Add(type, result);
            //    return true;
            //}
            return false;
        }
    }
}
