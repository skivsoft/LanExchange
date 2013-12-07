using LanExchange.SDK;
using LanExchange.SDK.UI;

namespace LanExchange.Intf
{
    public interface IAboutPresenter : IPresenter<IAboutView>
    {
        void LoadFromModel();

        void OpenHomeLink();

        void OpenTwitterLink();

        void OpenEmailLink();

        void OpenLocalizationLink();

        void OpenBugTrackerWebLink();
    }
}
