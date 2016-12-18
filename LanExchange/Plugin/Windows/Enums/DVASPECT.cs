using System;

namespace LanExchange.Plugin.Windows.Enums
{
    // Specifies the desired data or view aspect of the object when drawing or getting data
    [Flags]
    public enum DVASPECT
    {
        CONTENT = 1,
        DOCPRINT = 8,
        ICON = 4,
        THUMBNAIL = 2
    }
}