using System.ComponentModel;
using System.Windows.Forms;
using LanExchange.SDK;
using LanExchange.Properties;

namespace LanExchange.UI
{
    public partial class InputBoxForm : Form, IInputBoxView
    {
        public static InputBoxForm CreateAskForm(string caption, string prompt, string defText)
        {
            var result = new InputBoxForm();
            result.Text = string.IsNullOrEmpty(caption) ? Application.ProductName : caption;
            result.lblInputLabel.Text = prompt + ':';
            result.Value = defText;
            result.ActiveControl = result.txtInputText;
            return result;
        }

        public InputBoxForm()
        {
            InitializeComponent();
        }

        public string Value
        {
            get { return txtInputText.Text.Trim(); }
            set { txtInputText.Text = value.Trim(); }
        }

        public void SetError(string errorText)
        {
            errorProvider.SetError(txtInputText, errorText);
        }

        public event CancelEventHandler InputValidating
        {
            add { txtInputText.Validating += value; }
            remove { txtInputText.Validating -= value; }
        }

        /// <summary>
        /// Validating method for InputBoxForm's control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void ValidatingEmpty(object sender, CancelEventArgs e)
        {
            var control = sender as Control;
            if (control == null) return;
            var form = control.Parent as InputBoxForm;
            if (form != null && form.Value.Length == 0)
            {
                form.SetError(Resources.InputBoxPresenter_NotEmpty);
                e.Cancel = true;
            }
        }

        private void InputBoxForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                e.Handled = true;
            }
        }
    }
}