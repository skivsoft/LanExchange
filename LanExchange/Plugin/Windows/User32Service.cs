using System;
using System.Drawing;
using LanExchange.Plugin.Windows.Utils;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Plugin.Windows
{
    internal class User32Service : IUser32Service
    {
        public void SelectAllItems(IntPtr handle)
        {
            User32Utils.SelectAllItems(handle);
        }

        public IntPtr GetHeaderControl(IntPtr listViewHandle)
        {
            return NativeMethods.GetHeaderControl(listViewHandle);
        }

        public int GetScrollPosition(IntPtr listViewHandle, bool horizontalBar)
        {
            return NativeMethods.GetScrollPosition(listViewHandle, horizontalBar);
        }

        public int GetColumnUnderPoint(IntPtr handle, Point pt)
        {
            return NativeMethods.GetColumnUnderPoint(handle, pt);
        }

        public void SetColumnImage(IntPtr listViewHandle, int columnIndex, int order, int imageIndex)
        {
            NativeMethods.SetColumnImage(listViewHandle, columnIndex, order, imageIndex);
        }
    }
}