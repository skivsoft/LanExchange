using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LanExchange
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void panelView1_LevelDown(object sender, EventArgs e)
        {
            MessageBox.Show(ApplicationFacade.LEVEL_DOWN);
        }
    }
}
