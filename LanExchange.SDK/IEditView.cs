using System.Collections.Generic;

namespace LanExchange.SDK
{
    public interface IEditView : IWindow
    {
        void SetColumns(IList<PanelColumnHeader> columns);

        bool ShowModal();
    }
}
