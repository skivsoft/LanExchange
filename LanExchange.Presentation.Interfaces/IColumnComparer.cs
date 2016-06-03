using System.Collections.Generic;

namespace LanExchange.Presentation.Interfaces
{
    public interface IColumnComparer : IComparer<PanelItemBase>
    {
        int ColumnIndex { get; set; }
        PanelSortOrder SortOrder { get; set; }
    }
}