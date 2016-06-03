using System;
using LanExchange.Presentation.Interfaces.EventArgs;

namespace LanExchange.Presentation.Interfaces
{
    public interface ILazyThreadPool : IDisposable
    {
        event EventHandler<DataReadyArgs> DataReady;
        event EventHandler NumThreadsChanged;

        long NumThreads { get; }
        IComparable AsyncGetData(PanelColumnHeader column, PanelItemBase panelItem);
    }
}