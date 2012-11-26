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
        }

        private void UpdateControls()
        {
            chDomains.Enabled = rbSelected.Checked;
        }

        private void TabParamsForm_Load(object sender, EventArgs e)
        {
            IList<string> Domains = Utils.GetDomainList();
            chDomains.Items.Clear();
            foreach (string value in Domains)
            {
                chDomains.Items.Add(value, false);
            }
        }

        private void rbAll_CheckedChanged(object sender, EventArgs e)
        {
            UpdateControls();
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
                return Result;
            }
            set
            {
                
            }
        }
    }
}
