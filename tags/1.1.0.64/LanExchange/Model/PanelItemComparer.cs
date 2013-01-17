using System.Collections.Generic;

namespace LanExchange.Model
{
    public class PanelItemComparer : IComparer<AbstractPanelItem>
    {
        public int Compare(AbstractPanelItem item1, AbstractPanelItem item2)
        {
            return item1.CompareTo(item2);
        }
    }
}
