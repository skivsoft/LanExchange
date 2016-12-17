namespace LanExchange.Application.Interfaces
{
    public interface IPagesPersistenceService
    {
        PagesDto LoadPages();

        void SavePages(PagesDto pages);
    }
}
