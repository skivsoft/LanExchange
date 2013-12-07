using System;


namespace LanExchange.SDK.OS
{
    public interface IHotkeysService : IDisposable
    {
        short HotkeyID { get; }
        string ShowWindowKey { get; }
        bool RegisterShowWindowKey(IntPtr handle);
    }
}
