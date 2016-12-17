using System;
using LanExchange.Application.Interfaces.EventArgs;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Interfaces
{
    public interface IPagesModel
    {
        event EventHandler<PanelEventArgs> PanelAdded;

        event EventHandler<PanelIndexEventArgs> PanelRemoved;

        event EventHandler<PanelIndexEventArgs> SelectedIndexChanged;

        event EventHandler Cleared;

        int Count { get; }

        int SelectedIndex { get; set; }

        bool Add(IPanelModel panel);

        void RemoveAt(int index);

        void Clear();

        IPanelModel GetAt(int index);

        void Assign(PagesDto dto);
    }
}