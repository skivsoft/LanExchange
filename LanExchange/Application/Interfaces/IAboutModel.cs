namespace LanExchange.Application.Interfaces
{
    public interface IAboutModel
    {
        string HomeLink { get; }

        string LocalizationLink { get; }

        string BugTrackerLink { get; }

        string Title { get; }

        string VersionShort { get; }

        string VersionFull { get; }

        string Description { get; }

        string Product { get; }

        string Copyright { get; }
    }
}
