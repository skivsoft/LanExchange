using System;

namespace LanExchange.Plugin.Windows.Enums
{
    // Indicate whether the method should try to return a name in the pwcsName member of the STATSTG structure
    [Flags]
    public enum STATFLAG
    {
        DEFAULT = 0,
        NONAME = 1,
        NOOPEN = 2
    }
}