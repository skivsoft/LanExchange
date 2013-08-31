using System;
using LanExchange.SDK;

namespace LanExchange.Intf
{
    public interface ILazyThreadPool : IDisposable
    {
        event EventHandler<DataReadyArgs> DataReady;
        event EventHandler NumThreadsChanged;

        long NumThreads { get; }
        IComparable AsyncGetData(PanelColumnHeader column, PanelItemBase panelItem);
    }
}