using System.Collections.Generic;
using System.Collections;

namespace LanExchange.Utils.Sorting
{
    public class ColumnComparer : IComparer<IColumnComparable>
    {
        public enum ColumnSortOrder
        {
            Ascending,
            Descending
        }

        public ColumnComparer(int index, ColumnSortOrder sortOrder)
        {
            ColumnIndex = index;
            SortOrder = sortOrder;
        }

        public int Compare(IColumnComparable item1, IColumnComparable item2)
        {
            int result = item1.CompareTo(item2, ColumnIndex);
            return SortOrder == ColumnSortOrder.Ascending ? result : -result;
        }

        public int ColumnIndex;
        public ColumnSortOrder SortOrder;
    }
}
