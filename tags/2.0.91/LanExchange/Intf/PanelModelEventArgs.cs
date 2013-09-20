using System;

namespace LanExchange.Intf
{
    public class PanelModelEventArgs : EventArgs
    {
        private readonly IPanelModel m_Info;

        public PanelModelEventArgs(IPanelModel info)
        {
            m_Info = info;
        }

        public IPanelModel Info
        {
            get { return m_Info; }
        }
    }
}