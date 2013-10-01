using System;
using System.Diagnostics;
using System.Threading;

namespace LanExchange.Misc
{
    public class DefferedAction : IDisposable
    {
        private readonly int m_Delay;
        private readonly TimerCallback m_Callback;
        private Timer m_Timer;


        public DefferedAction(TimerCallback callback, int delay)
        {
            m_Delay = delay;
            m_Callback = callback;
        }

        private void InternalCallback(object state)
        {
            if (m_Callback != null)
                try
                {
                    m_Callback(state);
                }
                catch(Exception ex)
                {
                    Debug.Print(ex.Message);
                }
        }

        private void KillTimer()
        {
            if (m_Timer != null)
            {
                m_Timer.Change(Timeout.Infinite, Timeout.Infinite);
                m_Timer.Dispose();
                m_Timer = null;
            }
        }

        public void Reset()
        {
            KillTimer();
            m_Timer = new Timer(InternalCallback, this, m_Delay, Timeout.Infinite);
        }

        public void Dispose()
        {
            KillTimer();
        }
    }
}