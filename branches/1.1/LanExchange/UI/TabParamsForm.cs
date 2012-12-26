using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LanExchange.View;
using LanExchange.Model;

namespace LanExchange.UI
{
    public partial class TabParamsForm : Form, ITabParamsView, ISubscriber
    {
        public TabParamsForm()
        {
            InitializeComponent();
            UpdateControls();
            // subscribe this object to domain list (subject = null)
            ServerListSubscription.Instance.SubscribeToSubject(this, null);
        }


        public void DataChanged(ISubscription sender, DataChangedEventArgs e)
        {
            var DomainList = (IList<ServerInfo>)e.Data;
            if (DomainList == null)
                return;
            var Saved = ListView_GetCheckedList(lvDomains);
            try
            {
                lvDomains.Items.Clear();
                foreach (var domain in DomainList)
                    lvDomains.Items.Add(domain.Name);
            }
            finally
            {
                ListView_SetCheckedList(lvDomains, Saved);
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
            foreach (var item in lvDomains.Items)
                if (((ListViewItem)item).Text.Equals(name))
                {
                    ((ListViewItem)item).Checked = Checked;
                    break;
                }
        }

        public bool ScanMode
        {
            get
            {
                if (rbSelected.Checked)
                    return true;
                else
                    return false;
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


        public static List<string> ListView_GetCheckedList(ListView LV)
        {
            List<string> Result = new List<string>();
            if (LV.FocusedItem != null)
                Result.Add(LV.FocusedItem.Text);
            else
                Result.Add("");
            foreach (int index in LV.CheckedIndices)
                Result.Add(LV.Items[index].Text);
            return Result;
        }

        public static void ListView_SetCheckedList(ListView LV, List<string> SaveSelected)
        {
            LV.FocusedItem = null;
            int Count = LV.VirtualMode ? LV.VirtualListSize : LV.Items.Count;
            if (Count > 0)
            {
                for (int i = 0; i < SaveSelected.Count; i++)
                {
                    int index = -1;
                    for (int j = 0; j < LV.Items.Count; j++)
                        if (SaveSelected[i].CompareTo(LV.Items[j].Text) == 0)
                        {
                            index = j;
                            break;
                        }
                    if (index == -1) continue;
                    if (i == 0)
                    {
                        LV.FocusedItem = LV.Items[index];
                        LV.SelectedIndices.Add(index);
                        LV.EnsureVisible(index);
                    }
                    else
                        LV.Items[index].Checked = true;
                }
            }
        }

    }
}
