using System;

namespace LanExchange.Plugin.Windows.Enums
{
    // Specify the conditions for performing the commit operation in the IStorage::Commit and IStream::Commit methods
    [Flags]
    public enum STGC
    {
        DEFAULT = 0,
        OVERWRITE = 1,
        ONLYIFCURRENT = 2,
        DANGEROUSLYCOMMITMERELYTODISKCACHE = 4,
        CONSOLIDATE = 8
    }
}