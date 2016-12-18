using System;

namespace LanExchange.Plugin.Windows.Enums
{
    // Indicate the type of locking requested for the specified range of bytes
    [Flags]
    public enum LOCKTYPE
    {
        WRITE = 1,
        EXCLUSIVE = 2,
        ONLYONCE = 4
    }
}