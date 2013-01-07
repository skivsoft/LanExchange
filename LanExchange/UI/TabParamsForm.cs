using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LanExchange.View;
using LanExchange.Model;
using LanExchange.Utils;
using System.Drawing;

namespace LanExchange.UI
{
    public partial class TabParamsForm : Form, ITabParamsView, ISubscriber
    {
        public TabParamsForm()
        {
            InitializeComponent();
            // subscribe this object to domain list (subject = null)
            ServerListSubscription.Instance.SubscribeToSubject(this, string.Empty);
        }

        private void TabParamsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ServerListSubscription.Instance.UnSubscribe(this);
        }

        public void DataChanged(ISubscription sender, DataChangedEventArgs e)
        {
            var DomainList = (IList<ServerInfo>)e.Data;
            if (DomainList == null)
                return;
            var Saved = ListViewUtils.GetCheckedList(lvDomains);
            try
            {
                lvDomains.Items.Clear();
                foreach (var domain in DomainList)
                    lvDomains.Items.Add(domain.Name);
            }
            finally
            {
                ListViewUtils.SetCheckedList(lvDomains, Saved);
            }
        }

        public bool ScanMode
        {
            get
            {
                return rbSelected.Checked;
            }
            set
            {
                switch (value)
                {
                    case true:
                        rbSelected.Checked = true;
                        break;
                    case false:
                        rbDontScan.Checked = true;
                        break;
                    default:
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
                    value.ForEach(str => ListViewUtils.SetChecked(lvDomains, str, true));
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

        private void lvDomains_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (ListViewUtils.GetCountChecked(lvDomains) == 0)
                rbDontScan.Checked = true;
            else
                rbSelected.Checked = true;
        }

        private void UpdateBackColor()
        {
            if (rbDontScan.Checked)
                lvDomains.BackColor = Color.LightGray;
            else
                lvDomains.BackColor = Color.White;
        }

        private void rbDontScan_CheckedChanged(object sender, EventArgs e)
        {
            UpdateBackColor();
        }
    }
}
