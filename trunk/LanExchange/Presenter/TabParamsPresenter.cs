using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.View;

namespace LanExchange.Presenter
{
    public class TabParamsPresenter
    {
        private readonly ITabParamsView m_View;

        public TabParamsPresenter(ITabParamsView view)
        {
            m_View = view;
        }
    }
}
