using System;
using System.Windows.Forms;

namespace LanExchange.UI
{
    public partial class InputBoxForm : Form
    {
        bool m_AllowEmpty = true;

        //public static string ErrorMsgOnEmpty { get; set; }

        public InputBoxForm()
        {
            InitializeComponent();
        }

        public void Prepare(string prompt, string defText, bool bAllowEmpty)
        {
            m_AllowEmpty = bAllowEmpty;
            lblInputLabel.Text = prompt + ':';
            //ErrorMsgOnEmpty = errorMsgOnEmpty;
            txtInputText.Text = defText;
            ActiveControl = txtInputText;
            errorProvider.SetError(txtInputText, "");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!m_AllowEmpty && String.IsNullOrEmpty(txtInputText.Text.Trim()))
            {
                errorProvider.SetError(txtInputText, "Строка не должна быть пустой.");
                DialogResult = DialogResult.None;
            } else
                DialogResult = DialogResult.OK;
        }

        public static string Ask(string caption, string prompt, string defText, bool allowEmpty)
        {
            using (var inputBox = new InputBoxForm())
            {
                if (!String.IsNullOrEmpty(caption))
                    inputBox.Text = caption;
                else
                    inputBox.Text = Application.ProductName;
                inputBox.Prepare(prompt, defText, allowEmpty);
                DialogResult res = inputBox.ShowDialog();
                if (res != DialogResult.OK)
                    return null;
                return inputBox.txtInputText.Text.Trim();
            }
        }

   }
}
