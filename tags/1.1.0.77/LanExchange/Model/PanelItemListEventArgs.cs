using System;

namespace LanExchange.Model
{
    public class PanelItemListEventArgs : EventArgs
    {
        private readonly PanelItemList m_Info;

        public PanelItemListEventArgs(PanelItemList info)
        {
            m_Info = info;
        }

        public PanelItemList Info
        {
            get { return m_Info; }
        }
    }
}