using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.View;

namespace LanExchange.Presenter
{
    public class PanelPresenter
    {
        private readonly IPanelView m_View;

        public PanelPresenter(IPanelView view)
        {
            m_View = view;
        }
    }
}
