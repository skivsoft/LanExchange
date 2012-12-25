using System;
using System.Collections.Generic;
using System.Text;

namespace LanExchange.View
{
    public interface ITabControlView
    {
        string Name { get; set; }
        int SelectedIndex { get; set; }
        int TabPagesCount { get; }
    }
}
