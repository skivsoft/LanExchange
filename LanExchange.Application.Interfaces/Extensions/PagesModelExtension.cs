using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Interfaces.Extensions
{
    public static class PagesModelExtension
    {
        public static PagesDto ToDto(this IPagesModel pages)
        {
            var items = new PanelDto[pages.Count];
            for (int index = 0; index < pages.Count; index++)
                items[index] = pages.GetAt(index).ToDto();
            var dto = new PagesDto
            {
                SelectedIndex = pages.SelectedIndex,
                Items = items
            };

           return dto;
        }
    }
}
