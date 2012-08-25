using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using ViewWinForms.OSLayer;

namespace ViewWinForms.View.Components
{
    public class ListViewEx : ListView
    {
        public ListViewEx()
        {
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true);
        }

        /// <summary>
        /// Select all rows on the given listview
        /// </summary>
        /// <param name="list">The listview whose items are to be selected</param>
        public void SelectAllItems()
        {
            SetItemState(-1, 2, 2);
        }

        /// <summary>
        /// Deselect all rows on the given listview
        /// </summary>
        /// <param name="list">The listview whose items are to be deselected</param>
        public void DeselectAllItems()
        {
            SetItemState(-1, 2, 0);
        }

        /// <summary>
        /// Set the item state on the given item
        /// </summary>
        /// <param name="list">The listview whose item's state is to be changed</param>
        /// <param name="itemIndex">The index of the item to be changed</param>
        /// <param name="mask">Which bits of the value are to be set?</param>
        /// <param name="value">The value to be set</param>
        public void SetItemState(int itemIndex, int mask, int value)
        {
            User32.LVITEM lvItem = new User32.LVITEM();
            lvItem.stateMask = mask;
            lvItem.state = value;
            User32.SendMessageLVItem(this.Handle, User32.LVM_SETITEMSTATE, itemIndex, ref lvItem);
        }
    }
}
