using System;
using System.Drawing;
using System.Windows.Forms;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Presentation.WinForms.Controls
{
    /// <summary>
    /// Class used to capture window messages for the header of the list view
    /// control.
    /// </summary>
    internal class HeaderControl : NativeWindow
    {
        private readonly IUser32Service userService;

        /// <summary>
        /// Create a header control for the given ObjectListView.
        /// </summary>
        /// <param name="olv"></param>
        public HeaderControl(IUser32Service userService, ListViewer olv)
        {
            if (userService == null) throw new ArgumentNullException(nameof(userService));

            this.userService = userService;

            ListView = olv;
            var handle = userService.GetHeaderControl(olv.Handle);
            if (handle != IntPtr.Zero)
                AssignHandle(handle);

            // AssignHandle(NativeMethods.GetHeaderControl(olv));
        }

        /// <summary>
        /// Return the index of the column under the current cursor position,
        /// or -1 if the cursor is not over a column
        /// </summary>
        /// <returns>Index of the column under the cursor, or -1</returns>
        public int ColumnIndexUnderCursor
        {
            get
            {
                Point pt = ListView.PointToClient(Cursor.Position);

                pt.X += userService.GetScrollPosition(ListView.Handle, true);
                return userService.GetColumnUnderPoint(Handle, pt);

                // pt.X += NativeMethods.GetScrollPosition(ListView, true);
                // return NativeMethods.GetColumnUnderPoint(Handle, pt);
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
            get { return userService.GetHeaderControl(ListView.Handle); }
        }

        /// <summary>
        /// Gets or sets the listview that this header belongs to
        /// </summary>
        protected ListViewer ListView { get; set; }
    }
}
