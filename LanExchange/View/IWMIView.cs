using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace LanExchange.View
{
    public interface IWMIView
    {
        ListView LV { get; }
        ContextMenuStrip MENU { get; }
        void ShowStat(int ClassCount, int PropCount, int MethodCount);
        void ClearClasses();
        void AddClass(string ClassName);
    }
}
