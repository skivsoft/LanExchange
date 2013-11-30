using System;
using System.Collections.Generic;
using LanExchange.SDK;

namespace LanExchange.Misc.Impl
{
    class PanelColumnManagerImpl : IPanelColumnManager
    {
        private readonly IDictionary<string, IList<PanelColumnHeader>> m_Types;
        private int m_MaxColumns;

        public PanelColumnManagerImpl()
        {
            m_Types = new Dictionary<string, IList<PanelColumnHeader>>();
        }

        public void RegisterColumn<TPanelItem>(PanelColumnHeader header) where TPanelItem : PanelItemBase
        {
            IList<PanelColumnHeader> found;
            var typeName = typeof (TPanelItem).Name;
            if (!m_Types.TryGetValue(typeName, out found))
            {
                found = new List<PanelColumnHeader>();
                m_Types.Add(typeName, found);
            }
            header.Index = found.Count;
            found.Add(header);
            if (found.Count > m_MaxColumns)
                m_MaxColumns = found.Count;
        }

        public void UnregisterColumns(string typeName)
        {
            m_Types.Remove(typeName);
        }

        public IList<PanelColumnHeader> GetColumns(Type type)
        {
            return GetColumns(type.Name);
        }

        public IList<PanelColumnHeader> GetColumns(string typeName)
        {
            IList<PanelColumnHeader> result;
            m_Types.TryGetValue(typeName, out result);
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
        public bool ReorderColumns(string typeName, int oldIndex, int newIndex)
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