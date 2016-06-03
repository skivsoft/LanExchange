using System;
using LanExchange.Application.Interfaces.EventArgs;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Interfaces
{
    public interface IPagesModel : IDisposable
    {
        event EventHandler<PanelModelEventArgs> AfterAppendTab;
        event EventHandler<PanelIndexEventArgs> AfterRemove;
        event EventHandler<PanelIndexEventArgs> IndexChanged;

        bool AddTab(IPanelModel model);

        void DelTab(int index);

        int Count { get; }
        int SelectedIndex { get; set; }
        IPanelModel GetItem(int index);
        string GetTabName(int index);

        void SetLoadedModel(IPagesModel model);
    }
}
