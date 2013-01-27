using System.Windows.Forms;

namespace LanExchange.WMI
{
    public interface IWMIView
    {
        ListView LV { get; }
        ContextMenuStrip MENU { get; }
    }
}
