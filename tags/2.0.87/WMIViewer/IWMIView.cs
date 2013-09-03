using System.Windows.Forms;

namespace WMIViewer
{
    public interface IWMIView
    {
        ListView LV { get; }
        ContextMenuStrip MENU { get; }
    }
}
