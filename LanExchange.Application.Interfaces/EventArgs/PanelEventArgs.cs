using LanExchange.Presentation.Interfaces;
using System;

namespace LanExchange.Application.Interfaces.EventArgs
{
    public sealed class PanelEventArgs : System.EventArgs
    {
        public PanelEventArgs(IPanelModel panel)
        {
            Panel = panel ?? throw new ArgumentNullException(nameof(panel));
        }

        public IPanelModel Panel { get; }
    }
}