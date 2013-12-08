using System;
using LanExchange.OS.Windows.Utils;
using LanExchange.SDK.OS;

namespace LanExchange.OS.Windows
{
    internal class User32Service : IUser32Service
    {
        public void SelectAllItems(IntPtr handle)
        {
            User32Utils.SelectAllItems(handle);
        }
    }
}