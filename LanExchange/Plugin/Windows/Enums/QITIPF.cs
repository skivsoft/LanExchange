using System;

namespace LanExchange.Plugin.Windows.Enums
{
    // Directing the handling of the item from which you're retrieving the info tip text
    [Flags]
    public enum QITIPF
    {
        DEFAULT = 0x00000,
        USENAME = 0x00001,
        LINKNOTARGET = 0x00002,
        LINKUSETARGET = 0x00004,
        USESLOWTIP = 0x00008
    }
}