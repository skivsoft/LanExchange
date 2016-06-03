using LanExchange.Application.Interfaces;

namespace LanExchange.Interfaces.Services
{
    public interface IPagesPersistenceService
    {
        void LoadSettings(out IPagesModel pages);
        void SaveSettings(IPagesModel pages);
    }
}
