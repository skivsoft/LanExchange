using System;

namespace LanExchange.Plugin.Windows.Enums
{
    // Flags that specify the drawing style when calling ImageList_GetIcon
    [Flags]
    public enum ILD : uint
    {
        NORMAL = 0x0000,
        TRANSPARENT = 0x0001,
        MASK = 0x0010,
        BLEND25 = 0x0002,
        BLEND50 = 0x0004
    }
}