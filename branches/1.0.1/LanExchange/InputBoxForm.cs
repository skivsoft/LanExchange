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
        public InputBoxForm()
        {
            InitializeComponent();
        }

        public void Prepare(string prompt, string errorMsgOnEmpty, string defText)
        {
            lblInputLabel.Text = prompt + ':';
            //ErrorMsgOnEmpty = errorMsgOnEmpty;
            txtInputText.Text = defText;
            ActiveControl = txtInputText;
            errorProvider.SetError(txtInputText, "");
        }
    }
}
