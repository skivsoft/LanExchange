using System.Collections.Generic;

namespace LanExchange.SDK
{
    public interface IEditView : IWindow
    {
        IEditPresenter Presenter { get; }

        void SetColumns(IList<PanelColumnHeader> columns);

        bool ShowModal();
    }
}
