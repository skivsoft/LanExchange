using System;
using System.Windows.Forms;

namespace LanExchange.UI
{
    public partial class InputBoxForm : Form
    {
        bool bAllowEmpty = true;

        public static string ErrorMsgOnEmpty { get; set; }

        public InputBoxForm()
        {
            InitializeComponent();
        }

        public void Prepare(string prompt, string errorMsgOnEmpty, string defText, bool bAllowEmpty)
        {
            this.bAllowEmpty = bAllowEmpty;
            lblInputLabel.Text = prompt + ':';
            //ErrorMsgOnEmpty = errorMsgOnEmpty;
            txtInputText.Text = defText;
            ActiveControl = txtInputText;
            errorProvider.SetError(txtInputText, "");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!bAllowEmpty && String.IsNullOrEmpty(txtInputText.Text.Trim()))
            {
                errorProvider.SetError(txtInputText, "Строка не должна быть пустой.");
                DialogResult = DialogResult.None;
            } else
                DialogResult = DialogResult.OK;
        }

        public static string Ask(string caption, string prompt, string defText, bool allow_empty)
        {
            var inputBox = new InputBoxForm();

            if (!String.IsNullOrEmpty(caption))
                inputBox.Text = caption;
            else
                inputBox.Text = Application.ProductName;

            inputBox.Prepare(prompt, ErrorMsgOnEmpty, defText, allow_empty);

            DialogResult res = inputBox.ShowDialog();
            if (res != DialogResult.OK)
                return null;
            else
                return inputBox.txtInputText.Text.Trim();
        }

   }
}
