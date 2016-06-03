using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using LanExchange.Presentation.Interfaces;
using LanExchange.Presentation.WinForms.Properties;

namespace LanExchange.Presentation.WinForms.Controls
{
    internal sealed partial class FilterView : UserControl, IFilterView
    {
        public event EventHandler FilterCountChanged;

        public FilterView()
        {
            InitializeComponent();
            Visible = false;
        }

        public IFilterPresenter Presenter { get; set; }

        public Control LinkedControl { get; set; }

        public void eFilter_TextChanged(object sender, EventArgs e)
        {
            var textbox = sender as TextBox;
            if (textbox != null)
                Presenter.FilterText = textbox.Text;
        }

        [Localizable(false)]
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
                if (e.KeyCode == Keys.Enter) SendKeys.SendWait("{ENTER}");
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
            SetFilterText(string.Empty);
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
            var filterCount = model.FilterCount;
            if (model.HasBackItem)
                filterCount--;
            eFilter.BackColor = string.IsNullOrEmpty(model.FilterText) || filterCount > 0 ? Color.White : notFoundColor;
        }

        
        public void FocusMe()
        {
            eFilter.Focus();
        }

        public bool IsVisible
        {
            get { return Visible; }
            set 
            { 
                Visible = value;
                if (!Visible)
                    if (LinkedControl != null)
                        if (LinkedControl.Parent is ContainerControl)
                            (LinkedControl.Parent as ContainerControl).ActiveControl = LinkedControl;
            }
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
