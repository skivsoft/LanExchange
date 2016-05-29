using System.ComponentModel;
using System.Drawing;

namespace LanExchange.Presentation.Interfaces
{
    public interface IMainPresenter : IPresenter<IMainView>
    {
        Rectangle SettingsGetBounds();
        void SettingsSetBounds(Rectangle rect);
        void ConfigOnChanged(object sender, PropertyChangedEventArgs e);
        int FindShortcutKeysPanelIndex();
        void GlobalTranslateUI();

        bool IsHotKey(short id);
    }
}
