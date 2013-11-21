using System;
using System.Collections.Generic;

namespace LanExchange.SDK
{
    public interface IPanelColumnManager
    {
        void RegisterColumn(string typeName, PanelColumnHeader header);
        IList<PanelColumnHeader> GetColumns(string typeName);
        IEnumerable<PanelColumnHeader> EnumAllColumns();
        int MaxColumns { get; }

        bool ReorderColumns(string typeName, int oldIndex, int newIndex);
    }

    public delegate IComparable LazyCallback(PanelItemBase item);
}
