using LanExchange.SDK.Presenter.Model;

namespace LanExchange.Model
{
    internal class CommanderModel : ICommanderPanelModel
    {
        public object FocusedItem { get; set; }

        public int Count
        {
            get { return 1000; }
        }
    }
}