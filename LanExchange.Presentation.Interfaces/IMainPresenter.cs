using System.Drawing;

namespace LanExchange.Presentation.Interfaces
{
    public interface IMainPresenter : IPresenter<IMainView>
    {
        Rectangle SettingsGetBounds();
        void GlobalTranslateUI();

        bool IsHotKey(short id);
        void DoPagesReRead();
        void DoAbout();
        void DoToggleVisible();
        void DoExit();
        void OpenHomeLink();
        void OpenLocalizationLink();
        void OpenBugTrackerWebLink();
        void DoChangeView(PanelViewMode viewMode);
        void PerformMenuKeyDown();
        bool PerformMenuKeyUp();
        void PerformEscapeKeyDown();
        void PerformEscapeKeyUp();
    }
}
