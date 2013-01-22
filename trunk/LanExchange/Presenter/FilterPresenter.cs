using System;
using LanExchange.View;
using System.Windows.Forms;
using LanExchange.Model;

namespace LanExchange.Presenter
{
    public class FilterPresenter
    {
        private readonly IFilterView m_View;
        private IFilterModel m_Model;
        private string m_Filter;

        public FilterPresenter(IFilterView view)
        {
            m_View = view;
        }

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
                    m_View.UpdateFromModel(m_Model);
                    m_View.DoFilterCountChanged();
                }
                m_View.IsVisible = IsFiltered;
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
                m_View.SetFilterText(value.FilterText);
                m_Model = value;
                m_Model.ApplyFilter();
                m_View.UpdateFromModel(m_Model);
                m_View.DoFilterCountChanged();
            }
        }

        public void LinkedControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetterOrDigit(e.KeyChar) || Char.IsPunctuation(e.KeyChar) || PuntoSwitcher.IsValidChar(e.KeyChar))
            {
                m_View.FocusAndKeyPress(e);
                e.Handled = true;
            }
        }

        public void LinkedControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Down)
            {
                if (m_View.IsVisible)
                    m_View.FocusMe();
                e.Handled = true;
            }
        }

    }
}
