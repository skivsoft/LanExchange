using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using LanExchange.Sdk;

namespace LanExchange.UI
{
    public partial class TabParamsForm : Form, ITabParamsView
    {
        public event EventHandler OkClicked;

        public TabParamsForm()
        {
            InitializeComponent();
        }

        public bool SelectedChecked
        {
            get { return rbSelected.Checked; }
            set { rbSelected.Checked = value; }
        }

        public bool DontScanChecked 
        {
            get { return rbDontScan.Checked; }
            set { rbDontScan.Checked = value; }
        }

        public IEnumerable<string> Groups
        {
            get
            {
                foreach (ListViewItem item in lvDomains.Items)
                    if (item.Checked)
                        yield return item.Text;
            }
        }

        public int DomainsCount
        {
            get { return lvDomains.Items.Count; }
        }

        public string DomainsFocusedText
        {
            get
            {
                if (lvDomains.FocusedItem == null)
                    return null;
                return lvDomains.FocusedItem.Text;
            }
            set
            {
                if (value == null) return;
                for (int index = 0; index < lvDomains.Items.Count; index++ )
                    if (String.CompareOrdinal(lvDomains.Items[index].Text, value) == 0)
                    {
                        lvDomains.FocusedItem = lvDomains.Items[index];
                        lvDomains.FocusedItem.Selected = true;
                    }
            }
        }

        public IList<string> CheckedList
        {
            get { return ListViewUtils.GetCheckedList(lvDomains); }
        }

        public bool ShowModal()
        {
            return ShowDialog() == DialogResult.OK;
        }

        public void DomainsClear()
        {
            lvDomains.Items.Clear();
        }

        public void DomainsAdd(string value, bool checkedItem)
        {
            var LVI = new ListViewItem(value);
            LVI.Checked = checkedItem;
            lvDomains.Items.Add(LVI);
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (OkClicked != null)
                OkClicked(this, EventArgs.Empty);
        }

    }
}
