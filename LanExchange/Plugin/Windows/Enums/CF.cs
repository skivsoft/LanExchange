namespace LanExchange.Plugin.Windows.Enums
{
    // Particular clipboard format of interest. 
    // There are three types of formats recognized by OLE
    public enum CF
    {
        BITMAP = 2,
        DIB = 8,
        DIF = 5,
        DSPBITMAP = 130,
        DSPENHMETAFILE = 0x8e,
        DSPMETAFILEPICT = 0x83,
        DSPTEXT = 0x81,
        ENHMETAFILE = 14,
        GDIOBJFIRST = 0x300,
        GDIOBJLAST = 0x3ff,
        HDROP = 15,
        LOCALE = 0x10,
        MAX = 0x11,
        METAFILEPICT = 3,
        OEMTEXT = 7,
        OWNERDISPLAY = 0x80,
        PALETTE = 9,
        PENDATA = 10,
        PRIVATEFIRST = 0x200,
        PRIVATELAST = 0x2ff,
        RIFF = 11,
        SYLK = 4,
        TEXT = 1,
        TIFF = 6,
        UNICODETEXT = 13,
        WAVE = 12
    }
}