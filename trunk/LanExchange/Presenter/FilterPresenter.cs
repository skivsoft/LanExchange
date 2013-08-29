using System;
using LanExchange.Core;
using LanExchange.Model;
using LanExchange.UI;

namespace LanExchange.Presenter
{
    public class FilterPresenter : PresenterBase<IFilterView>, IFilterPresenter
    {
        private IFilterModel m_Model;
        private string m_Filter;

        public string FilterText
        {
            get { return m_Filter; }
            set
            {
                m_Filter = value;
                if (m_Model != null)
                {
                    m_Model.FilterText = value;
                    m_Model.ApplyFilter();
                    View.UpdateFromModel(m_Model);
                    View.DoFilterCountChanged();
                }
                View.IsVisible = IsFiltered;
            }
        }

        public bool IsFiltered
        {
            get
            {
                return !String.IsNullOrEmpty(m_Filter);
            }
        }

        public IFilterModel GetModel()
        {
            return m_Model;
        }

        public void SetModel(IFilterModel value)
        {
            m_Model = null;
            if (value != null)
            {
                View.SetFilterText(value.FilterText);
                m_Model = value;
                m_Model.ApplyFilter();
                View.UpdateFromModel(m_Model);
                View.DoFilterCountChanged();
            }
        }
    }
}