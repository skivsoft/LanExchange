using System.Drawing;

namespace LanExchange.SDK.UI
{
    public interface IMainPresenter : IPresenter<IMainView>
    {
        Rectangle SettingsGetBounds();
        void SettingsSetBounds(Rectangle rect);
        void RegisterAction(IAction action);
        void ExecuteAction<T>() where T : IAction;
        bool IsActionEnabled<T>() where T : IAction;
        void ConfigOnChanged(object sender, ConfigChangedArgs e);
        void PrepareForm();
    }
}
