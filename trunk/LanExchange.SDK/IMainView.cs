using System;

namespace LanExchange.SDK
{
    public interface IMainView : IWindow
    {
        bool ShowInfoPanel { get; set; }
        int NumInfoLines { get; set; }
        string TrayText { get; set; }
        bool TrayVisible { get; set; }
        IInfoView Info { get; }
        string ShowWindowKey { get; set; }

        void ApplicationExit();
        void ShowStatusText(string format, params object[] args);
        void SetToolTip(object control, string tipText);
        void ClearInfoPanel();

        void SetRunMinimized(bool minimized);
        void SetupMenuLanguages();

        void SetupPages();
        object SafeInvoke(Delegate method, params object[] args);
    }
}
