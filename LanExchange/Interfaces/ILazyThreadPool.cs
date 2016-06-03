using System;
using LanExchange.Application.Interfaces;

namespace LanExchange.Interfaces
{
    public interface ILazyThreadPool : IDisposable
    {
        event EventHandler<DataReadyArgs> DataReady;
        event EventHandler NumThreadsChanged;

        long NumThreads { get; }
        IComparable AsyncGetData(PanelColumnHeader column, PanelItemBase panelItem);
    }
}