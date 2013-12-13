using System;
using LanExchange.SDK;
using System.Drawing;

namespace LanExchange.OS.Linux
{
    internal class User32Service : IUser32Service
    {
        public void SelectAllItems(IntPtr handle)
        {
			// do nothing
        }

		public IntPtr GetHeaderControl(IntPtr listViewHandle)
		{
			return IntPtr.Zero;
		}

		public int GetScrollPosition (IntPtr listViewHandle, bool horizontalBar)
		{
			return 0;
		}

		public int GetColumnUnderPoint (IntPtr handle, Point pt)
		{
			return -1;
		}

		public void SetColumnImage (IntPtr listViewHandle, int columnIndex, int order, int imageIndex)
		{
			// do nothing
		}
	}
}