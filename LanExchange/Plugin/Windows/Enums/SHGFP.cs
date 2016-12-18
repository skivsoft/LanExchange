using System;

namespace LanExchange.Plugin.Windows.Enums
{
    // Flags to specify which path is to be returned with SHGetFolderPath
    [Flags]
    public enum SHGFP
    {
        TYPE_CURRENT = 0,
        TYPE_DEFAULT = 1
    }
}