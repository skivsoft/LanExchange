using System;
using LanExchange.Application.Interfaces.EventArgs;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Interfaces
{
    public interface IPagesModel
    {
        event EventHandler<PanelEventArgs> AppendPanel;
        event EventHandler<PanelIndexEventArgs> RemovePanel;
        event EventHandler<PanelIndexEventArgs> SelectedIndexChanged;

        bool Append(IPanelModel panel);
        void RemoveAt(int index);
        IPanelModel GetAt(int index);
        void Assign(PagesDto dto);

        int Count { get; }
        int SelectedIndex { get; set; }
    }
}
