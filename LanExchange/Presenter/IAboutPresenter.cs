using LanExchange.Core;
using LanExchange.UI;

namespace LanExchange.Presenter
{
    public interface IAboutPresenter : IPresenter<IAboutView>
    {
        void LoadFromModel();

        void OpenWebLink();

        void OpenTwitterLink();

        void OpenEmailLink();
    }
}
