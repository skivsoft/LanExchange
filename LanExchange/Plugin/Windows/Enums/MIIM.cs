using System;

namespace LanExchange.Plugin.Windows.Enums
{
    // Specifies the content of the new menu item
    [Flags]
    public enum MIIM : uint
    {
        BITMAP = 0x80,
        CHECKMARKS = 0x08,
        DATA = 0x20,
        FTYPE = 0x100,
        ID = 0x02,
        STATE = 0x01,
        STRING = 0x40,
        SUBMENU = 0x04,
        TYPE = 0x10
    }
}