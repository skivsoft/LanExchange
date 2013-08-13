using System;
using System.Collections.Generic;

namespace LanExchange.SDK
{
    public interface IPanelColumnManager
    {
        void RegisterColumn(Type type, PanelColumnHeader header);
        IList<PanelColumnHeader> GetColumns(Type type);
        IEnumerable<PanelColumnHeader> EnumAllColumns();
        int MaxColumns { get; }

        bool ReorderColumns(Type type, int oldIndex, int newIndex);
    }
}
