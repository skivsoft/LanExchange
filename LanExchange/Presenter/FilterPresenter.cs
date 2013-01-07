using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.View;

namespace LanExchange.Presenter
{
    public class FilterPresenter
    {
        private readonly IFilterView m_View;

        public FilterPresenter(IFilterView view)
        {
            m_View = view;
        }

        public string FilterText { get; set; }
    }
}
