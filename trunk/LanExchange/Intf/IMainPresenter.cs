using System.Drawing;
using LanExchange.SDK;

namespace LanExchange.Intf
{
    public interface IMainPresenter : IPresenter<IMainView>
    {
        Rectangle SettingsGetBounds();
        void SettingsSetBounds(Rectangle rect);
        void RegisterAction(IAction action);
        void ExecuteAction<T>() where T : IAction;
        bool IsActionEnabled<T>() where T : IAction;
    }
}
