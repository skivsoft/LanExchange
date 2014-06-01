using System;
using System.Drawing;
using System.Security.AccessControl;

namespace LanExchange.SDK
{
    public interface ICheckAvailabilityWindow : IWindow
    {
        PanelItemBase CurrentItem { get; set; }

        string RunText { get; set; }

        Image RunImage { get; set; }

        Action RunAction { get; set; }

        object CallerControl { get; set; }

        Func<PanelItemBase, bool> AvailabilityChecker { get; set; }

        void StartChecking();

        void WaitAndShow();

        bool DoneAndAvailable { get; }
    }
}
