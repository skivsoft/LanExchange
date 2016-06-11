using System.ComponentModel;
using System.Drawing;

namespace LanExchange.Presentation.Interfaces
{
    public interface IMainPresenter : IPresenter<IMainView>
    {
        Rectangle SettingsGetBounds();
        void SettingsSetBounds(Rectangle rect);
        void GlobalTranslateUI();

        bool IsHotKey(short id);
        void DoPagesReRead();
        void DoPagesCloseTab();
        void DoAbout();
        void DoPagesCloseOther();
        void DoToggleVisible();
        void DoExit();
        void OpenHomeLink();
        void OpenLocalizationLink();
        void OpenBugTrackerWebLink();
        void DoChangeView(PanelViewMode viewMode);
    }
}
