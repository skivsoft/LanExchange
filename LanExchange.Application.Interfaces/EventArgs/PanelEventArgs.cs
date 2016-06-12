using LanExchange.Presentation.Interfaces;
using System;
using System.Diagnostics.Contracts;

namespace LanExchange.Application.Interfaces.EventArgs
{
    public sealed class PanelEventArgs : System.EventArgs
    {
        public PanelEventArgs(IPanelModel panel)
        {
            Contract.Requires<ArgumentNullException>(panel != null);
            Panel = panel;
        }

        public IPanelModel Panel { get; }
    }
}