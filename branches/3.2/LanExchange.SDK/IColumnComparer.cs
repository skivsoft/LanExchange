using System.Collections.Generic;

namespace LanExchange.SDK
{
    public interface IColumnComparer : IComparer<PanelItemBase>
    {
        int ColumnIndex { get; set; }
        PanelSortOrder SortOrder { get; set; }
    }
}