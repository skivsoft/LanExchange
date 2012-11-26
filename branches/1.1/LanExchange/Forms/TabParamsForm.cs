using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LanExchange.Network;

namespace LanExchange.Forms
{
    public partial class TabParamsForm : Form
    {
        public TabParamsForm()
        {
            InitializeComponent();
            UpdateControls();
            lvDomains.Items.Clear();
            foreach (string value in NetworkScanner.GetInstance().DomainList)
                lvDomains.Items.Add(value);
        }

        private void UpdateControls()
        {
            lvDomains.Enabled = rbSelected.Checked;
        }

        private void rbAll_CheckedChanged(object sender, EventArgs e)
        {
            UpdateControls();
        }

        private void SetChecked(string name, bool Checked)
        {
            foreach(ListViewItem item in lvDomains.Items)
                if (item.Text.Equals(name))
                {
                    item.Checked = Checked;
                    break;
                }
        }

        public bool AllGroups
        {
            get
            {
                return rbAll.Checked;
            }
            set
            {
                rbAll.Checked = value;
            }
        }

        public List<string> Groups
        {
            get
            {
                List<string> Result = new List<string>();
                if (rbSelected.Checked)
                    foreach (ListViewItem item in lvDomains.Items)
                        if (item.Checked)
                            Result.Add(item.Text);
                return Result;
            }
            set
            {
                if (!rbAll.Checked)
                {
                    if (value.Count == 0)
                        rbDontScan.Checked = true;
                    else
                        rbSelected.Checked = true;
                    foreach (var str in value)
                        SetChecked(str, true);
                }
            }
        }

        private void TabParamsForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                e.Handled = true;
            }
        }
    }
}
