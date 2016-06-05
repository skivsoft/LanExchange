using System;
using System.Collections.Generic;
using System.Threading;

namespace LanExchange.Presentation.Interfaces
{
    public interface IAppView : IView
    {
        void SetExceptionHandlers();
        void InitVisualStyles();
        void Run(IWindow mainView);
        void Exit();
        IEnumerable<IWindow> GetOpenWindows();
    }
}