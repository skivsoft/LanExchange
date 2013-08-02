using System;
using System.Windows.Forms;
using LanExchange.SDK;

namespace LanExchange.UI
{
    public partial class InputBoxForm : Form, IInputBoxView
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
                errorProvider.SetError(txtInputText, "Input text must not be empty.");
                DialogResult = DialogResult.None;
            } else
                DialogResult = DialogResult.OK;
        }
   }
}
