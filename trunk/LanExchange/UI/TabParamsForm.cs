using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LanExchange.Interface;
using LanExchange.Model;
using LanExchange.Utils;
using System.Drawing;
using LanExchange.Presenter;

namespace LanExchange.UI
{
    public partial class TabParamsForm : Form, ITabParamsView
    {
        private readonly TabParamsPresenter m_Presenter;

        public TabParamsForm()
        {
            InitializeComponent();
            m_Presenter = new TabParamsPresenter(this);
        }

        public IPresenter GetPresenter()
        {
            return m_Presenter;
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

        public void PrepareForm()
        {
            // subscribe Form to ROOT subject (must return domain list)
            PanelSubscription.Instance.SubscribeToSubject(m_Presenter, ConcreteSubject.Root);
            // unsubscribe Form from any subjects when Closed event will be fired
            Closed += (sender, args) => PanelSubscription.Instance.UnSubscribe(m_Presenter, false);
        }

        public bool ShowModal()
        {
            return ShowDialog() == DialogResult.OK;
        }

        public void ClearGroups()
        {
            lvDomains.Clear();
        }

        public void AddGroup(string value)
        {
            lvDomains.Items.Add(value);
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
