using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace LanExchange.Network
{
    public class BackgroundWorkerList
    {
        private IDictionary<object, BackgroundWorker> m_Workers = null;

        public BackgroundWorkerList()
        {
            m_Workers = new Dictionary<object, BackgroundWorker>();
        }

        public int Count
        {
            get
            {
                return m_Workers.Count;
            }
        }

        public bool Exists(object argument)
        {
            bool Found;
            lock (m_Workers)
            {
                Found = m_Workers.ContainsKey(argument);
            }
            return Found;
        }
        
        public void Add(object argument, BackgroundWorker worker)
        {
            lock (m_Workers)
            {
                m_Workers.Add(argument, worker);
            }
        }

        public void ClearNotBusy()
        {
            lock (m_Workers)
            {
                bool Found;
                do
                {
                    Found = false;
                    foreach(var Pair in m_Workers)
                        if (!Pair.Value.IsBusy)
                        {
                            Found = true;
                            BackgroundWorker worker = Pair.Value;
                            m_Workers.Remove(Pair.Key);
                            worker = null;
                            break;
                        }
                } while (Found);
            }
        }

        public void RunWorkerAsync()
        {
            lock (m_Workers)
            {
                foreach (var Pair in m_Workers)
                    if (!Pair.Value.IsBusy)
                        Pair.Value.RunWorkerAsync(Pair.Key);
            }
        }

        public void CancelAsync()
        {
            lock (m_Workers)
            {
                foreach (var Pair in m_Workers)
                    if (Pair.Value.IsBusy)
                        Pair.Value.CancelAsync();
            }
        }

        public int BusyCount
        {
            get
            {
                int Count = 0;
                lock (m_Workers)
                {
                    foreach (var Pair in m_Workers)
                        if (Pair.Value.IsBusy)
                            Count++;
                }
                return Count;
            }
        }

        public bool IsBusy
        {
            get
            {
                bool bFound = false;
                lock (m_Workers)
                {
                    foreach (var Pair in m_Workers)
                        if (Pair.Value.IsBusy)
                        {
                            bFound = true;
                            break;
                        }
                }
                return bFound;
            }
        }

    }
}
