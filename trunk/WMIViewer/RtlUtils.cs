using System.Windows.Forms;

namespace WMIViewer
{
    public static class RtlUtils
    {
        public static MessageBoxOptions Options
        {
            get
            {
                // TODO RTL need add here for message box
                return MessageBoxOptions.DefaultDesktopOnly;
            }
        }
    }
}
