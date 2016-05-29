using System;

namespace LanExchange.SDK
{
    public sealed class PanelIndexEventArgs : EventArgs
    {
        public PanelIndexEventArgs(int index)
        {
            Index = index;
        }

        public int Index { get; }
    }
}