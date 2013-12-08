using System;

namespace LanExchange.SDK.UI
{
    public interface IMainView : IView
    {
        bool ShowInfoPanel { get; set; }
        int NumInfoLines { get; set; }
        string Text { get; set; }
        string TrayText { get; set; }
        bool TrayVisible { get; set; }
        void ApplicationExit();
        void ShowStatusText(string format, params object[] args);
        void SetToolTip(object control, string tipText);
        void ClearInfoPanel();
        void Invoke(Delegate method, object sender);

        void SetRunMinimized(bool minimized);
        void SetupMenuLanguages();

        void SetBounds(int left, int top, int width, int height);

        void SetupPages();
    }
}
