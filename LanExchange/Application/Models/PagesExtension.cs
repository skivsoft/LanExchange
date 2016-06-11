using System.Linq;
using LanExchange.Application.Interfaces;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Models
{
    internal static class PagesExtension
    {
        public static PagesDto ToDto(this IPagesModel pages)
        {
            var items = new PanelDto[pages.Count];
            for (int index = 0; index < pages.Count; index++)
                items[index] = ToDto(pages.GetItem(index));
            var dto = new PagesDto
            {
                SelectedIndex = pages.SelectedIndex,
                Items = items
            };
            return dto;
        }

        public static PanelDto ToDto(this IPanelModel panel)
        {
            var dto = new PanelDto
            {
                DataType = panel.DataType,
                View = panel.CurrentView,
                Filter = panel.FilterText,
                Path = panel.CurrentPath.ToArray(),
                Focused = panel.FocusedItem
            };
            return dto;
        }
    }
}
