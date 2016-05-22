using System.Collections.Generic;

namespace LanExchange.SDK
{
    public interface IPanelColumnManager
    {
        void RegisterColumn<TPanelItem>(PanelColumnHeader header) where TPanelItem : PanelItemBase;
        void UnregisterColumns(string typeName);

        IEnumerable<PanelColumnHeader> GetColumns(string typeName);
        IEnumerable<PanelColumnHeader> EnumAllColumns();

        bool ReorderColumns(string typeName, int oldIndex, int newIndex);
    }
}
