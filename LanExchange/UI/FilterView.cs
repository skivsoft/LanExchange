using System;
using System.Drawing;
using System.Windows.Forms;
using LanExchange.View;
using LanExchange.Presenter;
using LanExchange.Properties;

namespace LanExchange.UI
{
    public partial class FilterView : UserControl, IFilterView
    {
        private readonly FilterPresenter m_Presenter;
        
        public FilterView()
        {
            InitializeComponent();
            m_Presenter = new FilterPresenter(this);
        }

        public FilterPresenter GetPresenter()
        {
            return m_Presenter;
        }

        public void eFilter_TextChanged(object sender, EventArgs e)
        {
            m_Presenter.FilterText = (sender as TextBox).Text;
        }

        private void eFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                //ActiveControl = LV;
                ActiveControl.Focus();
                if (e.KeyCode == Keys.Up) SendKeys.Send("{UP}");
                if (e.KeyCode == Keys.Down) SendKeys.Send("{DOWN}");
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                //ActiveControl = LV;
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
            FilterText = "";
        }

        public static void SendKeysCorrect(string Keys)
        {
            const string Chars = "+^%~{}()[]";
            string NewKeys = "";
            foreach (Char Ch in Keys)
            {
                if (Chars.Contains(Ch.ToString()))
                    NewKeys += String.Format("{{{0}}}", Ch);
                else
                    NewKeys = Ch.ToString();
            }
            SendKeys.Send(NewKeys);
        }

        public string FilterText
        {
            get
            {
                return eFilter.Text;
            }
            set
            {
                eFilter.Text = value;
            }
        }

        public void InitFilterText(string value)
        {
            eFilter.TextChanged -= eFilter_TextChanged;
            eFilter.Text = value;
            //eFilter.SelectionLength = 0;
            //eFilter.SelectionStart = Text.Length;
            eFilter.TextChanged += eFilter_TextChanged;
        }

        public void SetIsFound(bool value)
        {
            eFilter.BackColor = value ? Color.White : Color.FromArgb(255, 102, 102); // Firefox Color
        }
    }
}
