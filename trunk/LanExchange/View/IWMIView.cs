using System.Windows.Forms;

namespace LanExchange.View
{
    public interface IWMIView
    {
        ListView LV { get; }
        ContextMenuStrip MENU { get; }
        void ShowStat(int classCount, int propCount, int methodCount);
        void ClearClasses();
        void AddClass(string className);
    }
}
