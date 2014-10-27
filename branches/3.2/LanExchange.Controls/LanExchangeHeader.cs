using System;
using System.Windows.Forms;
using LanExchange.Controls.Implementation;

namespace LanExchange.Controls
{
    /// <summary>
    /// Class used to capture window messages for the header of the list view
    /// control.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Lan")]
    public class LanExchangeHeader : NativeWindow
    {
        /// <summary>
        /// Create a header control for the given ObjectListView.
        /// </summary>
        /// <param name="listView"></param>
        public LanExchangeHeader(LanExchangeListView listView) 
        {
            if (listView == null)
                throw new ArgumentNullException("listView");

            ListView = listView;
			var handle = NativeMethods.GetHeaderControl(listView.Handle);
			if (handle != IntPtr.Zero)
				AssignHandle(handle);
        }

        /// <summary>
        /// Return the index of the column under the current cursor position,
        /// or -1 if the cursor is not over a column
        /// </summary>
        /// <returns>Index of the column under the cursor, or -1</returns>
        public int ColumnIndexUnderCursor 
        {
            get {
                var pt = ListView.PointToClient(Cursor.Position);

				pt.X += NativeMethods.GetScrollPosition(ListView.Handle, true);
				return NativeMethods.GetColumnUnderPoint(Handle, pt);
            }
        }

        /// <summary>
        /// Return the Windows handle behind this control
        /// </summary>
        /// <remarks>
        /// When an ObjectListView is initialized as part of a UserControl, the
        /// GetHeaderControl() method returns 0 until the UserControl is
        /// completely initialized. So the AssignHandle() call in the constructor
        /// doesn't work. So we override the Handle property so value is always
        /// current.
        /// </remarks>
        public new IntPtr Handle
        {
            get
            {
                return NativeMethods.GetHeaderControl(ListView.Handle);
            }
        }

        /// <summary>
        /// Gets or sets the listview that this header belongs to
        /// </summary>
        protected LanExchangeListView ListView { get; set; }
    }
}