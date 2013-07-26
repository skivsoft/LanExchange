using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LanExchange.UI
{
    public partial class EscapeForm : Form
    {
        public EscapeForm()
        {
            InitializeComponent();
        }

        private void EscapeForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
                e.Handled = true;
            }
        }
    }
}
