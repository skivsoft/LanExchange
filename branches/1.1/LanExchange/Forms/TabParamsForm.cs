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
            UpdateDomainList();
            NetworkScanner.GetInstance().DomainListChanged +=new EventHandler(TabParamsForm_DomainListChanged);
        }

        private void TabParamsForm_DomainListChanged(object sender, EventArgs e)
        {
            UpdateDomainList();
        }

        private void UpdateDomainList()
        {
            if (NetworkScanner.GetInstance().DomainList == null)
                return;
            List<string> Saved = lvDomains.GetCheckedList();
            try
            {
                lvDomains.Items.Clear();
                foreach (var domain in NetworkScanner.GetInstance().DomainList)
                    lvDomains.Items.Add(domain.Name);
            }
            finally
            {
                lvDomains.SetCheckedList(Saved);
            }
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

        public PanelItemListScope Scope
        {
            get
            {
                if (rbAll.Checked)
                    return PanelItemListScope.ALL_GROUPS;
                else
                if (rbSelected.Checked)
                    return PanelItemListScope.SELECTED_GROUPS;
                else
                    return PanelItemListScope.DONT_SCAN;
            }
            set
            {
                switch (value)
                {
                    case PanelItemListScope.ALL_GROUPS:
                        rbAll.Checked = true;
                        break;
                    case PanelItemListScope.SELECTED_GROUPS:
                        rbSelected.Checked = true;
                        break;
                    default:
                        rbDontScan.Checked = true;
                        break;
                }
            }
        }

        public List<string> Groups
        {
            get
            {
                List<string> Result = new List<string>();
                foreach (ListViewItem item in lvDomains.Items)
                    if (item.Checked)
                        Result.Add(item.Text);
                return Result;
            }
            set
            {
                if (value != null)
                    foreach (var str in value)
                        SetChecked(str, true);
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

        private void lvDomains_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
