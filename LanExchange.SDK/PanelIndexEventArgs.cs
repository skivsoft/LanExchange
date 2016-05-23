using System;

namespace LanExchange.SDK
{
    public sealed class PanelIndexEventArgs : EventArgs
    {
        private readonly int index;

        public PanelIndexEventArgs(int index)
        {
            this.index = index;
        }

        public int Index
        {
            get { return index; }
        }
    }
}