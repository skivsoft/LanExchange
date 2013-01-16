using System.Windows.Forms;

namespace LanExchange.WMI
{
    public interface IWMIView
    {
        ListView LV { get; }
        ContextMenuStrip MENU { get; }
        void ShowStat(int classCount, int propCount, int methodCount);
    }
}
