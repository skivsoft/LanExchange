using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.Model;

namespace LanExchange.View
{
    /// <summary>
    /// Interface for Panel component.
    /// </summary>
    public interface IPanelView
    {
        IEnumerable<int> SelectedIndices { get; }
        void SelectItem(int Index);
        PanelItem GetItem(int Index);
    }
}
