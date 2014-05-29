using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using LanExchange.SDK;
using ThreadState = System.Threading.ThreadState;

namespace LanExchange.Misc.Impl
{
    public class LazyThreadPoolImpl : ILazyThreadPool
    {
        private const int NUM_CYCLES_IN_THREAD = 10;

        private readonly LinkedList<PanelItemBase> m_AsyncQueue;
        private readonly List<Thread> m_Threads;
        private long m_NumThreads;

        public event EventHandler<DataReadyArgs> DataReady;
        public event EventHandler NumThreadsChanged;

        public LazyThreadPoolImpl()
        {
            m_AsyncQueue = new LinkedList<PanelItemBase>();
            m_Threads = new List<Thread>();
        }

        public long NumThreads
        {
            get { return Interlocked.Read(ref m_NumThreads); }
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
            lock (m_AsyncQueue)
            {
                if (m_AsyncQueue.Contains(panelItem))
                    m_AsyncQueue.Remove(panelItem);
                m_AsyncQueue.AddFirst(panelItem);
            }
            UpdateThreads(column);
            return null;
        }

        private void AsyncEnum(object state)
        {
            var column = state as PanelColumnHeader;
            if (column == null || column.Callback == null)
                return;
            Interlocked.Increment(ref m_NumThreads);
            DoNumThreadsChanged();
            int number = 0;
            while (m_AsyncQueue.Count > 0 && number < NUM_CYCLES_IN_THREAD)
            {
                try
                {
                    PanelItemBase item;
                    lock (m_AsyncQueue)
                    {
                        item = m_AsyncQueue.First.Value;
                        m_AsyncQueue.RemoveFirst();
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
            Interlocked.Decrement(ref m_NumThreads);
            DoNumThreadsChanged();
        }

        private void UpdateThreads(PanelColumnHeader column)
        {
            if (NumThreads * NUM_CYCLES_IN_THREAD < m_AsyncQueue.Count)
                lock (m_Threads)
                {
                    // remove stopped threads
                    for (int i = m_Threads.Count - 1; i >= 0; i--)
                        if (m_Threads[i].ThreadState == ThreadState.Stopped)
                            m_Threads.RemoveAt(i);
                    // start new threads
                    while (NumThreads * NUM_CYCLES_IN_THREAD < m_AsyncQueue.Count)
                    {
                        var thread = new Thread(AsyncEnum);
                        m_Threads.Add(thread);
                        thread.Start(column);
                    }
                }
        }

        public void Dispose()
        {
            lock (m_Threads)
            {
                if (!m_Disposed)
                {
                    foreach (var t in m_Threads.Where(t => t.ThreadState == ThreadState.Running))
                        t.Abort();
                    m_Disposed = true;
                }
            }
        }

        private bool m_Disposed;
    }
}