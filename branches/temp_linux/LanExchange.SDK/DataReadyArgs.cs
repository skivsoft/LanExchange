using System;

namespace LanExchange.SDK
{
    public class DataReadyArgs : EventArgs
    {
        public DataReadyArgs(PanelItemBase item)
        {
            Item = item;
        }

        public PanelItemBase Item { get; private set; }
    }
}