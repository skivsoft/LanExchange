using System.Collections.Generic;
using System.Linq;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Implementation
{
    internal sealed class PanelColumnManager : IPanelColumnManager
    {
        private readonly IDictionary<string, IList<PanelColumnHeader>> types;

        public PanelColumnManager()
        {
            types = new Dictionary<string, IList<PanelColumnHeader>>();
        }

        public void RegisterColumn<TPanelItem>(PanelColumnHeader header) where TPanelItem : PanelItemBase
        {
            IList<PanelColumnHeader> found;
            var typeName = typeof (TPanelItem).Name;
            if (!types.TryGetValue(typeName, out found))
            {
                found = new List<PanelColumnHeader>();
                types.Add(typeName, found);
            }
            header.Index = found.Count;
            found.Add(header);
        }

        public void UnregisterColumns(string typeName)
        {
            types.Remove(typeName);
        }

        public IEnumerable<PanelColumnHeader> GetColumns(string typeName)
        {
            IList<PanelColumnHeader> result;
            types.TryGetValue(typeName, out result);
            return result;
        }

        public IEnumerable<PanelColumnHeader> EnumAllColumns()
        {
            return types.SelectMany(pair => pair.Value);
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