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

    public class DataReadyArgs : EventArgs
    {
        public DataReadyArgs(PanelItemBase item)
        {
            Item = item;
        }

        public PanelItemBase Item { get; private set; }
    }
}