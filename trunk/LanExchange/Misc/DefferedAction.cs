using System;
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
                m_Callback(state);
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