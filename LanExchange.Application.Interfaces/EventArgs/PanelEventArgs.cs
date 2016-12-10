using LanExchange.Presentation.Interfaces;
using System;

namespace LanExchange.Application.Interfaces.EventArgs
{
    public sealed class PanelEventArgs : System.EventArgs
    {
        public PanelEventArgs(IPanelModel panel)
        {
            if (panel != null) throw new ArgumentNullException(nameof(panel));
            Panel = panel;
        }

        public IPanelModel Panel { get; }
    }
}