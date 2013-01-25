using System;
using System.Collections.Generic;
using LanExchange.Strategy;
using System.ComponentModel;
using LanExchange.Utils;

namespace LanExchange.Model
{
    public sealed class BackgroundWorkers : IDisposable
    {
        private static BackgroundWorkers m_Instance;
        private readonly IDictionary<BackgroundContext, BackgroundWorkerEx> m_Workers;

        public event EventHandler CountChanged;

        public BackgroundWorkers()
        {
            m_Workers = new Dictionary<BackgroundContext, BackgroundWorkerEx>();
        }

        public void Dispose()
        {
            StopAllWorkers();
        }

        public static BackgroundWorkers Instance
        {
            get
            {
                if (m_Instance == null)
                    m_Instance = new BackgroundWorkers();
                return m_Instance;
            }
        }

        public int Count
        {
            get { return m_Workers.Count; }
        }

        public IEnumerable<BackgroundContext> EnumContexts()
        {
            foreach (var pair in m_Workers)
                yield return pair.Key;
        }
        //public bool Exists(BackgroundContext context)
        //{
        //    lock (m_Workers)
        //    {
        //        return m_Workers.ContainsKey(context);
        //    }
        //}

        private void DoCountChanged()
        {
            if (CountChanged != null)
                CountChanged(this, EventArgs.Empty);
        }

        private void worker_AfterRunWorkerCompleted(object sender, EventArgs e)
        {
            var worker = sender as BackgroundWorkerEx;
            if (worker == null) return;
            lock (m_Workers)
            {
                foreach(var pair in m_Workers)
                    if (pair.Value.Equals(worker))
                    {
                        m_Workers.Remove(pair.Key);
                        worker.Dispose();
                        DoCountChanged();
                        break;
                    }
            }
            
        }

        internal void Add(BackgroundContext context, BackgroundWorkerEx worker)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (worker == null)
                throw new ArgumentNullException("worker");
            lock (m_Workers)
            {
                LogUtils.Info("Add worker for {0}", context.Strategy);
                worker.AfterRunWorkerCompleted += worker_AfterRunWorkerCompleted;
                m_Workers.Add(context, worker);
                DoCountChanged();
            }
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var context = e.Argument as BackgroundContext;
            if (context == null)
                e.Cancel = true;
            else
                context.ExecuteOperation();
        }

        internal void Add(BackgroundContext backgroundContext)
        {
            var worker = new BackgroundWorkerEx();
            worker.DoWork += worker_DoWork;
            //worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            Add(backgroundContext, worker);
            worker.RunWorkerAsync(backgroundContext);
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

        //public void RunWorkerAsync()
        //{
        //    lock (m_Workers)
        //    {
        //        foreach (var Pair in m_Workers)
        //            if (!Pair.Value.IsBusy)
        //                Pair.Value.RunWorkerAsync(Pair.Key);
        //    }
        //}

        //public void CancelAsync()
        //{
        //    lock (m_Workers)
        //    {
        //        foreach (var Pair in m_Workers)
        //            if (Pair.Value.IsBusy)
        //                Pair.Value.CancelAsync();
        //    }
        //}

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


        public void StopAllWorkers()
        {
            lock (m_Workers)
            {
                foreach (var pair in m_Workers)
                {
                    LogUtils.Info("Stop worker for {0}", pair.Key.Strategy);
                    if (pair.Value.IsBusy)
                        pair.Value.Abort();
                    pair.Value.Dispose();
                }
                m_Workers.Clear();
                DoCountChanged();
            }
        }
    }
}
