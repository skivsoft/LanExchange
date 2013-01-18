using System;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using LanExchange.View;
using LanExchange.Presenter;
using LanExchange.Properties;
using NLog;
using LanExchange.Model;

namespace LanExchange.UI
{
    public partial class FilterView : UserControl, IFilterView
    {
        private readonly FilterPresenter m_Presenter;

        public event EventHandler FilterCountChanged;

        public FilterView()
        {
            InitializeComponent();
            Visible = false;
            m_Presenter = new FilterPresenter(this);
        }

        public FilterPresenter GetPresenter()
        {
            return m_Presenter;
        }

        public Control LinkedControl { get; set; }

        public void eFilter_TextChanged(object sender, EventArgs e)
        {
            var textbox = sender as TextBox;
            if (textbox != null)
                m_Presenter.FilterText = textbox.Text;
        }

        private void eFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                if (Parent == null) return;
                if (!(Parent is ContainerControl)) return;
                if (LinkedControl == null || !LinkedControl.Visible) return;
                (Parent as ContainerControl).ActiveControl = LinkedControl;
                LinkedControl.Focus();
                if (!e.Control && e.KeyCode == Keys.Up) SendKeys.SendWait("{UP}");
                if (e.KeyCode == Keys.Down) SendKeys.SendWait("{DOWN}");
                e.Handled = true;
            }
        }

        private void imgClear_MouseHover(object sender, EventArgs e)
        {
            imgClear.Image = Resources.clear_hover;
        }

        private void imgClear_MouseLeave(object sender, EventArgs e)
        {
            imgClear.Image = Resources.clear_normal;
        }

        private void imgClear_Click(object sender, EventArgs e)
        {
            SetFilterText(String.Empty);
        }

        public void SetFilterText(string value)
        {
            eFilter.Text = value;
        }

        public string GetFilterText()
        {
            return eFilter.Text;
        }

        public void UpdateFromModel(IFilterModel model)
        {
            Color notFoundColor = Color.FromArgb(255, 102, 102); // firefox color
            eFilter.BackColor = String.IsNullOrEmpty(model.FilterText) || model.FilterCount > 0 ? Color.White : notFoundColor;
            DoFilterCountChanged();
        }

        
        public void FocusMe()
        {
            eFilter.Focus();
        }

        public void FocusAndKeyPress(KeyPressEventArgs e)
        {
            if (!Visible)
                Visible = true;
            if (!eFilter.Focused)
            {
                if (Parent is ContainerControl)
                    (Parent as ContainerControl).ActiveControl = this;
                ActiveControl = eFilter;
                eFilter.Focus();
                eFilter.AppendText(e.KeyChar.ToString(CultureInfo.InvariantCulture));
            }
            
        }

        public void DoFilterCountChanged()
        {
            if (FilterCountChanged != null)
                FilterCountChanged(this, EventArgs.Empty);
        }
    }
}
