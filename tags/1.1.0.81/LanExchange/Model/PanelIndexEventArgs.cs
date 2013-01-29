using System;

namespace LanExchange.Model
{
    public class PanelIndexEventArgs : EventArgs
    {
        private readonly int m_Index;

        public PanelIndexEventArgs(int index)
        {
            m_Index = index;
        }

        public int Index
        {
            get { return m_Index; }
        }
    }
}