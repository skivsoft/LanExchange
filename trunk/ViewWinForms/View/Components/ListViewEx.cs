using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using LanExchange.OSLayer;

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
            NativeMethods.SetItemState(this, -1, 2, 2);
        }

        /// <summary>
        /// Deselect all rows on the given listview
        /// </summary>
        /// <param name="list">The listview whose items are to be deselected</param>
        public void DeselectAllItems()
        {
            NativeMethods.SetItemState(this, -1, 2, 0);
        }
    }
}
