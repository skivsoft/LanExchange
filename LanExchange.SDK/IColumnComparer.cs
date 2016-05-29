using System.Collections.Generic;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.SDK
{
    public interface IColumnComparer : IComparer<PanelItemBase>
    {
        int ColumnIndex { get; set; }
        PanelSortOrder SortOrder { get; set; }
    }
}