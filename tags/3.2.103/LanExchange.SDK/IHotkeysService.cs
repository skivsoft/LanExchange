using System;

namespace LanExchange.SDK
{
    public interface IHotkeysService : IDisposable
    {
        short HotkeyId { get; }
        string ShowWindowKey { get; }
        bool RegisterShowWindowKey(IntPtr handle);
    }
}
