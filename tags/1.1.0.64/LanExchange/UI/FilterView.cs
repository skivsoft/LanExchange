using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using LanExchange.View;
using LanExchange.Presenter;
using LanExchange.Properties;

namespace LanExchange.UI
{
    public partial class FilterView : UserControl, IFilterView
    {
        private readonly FilterPresenter m_Presenter;

        public event EventHandler FilterCountChanged;

        public FilterView()
        {
            InitializeComponent();
            m_Presenter = new FilterPresenter(this);
            Visible = false;
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
                if (!e.Control && e.KeyCode == Keys.Up) SendKeys.Send("{UP}");
                if (e.KeyCode == Keys.Down) SendKeys.Send("{DOWN}");
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

        public void SendKeysCorrect(string keys)
        {
            const string chars = "+^%~{}()[]";
            string NewKeys = "";
            foreach (Char Ch in keys)
            {
                if (chars.Contains(Ch.ToString(CultureInfo.InvariantCulture)))
                    NewKeys += String.Format("{{{0}}}", Ch);
                else
                    NewKeys = Ch.ToString(CultureInfo.InvariantCulture);
            }
            SendKeys.Send(NewKeys);
        }

        public void SetFilterText(string value)
        {
            eFilter.Text = value;
        }

        public string GetFilterText()
        {
            return eFilter.Text;
        }

        public void SetIsFound(bool value)
        {
            eFilter.BackColor = value ? Color.White : Color.FromArgb(255, 102, 102); // Firefox Color
        }

        public void FocusMe()
        {
            if (Parent is ContainerControl)
                (Parent as ContainerControl).ActiveControl = this;
            ActiveControl = eFilter;
            eFilter.Focus();
        }

        public void DoFilterCountChanged()
        {
            if (FilterCountChanged != null)
                FilterCountChanged(this, new EventArgs());
        }
    }
}
