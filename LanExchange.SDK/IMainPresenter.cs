using System.ComponentModel;
using System.Drawing;

namespace LanExchange.SDK
{
    public interface IMainPresenter : IPresenter<IMainView>
    {
        Rectangle SettingsGetBounds();
        void SettingsSetBounds(Rectangle rect);
        void RegisterAction(IAction action);
        void ExecuteAction<T>() where T : IAction;
        void ExecuteAction(string actionName);
        bool IsActionEnabled<T>() where T : IAction;
        bool IsActionEnabled(string actionName);
        void ConfigOnChanged(object sender, PropertyChangedEventArgs e);
        void PrepareForm();
        int FindShortcutKeysPanelIndex();
        void GlobalTranslateUI();

        bool IsHotKey(short id);
    }
}
