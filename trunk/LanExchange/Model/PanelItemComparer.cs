using System.Collections.Generic;

namespace LanExchange.Model
{
    public class PanelItemComparer : IComparer<PanelItem>
    {
        public int Compare(PanelItem item1, PanelItem item2)
        {
            return item1.CompareTo(item2);
        }
    }
}
