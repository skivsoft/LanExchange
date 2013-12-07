using System;

namespace LanExchange.SDK.UI
{
    public interface IMainView : IView
    {
        void ApplicationExit();
        void ShowStatusText(string format, params object[] args);
        void SetToolTip(object control, string tipText);
        bool ShowInfoPanel { get; set; }
        int NumInfoLines { get; set; }
        void ClearInfoPanel();
        void Invoke(Delegate method, object sender);

        void SetRunMinimized(bool minimized);
        void SetupMenuLanguages();
    }
}
