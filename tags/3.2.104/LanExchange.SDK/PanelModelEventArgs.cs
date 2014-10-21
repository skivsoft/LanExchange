using System;

namespace LanExchange.SDK
{
    public sealed class PanelModelEventArgs : EventArgs, IDisposable
    {
        public PanelModelEventArgs(IPanelModel info)
        {
            Info = info;
        }

        public IPanelModel Info { get; private set; }

        public void Dispose()
        {
        }
    }
}