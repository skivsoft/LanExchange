using System;
using LanExchange.Presentation.Interfaces.Menu;
using System.Collections.Generic;

namespace LanExchange.Presentation.Interfaces
{
    public interface IMainView : IWindow, IViewContainer, ISupportRightToLeft
    {
        IntPtr Handle { get; }
        string TrayText { get; set; }
        bool TrayVisible { get; set; }
        string ShowWindowKey { get; set; }
        bool MenuVisible { get; set; }

        void ShowStatusText(string format, params object[] args);
        void SetToolTip(object control, string tipText);

        void SetupMenuLanguages();
        void SetupMenuTags();
        void InitializeMenu(IMenuElement menu);
    }
}
