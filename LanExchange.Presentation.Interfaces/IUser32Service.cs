using System;
using System.Drawing;

namespace LanExchange.Presentation.Interfaces
{
    public interface IUser32Service
    {
        void SelectAllItems(IntPtr handle);

		IntPtr GetHeaderControl(IntPtr listViewHandle);
		int GetScrollPosition (IntPtr listViewHandle, bool horizontalBar);
		int GetColumnUnderPoint(IntPtr handle, Point pt);
		void SetColumnImage(IntPtr listViewHandle, int columnIndex, int order, int imageIndex);
    }
}