using System.Collections.Generic;

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