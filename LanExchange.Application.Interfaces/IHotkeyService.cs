using System;

namespace LanExchange.Application.Interfaces
{
    public interface IHotkeyService : IDisposable
    {
        string ShowWindowKey { get; }

        bool RegisterShowWindowKey(IntPtr handle);

        bool IsHotKey(short id);
    }
}
