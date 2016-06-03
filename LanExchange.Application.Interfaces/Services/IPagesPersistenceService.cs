namespace LanExchange.Application.Interfaces.Services
{
    public interface IPagesPersistenceService
    {
        void LoadSettings(out IPagesModel pages);
        void SaveSettings(IPagesModel pages);
    }
}
