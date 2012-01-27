using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SkivSoft.LanExchange
{
    public partial class MainForm : Form
    {
        public static TMainApp MainApp;

        public MainForm()
        {
            InitializeComponent();
        }

        public void ActivateFromNewInstance()
        {
            //F.bReActivate = true;
            //F.IsFormVisible = true;
            this.Activate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (MainForm.MainApp == null)
                return;
            //MainForm.MainApp.LogPrint("MainForm_Load");
            listView1.Items.Clear();
            foreach (var Pair in MainApp.plugins)
            {
                ListViewItem Item = new ListViewItem();
                Item.Text = Pair.Value.Name;
                Item.SubItems.Add(Pair.Value.Version);
                Item.SubItems.Add(Pair.Value.Author);
                Item.SubItems.Add(Pair.Value.Description);
                listView1.Items.Add(Item);
            }
        }
    }
}
