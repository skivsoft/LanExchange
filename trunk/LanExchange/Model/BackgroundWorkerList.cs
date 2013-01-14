using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace LanExchange.Model
{
    public class BackgroundWorkerList
    {
        private readonly IDictionary<object, BackgroundWorker> m_Workers;

        public BackgroundWorkerList()
        {
            m_Workers = new Dictionary<object, BackgroundWorker>();
        }

        public int Count
        {
            get { return m_Workers.Count; }
        }

        public bool Exists(object argument)
        {
            lock (m_Workers)
            {
                return m_Workers.ContainsKey(argument);
            }
        }
        
        public void Add(object argument, BackgroundWorker worker)
        {
            lock (m_Workers)
            {
                m_Workers.Add(argument, worker);
            }
        }

        //public void ClearNotBusy()
        //{
        //    lock (m_Workers)
        //    {
        //        bool Found;
        //        do
        //        {
        //            Found = false;
        //            foreach(var Pair in m_Workers)
        //                if (!Pair.Value.IsBusy)
        //                {
        //                    Found = true;
        //                    m_Workers.Remove(Pair.Value);
        //                    break;
        //                }
        //        } while (Found);
        //    }
        //}

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
                int count = 0;
                lock (m_Workers)
                {
                    foreach (var Pair in m_Workers)
                        if (Pair.Value.IsBusy)
                            count++;
                }
                return count;
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
