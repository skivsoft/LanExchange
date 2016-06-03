using System.Collections.Generic;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Interfaces
{
    public interface IColumnComparer : IComparer<PanelItemBase>
    {
        int ColumnIndex { get; set; }
        PanelSortOrder SortOrder { get; set; }
    }
}