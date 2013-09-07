using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LanExchange.Intf
{
    public interface IMainView : IView
    {
        void ApplicationExit();
        void ShowStatusText(string format, params object[] args);
        void SetToolTip(object control, string tipText);
        void ClearToolTip(object control);
    }
}
