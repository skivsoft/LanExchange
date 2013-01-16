using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LanExchange.Model;
using LanExchange.Utils;
using System.Drawing;

namespace LanExchange.UI
{
    public partial class TabParamsForm : Form, ISubscriber
    {
        public TabParamsForm()
        {
            InitializeComponent();
            // subscribe Form to domain list (subject = "")
            ServerListSubscription.Instance.SubscribeToSubject(this, string.Empty);
            // unsubscribe Form from any subjects when Closed event will be fired
            Closed += (sender, args) => ServerListSubscription.Instance.UnSubscribe(this);
        }

        public void DataChanged(ISubscription sender, string subject)
        {
            // do not update list after unsubscribe
            if (subject == null) return;
            var Saved = ListViewUtils.GetCheckedList(lvDomains);
            string FocusedText = null;
            if (lvDomains.FocusedItem != null)
                FocusedText = lvDomains.FocusedItem.Text;
            lvDomains.Items.Clear();
            int index = 0;
            foreach (ServerInfo SI in sender.GetListBySubject(subject))
            {
                var LVI = new ListViewItem(SI.Name);
                if (Saved.Contains(SI.Name))
                    LVI.Checked = true;
                lvDomains.Items.Add(LVI);
                if (FocusedText != null && String.CompareOrdinal(SI.Name, FocusedText) == 0)
                {
                    lvDomains.FocusedItem = lvDomains.Items[index];
                    lvDomains.FocusedItem.Selected = true;
                }
                index++;
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
                if (value)
                    rbSelected.Checked = true;
                else
                    rbDontScan.Checked = true;
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
