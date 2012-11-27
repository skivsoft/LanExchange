using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace LanExchange
{
    public class ListViewEx : ListView
    {

        public ListViewEx()
        {
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true);
        }

        public void SetObject(PanelItemList ItemList)
        {
            PanelItemList List = GetObject();
            this.Tag = ItemList;
            List = null;
        }

        public PanelItemList GetObject()
        {
            return this.Tag as PanelItemList;
        }

        public List<string> GetCheckedList()
        {
            List<string> Result = new List<string>();
            if (FocusedItem != null)
                Result.Add(FocusedItem.Text);
            else
                Result.Add("");
            foreach (int index in CheckedIndices)
                Result.Add(Items[index].Text);
            return Result;
        }

        public void SetCheckedList(List<string> SaveSelected)
        {
            FocusedItem = null;
            int Count = VirtualMode ? VirtualListSize : Items.Count;
            if (Count > 0)
            {
                for (int i = 0; i < SaveSelected.Count; i++)
                {
                    int index = -1;
                    for (int j = 0; j < Items.Count; j++)
                        if (SaveSelected[i].CompareTo(Items[j].Text) == 0)
                        {
                            index = j;
                            break;
                        }
                    if (index == -1) continue;
                    if (i == 0)
                    {
                        FocusedItem = Items[index];
                        SelectedIndices.Add(index);
                        EnsureVisible(index);
                    }
                    else
                        Items[index].Checked = true;
                }
            }
        }

    }
}
