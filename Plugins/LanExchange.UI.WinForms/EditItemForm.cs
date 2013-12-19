using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LanExchange.SDK;

namespace LanExchange.UI.WinForms
{
    public partial class EditItemForm : Form, IEditItemView
    {
        public EditItemForm()
        {
            InitializeComponent();
        }

        private void EditItemForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
                e.Handled = true;
            }
        }

    }
}
