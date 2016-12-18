using System;

namespace LanExchange.Plugin.Windows.Enums
{
    // Specifies a group of flags for controlling the advisory connection
    [Flags]
    public enum ADVF
    {
        CACHE_FORCEBUILTIN = 0x10,
        CACHE_NOHANDLER = 8,
        CACHE_ONSAVE = 0x20,
        DATAONSTOP = 0x40,
        NODATA = 1,
        ONLYONCE = 4,
        PRIMEFIRST = 2
    }
}