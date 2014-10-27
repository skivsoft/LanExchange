using LanExchange.SDK.Presenter.Model;
using LanExchange.SDK.Presenter.View;

namespace LanExchange.SDK.Presenter
{
    public interface ICommanderPresenter : IPresenter<ICommanderView>
    {
        void CycleActiveModel();

        void SetModels(ICommanderPanelModel leftModel, ICommanderPanelModel rightModel);

        ICommanderPanelModel ActiveModel { get; }

        ICommanderPanelModel PassiveModel { get; }
    }
}
