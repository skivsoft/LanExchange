using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LanExchange.Network;
using LanExchange.View;

namespace LanExchange.UI
{
    public partial class TabParamsForm : Form, ITabParamsView
    {
        public TabParamsForm()
        {
            InitializeComponent();
            UpdateControls();
            UpdateDomainList();
            NetworkScanner.GetInstance().DomainListChanged += new EventHandler(TabParamsForm_DomainListChanged);
        }

        private void TabParamsForm_DomainListChanged(object sender, EventArgs e)
        {
            UpdateDomainList();
        }

        private void UpdateDomainList()
        {
            if (NetworkScanner.GetInstance().DomainList == null)
                return;
            List<string> Saved = ListView_GetCheckedList(lvDomains);
            try
            {
                lvDomains.Items.Clear();
                foreach (var domain in NetworkScanner.GetInstance().DomainList)
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
            foreach(ListViewItem item in lvDomains.Items)
                if (item.Text.Equals(name))
                {
                    item.Checked = Checked;
                    break;
                }
        }

        public LanExchange.PanelItemList.PanelScanMode ScanMode
        {
            get
            {
                if (rbAll.Checked)
                    return LanExchange.PanelItemList.PanelScanMode.All;
                else
                if (rbSelected.Checked)
                    return LanExchange.PanelItemList.PanelScanMode.Selected;
                else
                    return LanExchange.PanelItemList.PanelScanMode.None;
            }
            set
            {
                switch (value)
                {
                    case PanelItemList.PanelScanMode.All:
                        rbAll.Checked = true;
                        break;
                    case PanelItemList.PanelScanMode.Selected:
                        rbSelected.Checked = true;
                        break;
                    case PanelItemList.PanelScanMode.None:
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
