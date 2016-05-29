using LanExchange.Presentation.Interfaces;
using LanExchange.SDK;

namespace LanExchange.Model.Comparers
{
    public sealed class ColumnComparer : IColumnComparer
    {
        public ColumnComparer(int index, PanelSortOrder sortOrder)
        {
            ColumnIndex = index;
            SortOrder = sortOrder;
        }

        public int Compare(PanelItemBase item1, PanelItemBase item2)
        {
            int result = item1.CompareTo(item2, ColumnIndex);
            var isDots = (item1 is PanelItemDoubleDot) || (item2 is PanelItemDoubleDot);
            if (isDots || SortOrder == PanelSortOrder.Ascending)
                return result;
            if (SortOrder == PanelSortOrder.Descending)
                return -result;
            return 0;
        }

        public int ColumnIndex { get; set; }
        public PanelSortOrder SortOrder { get; set; }
    }
}
