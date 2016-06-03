using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Interfaces
{
    public delegate void SetTabImageDelegate(IPanelModel model, string imageName);

    public delegate void SetFillerResultDelegate(IPanelModel model, PanelFillerResult fillerResult);
}
