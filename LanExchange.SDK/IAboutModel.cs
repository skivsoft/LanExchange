﻿namespace LanExchange.SDK
{
    public interface IAboutModel
    {
        string HomeLink { get; }

        string LocalizationLink { get; }

        string BugTrackerLink { get; }

        string TwitterLink { get; }

        string EmailLink { get; }

        string Title { get; }

        string VersionShort { get; }

        string VersionFull { get; }

        string Description { get; }

        string Product { get; }

        string Copyright { get; }

        string Company { get; }
    }
}
