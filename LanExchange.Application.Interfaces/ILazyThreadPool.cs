using System;
using LanExchange.Application.Interfaces.EventArgs;

namespace LanExchange.Application.Interfaces
{
    public interface ILazyThreadPool : IDisposable
    {
        event EventHandler<DataReadyArgs> DataReady;
        event EventHandler NumThreadsChanged;

        long NumThreads { get; }
        IComparable AsyncGetData(PanelColumnHeader column, PanelItemBase panelItem);
    }
}