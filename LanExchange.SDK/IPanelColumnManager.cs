using System;
using System.Collections.Generic;

namespace LanExchange.SDK
{
    public interface IPanelColumnManager
    {
        void RegisterColumn(Type type, PanelColumnHeader header);

        IEnumerable<PanelColumnHeader> GetColumns(Type type);
    }
}
