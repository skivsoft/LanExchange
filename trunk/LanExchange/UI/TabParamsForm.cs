using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LanExchange.Model;
using LanExchange.Utils;
using System.Drawing;
using LanExchange.Model.Panel;
using LanExchange.View;
using LanExchange.Presenter;

namespace LanExchange.UI
{
    public partial class TabParamsForm : Form, ITabParamsView, ISubscriber
    {
        private readonly IList<string> m_Groups;
        private readonly TabParamsPresenter m_Presenter;

        public TabParamsForm()
        {
            InitializeComponent();
            m_Presenter = new TabParamsPresenter(this);
            m_Groups = new List<string>();
        }

        public void PrepareForm()
        {
            // subscribe Form to ROOT subject (must return domain list)
            PanelSubscription.Instance.SubscribeToSubject(this, ConcreteSubject.Root);
            // unsubscribe Form from any subjects when Closed event will be fired
            Closed += (sender, args) => PanelSubscription.Instance.UnSubscribe(this, false);
        }

        public void DataChanged(ISubscription sender, ISubject subject)
        {
            // do not update list after unsubscribe
            if (subject == ConcreteSubject.NotSubscribed) return;
            IList<string> Saved;
            if (lvDomains.Items.Count == 0)
                Saved = m_Groups;
            else
                Saved = ListViewUtils.GetCheckedList(lvDomains);
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

        public IList<ISubject> Groups
        {
            get
            {
                var result = new List<ISubject>();
                if (rbSelected.Checked)
                    foreach (ListViewItem item in lvDomains.Items)
                        if (item.Checked)
                            result.Add(new DomainPanelItem(item.Text));
                return result;
            }
            set
            {
                m_Groups.Clear();
                if (value != null)
                    foreach(var item in value)
                        m_Groups.Add(item.Subject);
                if (m_Groups.Count > 0)
                    rbSelected.Checked = true;
                else
                    rbDontScan.Checked = true;
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
