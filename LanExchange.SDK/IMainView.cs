using System;

namespace LanExchange.SDK
{
    public interface IMainView : IWindow
    {
        string TrayText { get; set; }
        bool TrayVisible { get; set; }
        string ShowWindowKey { get; set; }

        void ApplicationExit();
        void ShowStatusText(string format, params object[] args);
        void SetToolTip(object control, string tipText);

        void SetRunMinimized(bool minimized);
        void SetupMenuLanguages();

        void SetupPages();
        object SafeInvoke(Delegate method, params object[] args);
    }
}
