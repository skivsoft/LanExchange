using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using LanExchange.Application.Interfaces;
using LanExchange.Interfaces;
using ThreadState = System.Threading.ThreadState;

namespace LanExchange.Misc.Impl
{
    public class LazyThreadPoolImpl : ILazyThreadPool
    {
        private const int NUM_CYCLES_IN_THREAD = 10;

        private readonly LinkedList<PanelItemBase> asyncQueue;
        private readonly List<Thread> threads;
        private long numThreads;

        public event EventHandler<DataReadyArgs> DataReady;
        public event EventHandler NumThreadsChanged;

        public LazyThreadPoolImpl()
        {
            asyncQueue = new LinkedList<PanelItemBase>();
            threads = new List<Thread>();
        }

        public long NumThreads
        {
            get { return Interlocked.Read(ref numThreads); }
        }

        private void DoDataReady(PanelItemBase item)
        {
            if (DataReady != null)
                DataReady(this, new DataReadyArgs(item));
        }

        private void DoNumThreadsChanged()
        {
            if (NumThreadsChanged != null)
                NumThreadsChanged(this, EventArgs.Empty);
        }

        public IComparable AsyncGetData(PanelColumnHeader column, PanelItemBase panelItem)
        {
            IComparable result;
            bool found;
            lock (column)
            {
                found = column.LazyDict.TryGetValue(panelItem, out result);
            }
            if (found)
                return result;
            lock (asyncQueue)
            {
                if (asyncQueue.Contains(panelItem))
                    asyncQueue.Remove(panelItem);
                asyncQueue.AddFirst(panelItem);
            }
            UpdateThreads(column);
            return null;
        }

        private void AsyncEnum(object state)
        {
            var column = state as PanelColumnHeader;
            if (column == null || column.Callback == null)
                return;
            Interlocked.Increment(ref numThreads);
            DoNumThreadsChanged();
            int number = 0;
            while (asyncQueue.Count > 0 && number < NUM_CYCLES_IN_THREAD)
            {
                try
                {
                    PanelItemBase item;
                    lock (asyncQueue)
                    {
                        item = asyncQueue.First.Value;
                        asyncQueue.RemoveFirst();
                    }
                    var result = column.Callback(item);
                    //bool bFound = false;
                    //IComparable found;
                    //lock (column)
                    //{
                    //    bFound = column.Dict.TryGetValue(item, out found);
                    //    if (!bFound)
                    //        column.Dict.Add(item, result);
                    //}
                    //if (!bFound)
                    //    DoDataReady(item);
                    column.LazyDict.Add(item, result);
                    DoDataReady(item);
                }
                catch(Exception ex)
                {
                    Debug.Print(ex.Message);
                }
                ++number;
            }
            Interlocked.Decrement(ref numThreads);
            DoNumThreadsChanged();
        }

        private void UpdateThreads(PanelColumnHeader column)
        {
            if (NumThreads * NUM_CYCLES_IN_THREAD < asyncQueue.Count)
                lock (threads)
                {
                    // remove stopped threads
                    for (int i = threads.Count - 1; i >= 0; i--)
                        if (threads[i].ThreadState == ThreadState.Stopped)
                            threads.RemoveAt(i);
                    // start new threads
                    while (NumThreads * NUM_CYCLES_IN_THREAD < asyncQueue.Count)
                    {
                        var thread = new Thread(AsyncEnum);
                        threads.Add(thread);
                        thread.Start(column);
                    }
                }
        }

        public void Dispose()
        {
            lock (threads)
            {
                if (!disposed)
                {
                    foreach (var t in threads.Where(t => t.ThreadState == ThreadState.Running))
                        t.Abort();
                    disposed = true;
                }
            }
        }

        private bool disposed;
    }
}