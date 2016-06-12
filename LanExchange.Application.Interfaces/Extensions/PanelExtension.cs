using System.Linq;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Interfaces.Extensions
{
    public static class PanelExtension
    {
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
