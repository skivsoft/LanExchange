using System.Collections.Generic;
using System.Windows.Forms;

namespace LanExchange.UI
{
    public static class ListViewUtils
    {
        public static int GetCountChecked(ListView lv)
        {
            int result = 0;
            foreach (ListViewItem lvi in lv.Items)
                if (lvi.Checked)
                    result++;
            return result;
        }

        public static void SetChecked(ListView lv, string name, bool Checked)
        {
            foreach (ListViewItem lvi in lv.Items)
                if (lvi.Text.Equals(name))
                {
                    lvi.Checked = Checked;
                    break;
                }
        }

        public static IList<string> GetCheckedList(ListView lv)
        {
            IList<string> result = new List<string>();
            foreach (int index in lv.CheckedIndices)
                result.Add(lv.Items[index].Text);
            return result;
        }

        //public static void SetCheckedList(ListView LV, IList<string> SaveSelected)
        //{
        //    LV.FocusedItem = null;
        //    int Count = LV.VirtualMode ? LV.VirtualListSize : LV.Items.Count;
        //    if (Count > 0)
        //    {
        //        for (int i = 0; i < SaveSelected.Count; i++)
        //        {
        //            int index = -1;
        //            for (int j = 0; j < LV.Items.Count; j++)
        //                if (SaveSelected[i].CompareTo(LV.Items[j].Text) == 0)
        //                {
        //                    index = j;
        //                    break;
        //                }
        //            if (index == -1) continue;
        //            LV.Items[index].Checked = true;
        //            if (i == 0)
        //            {
        //                LV.FocusedItem = LV.Items[index];
        //                LV.SelectedIndices.Add(index);
        //            }
        //        }
        //    }
        //}
    }
}
