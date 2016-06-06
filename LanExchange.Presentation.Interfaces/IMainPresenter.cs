using System.ComponentModel;
using System.Drawing;

namespace LanExchange.Presentation.Interfaces
{
    public interface IMainPresenter : IPresenter<IMainView>
    {
        Rectangle SettingsGetBounds();
        void SettingsSetBounds(Rectangle rect);
        void ConfigOnChanged(object sender, PropertyChangedEventArgs e);
        void GlobalTranslateUI();

        bool IsHotKey(short id);
        void DoPagesReRead();
        void DoPagesCloseTab();
        void DoAbout();
        void DoPagesCloseOther();
    }
}
