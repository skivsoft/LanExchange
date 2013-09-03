namespace LanExchange.Intf
{
    public interface IAboutModel : IModel
    {
        string WebSite { get; }

        string Twitter { get; }

        string Email { get; }

        string Title { get; }

        string VersionShort { get; }

        string VersionFull { get; }

        string Description { get; }

        string Product { get; }

        string Copyright { get; }

        string Company { get; }
    }
}
