using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LanExchange.Utils
{
    public class ListViewUtils
    {
        public static int GetCountChecked(ListView LV)
        {
            int Result = 0;
            foreach (ListViewItem LVI in LV.Items)
                if (LVI.Checked)
                    Result++;
            return Result;
        }

        public static void SetChecked(ListView LV, string name, bool Checked)
        {
            foreach (ListViewItem LVI in LV.Items)
                if (LVI.Text.Equals(name))
                {
                    LVI.Checked = Checked;
                    break;
                }
        }

        public static IList<string> GetCheckedList(ListView LV)
        {
            IList<string> Result = new List<string>();
            foreach (int index in LV.CheckedIndices)
                Result.Add(LV.Items[index].Text);
            return Result;
        }

        public static void SetCheckedList(ListView LV, IList<string> SaveSelected)
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
