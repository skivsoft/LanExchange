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
            get
            {
                return m_Filter;
            }
            set
            {
                m_Filter = value;
                m_View.Visible = IsFiltered;
                if (m_Model != null)
                {
                    m_Model.FilterText = value;
                    m_Model.ApplyFilter();
                    m_View.SetIsFound(m_Model.FilterCount > 0);
                    m_View.DoFilterCountChanged();
                }
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
                m_View.FilterText = value.FilterText;
                m_Model = value;
                m_Model.ApplyFilter();
                m_View.SetIsFound(m_Model.FilterCount > 0);
                m_View.DoFilterCountChanged();
            }
        }

        public void LinkedControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetterOrDigit(e.KeyChar) ||
                Char.IsPunctuation(e.KeyChar) ||
                PuntoSwitcher.IsValidChar(e.KeyChar))
            {
                m_View.Visible = true;
                m_View.FocusMe();
                m_View.SendKeysCorrect(e.KeyChar.ToString());
                e.Handled = true;
            }
        }

        public void LinkedControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Down)
            {
                if (m_View.Visible)
                    m_View.FocusMe();
                e.Handled = true;
            }
        }

    }
}
