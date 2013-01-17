using System.ComponentModel;
using System.Threading;
using System;

namespace LanExchange.Model
{
    public class BackgroundWorkerEx : BackgroundWorker
    {
        private Thread m_WorkerThread;

        public event EventHandler AfterRunWorkerCompleted;

        protected override void OnDoWork(DoWorkEventArgs e)
        {
            m_WorkerThread = Thread.CurrentThread;
            try
            {
                base.OnDoWork(e);
            }
            catch (ThreadAbortException)
            {
                e.Cancel = true; //We must set Cancel property to true!
                Thread.ResetAbort(); //Prevents ThreadAbortException propagation
            }
        }

        protected override void OnRunWorkerCompleted(RunWorkerCompletedEventArgs e)
        {
            base.OnRunWorkerCompleted(e);
            DoAfterRunWorkerCompleted();
        }

        private void DoAfterRunWorkerCompleted()
        {
            if (AfterRunWorkerCompleted != null)
                AfterRunWorkerCompleted(this, new EventArgs());
        }

        public void Abort()
        {
            if (m_WorkerThread != null)
            {
                m_WorkerThread.Abort();
                m_WorkerThread = null;
            }
        }
    }
}
