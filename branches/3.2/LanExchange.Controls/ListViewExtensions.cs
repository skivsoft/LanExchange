using System;
using System.Windows.Forms;
using LanExchange.Controls.Implementation;

namespace LanExchange.Controls
{
    /// <summary>
    /// ListView extensions.
    /// </summary>
    public static class ListViewExtensions
    {

        /// <summary>
        /// Select all rows on the given listview
        /// </summary>
        /// <param name="listView">The listview whose items are to be selected</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SelectAllItems(this ListView listView)
        {
            if (listView == null)
                throw new ArgumentNullException("listView");

            NativeMethods.SetItemState(listView.Handle, -1, 2, 2);
        }

        /// <summary>
        /// Set "arrow up" or "arrow down" to ListView's column.
        /// </summary>
        /// <param name="listView"></param>
        /// <param name="columnIndex"></param>
        /// <param name="order"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static void SetColumnImage(this ListView listView, int columnIndex, int order)
        {
            if (listView == null)
                throw new ArgumentNullException("listView");

            NativeMethods.SetColumnImage(listView.Handle, columnIndex, order);
        }

    }
}