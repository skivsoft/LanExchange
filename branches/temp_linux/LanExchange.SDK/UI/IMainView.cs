using System;
using LanExchange.SDK.Model;

namespace LanExchange.SDK.UI
{
    public delegate void UpdateTabImageDelegate(IPanelModel model, int imageIndex);

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
        void Invoke(UpdateTabImageDelegate method, params object[] args);

        void SetRunMinimized(bool minimized);
        void SetupMenuLanguages();

        void SetBounds(int left, int top, int width, int height);

        void SetupPages();
    }
}
