using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LanExchange.SDK
{
    public class ColumnComparer : IComparer<PanelItemBase>
    {
        public ColumnComparer(int index, SortOrder sortOrder)
        {
            ColumnIndex = index;
            SortOrder = sortOrder;
        }

        public int Compare(PanelItemBase item1, PanelItemBase item2)
        {
            int result = item1.CompareTo(item2, ColumnIndex);
            var isDots = (item1 is PanelItemDoubleDot) || (item2 is PanelItemDoubleDot);
            if (isDots || SortOrder == SortOrder.Ascending)
                return result;
            if (SortOrder == SortOrder.Descending)
                return -result;
            return 0;
        }

        public int ColumnIndex;
        public SortOrder SortOrder;
    }
}