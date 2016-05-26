using System;

namespace LanExchange.SDK
{
    public interface IHotkeyService : IDisposable
    {
        string ShowWindowKey { get; }
        bool RegisterShowWindowKey(IntPtr handle);
        bool IsHotKey(short id);
    }
}
