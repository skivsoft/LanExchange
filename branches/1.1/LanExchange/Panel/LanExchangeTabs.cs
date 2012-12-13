using System;

namespace LanExchange
{
    public class LanExchangeTabs
    {
        public int SelectedIndex { get; set; }
        public TabSettings[] Items { get; set; }

        public LanExchangeTabs()
        {
            Items = new TabSettings[0];
        }
    }
}
