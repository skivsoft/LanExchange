using System.Drawing;

namespace LanExchange.Presentation.Interfaces
{
    public interface IMainPresenter : IPresenter<IMainView>
    {
        Rectangle SettingsGetBounds();
        void GlobalTranslateUI();

        bool IsHotKey(short id);
        void DoPagesReRead();
        void DoToggleVisible();
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
