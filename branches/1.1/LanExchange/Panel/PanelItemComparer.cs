using System;
using System.Collections.Generic;

namespace LanExchange
{
    public class PanelItemComparer : IComparer<PanelItem>
    {
        public int Compare(PanelItem Item1, PanelItem Item2)
        {
            return Item1.CompareTo(Item2);
        }
    }
}
