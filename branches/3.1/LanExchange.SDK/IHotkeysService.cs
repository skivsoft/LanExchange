using System;

namespace LanExchange.SDK
{
    public interface IHotkeysService : IDisposable
    {
        short HotkeyID { get; }
        string ShowWindowKey { get; }
        bool RegisterShowWindowKey(IntPtr handle);
    }
}
