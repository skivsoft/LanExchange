using System.ComponentModel;
using System.Drawing;
using LanExchange.SDK.Presentation.Interfaces;

namespace LanExchange.SDK
{
    public interface IMainPresenter : IPresenter<IMainView>
    {
        Rectangle SettingsGetBounds();
        void SettingsSetBounds(Rectangle rect);
        void ConfigOnChanged(object sender, PropertyChangedEventArgs e);
        void PrepareForm();
        int FindShortcutKeysPanelIndex();
        void GlobalTranslateUI();

        bool IsHotKey(short id);
    }
}
