using System;

namespace LanExchange.SDK
{
    public sealed class DataReadyArgs : EventArgs
    {
        public DataReadyArgs(PanelItemBase item)
        {
            Item = item;
        }

        public PanelItemBase Item { get; private set; }
    }
}