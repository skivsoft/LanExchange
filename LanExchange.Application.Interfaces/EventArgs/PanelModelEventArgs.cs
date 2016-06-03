using System;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Interfaces.EventArgs
{
    public sealed class PanelModelEventArgs : System.EventArgs, IDisposable
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