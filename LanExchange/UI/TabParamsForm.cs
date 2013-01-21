using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LanExchange.Model;
using LanExchange.Utils;
using System.Drawing;
using LanExchange.Model.Panel;

namespace LanExchange.UI
{
    public partial class TabParamsForm : Form, ISubscriber
    {
        public TabParamsForm()
        {
            InitializeComponent();
            // subscribe Form to ROOT subject (must return domain list)
            PanelSubscription.Instance.SubscribeToSubject(this, ConcreteSubject.Root);
            // unsubscribe Form from any subjects when Closed event will be fired
            Closed += (sender, args) => PanelSubscription.Instance.UnSubscribe(this);
        }

        public void DataChanged(ISubscription sender, ISubject subject)
        {
            // do not update list after unsubscribe
            if (subject == null) return;
            var Saved = ListViewUtils.GetCheckedList(lvDomains);
            string FocusedText = null;
            if (lvDomains.FocusedItem != null)
                FocusedText = lvDomains.FocusedItem.Text;
            lvDomains.Items.Clear();
            int index = 0;
            foreach (var PItem in sender.GetListBySubject(subject))
            {
                var domain = PItem as DomainPanelItem;
                if (domain == null) continue;
                var LVI = new ListViewItem(domain.Name);
                if (Saved.Contains(domain.Name))
                    LVI.Checked = true;
                lvDomains.Items.Add(LVI);
                if (FocusedText != null && String.CompareOrdinal(domain.Name, FocusedText) == 0)
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

        public IList<ISubject> Groups
        {
            get
            {
                var result = new List<ISubject>();
                foreach (ListViewItem item in lvDomains.Items)
                    if (item.Checked)
                        result.Add(new DomainPanelItem(item.Text));
                return result;
            }
            set
            {
                if (value != null)
                    foreach(var PItem in value)
                        ListViewUtils.SetChecked(lvDomains, PItem.Subject, true);
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
