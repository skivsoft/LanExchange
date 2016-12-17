using System;
using System.Drawing;

namespace LanExchange.Presentation.Interfaces
{
    public interface ICheckAvailabilityWindow : IWindow
    {
        string RunText { get; set; }

        Image RunImage { get; set; }

        Action RunAction { get; set; }

        object CallerControl { get; set; }

        Func<bool> AvailabilityChecker { get; set; }

        Image ObjectImage { get; set; }

        string ObjectText { get; set; }

        Icon Icon { get; set; }

        void StartChecking();

        void WaitAndShow();

        void SetToolTip(string tooltip);

        void InvokeClose();
    }
}
