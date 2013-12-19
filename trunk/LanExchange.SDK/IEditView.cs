using System;
using System.Collections.Generic;
using System.Text;

namespace LanExchange.SDK
{
    public interface IEditView : IWindow
    {
        IEditPresenter Presenter { get; }

        void SetColumns(IList<PanelColumnHeader> columns);
    }
}
