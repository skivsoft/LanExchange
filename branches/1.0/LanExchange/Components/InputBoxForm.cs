using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LanExchange
{
    public partial class InputBoxForm : Form
    {
        bool bAllowEmpty = true;

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
   }
}
