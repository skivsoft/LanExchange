using System;
using System.ComponentModel;
using System.Text;
using LanExchange.Plugin.Network.NetApi;

namespace LanExchange.Plugin.Network
{
    [Serializable]
    public sealed class ServerInfo
    {
        private string name;
        private string comment;
        private readonly OSVersion version;

        private DateTime utcUpdated;

        /// <summary>
        /// Constructor without params is required for XML-serialization.
        /// </summary>
        // public ServerInfo()

        // {

        // }

        public static ServerInfo FromNetApi32(SERVER_INFO_101 info)
        {
            var result = new ServerInfo();
            result.name = info.name;
            result.version.PlatformId = info.platform_id;
            result.version.Major = info.version_major;
            result.version.Minor = info.version_minor;
            result.version.Type = info.type;
            result.comment = info.comment;
            return result;
        }

        public ServerInfo()
        {
            version = new OSVersion();
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }

        public OSVersion Version
        {
            get { return version; }
        }

        public DateTime UtcUpdated
        {
            get { return utcUpdated; }
            set { utcUpdated = value; }
        }

        public void ResetUtcUpdated()
        {
            utcUpdated = DateTime.UtcNow;
        }

        /// <summary>
        /// This method is virtual for unit-tests only.
        /// </summary>
        /// <returns></returns>
        public TimeSpan GetTopicality()
        {
            return DateTime.UtcNow - UtcUpdated;
        }

        [Localizable(false)]
        public string GetTopicalityText()
        {
            TimeSpan diff = GetTopicality();
            var sb = new StringBuilder();
            bool showSeconds = true;
            if (diff.Days > 0)
            {
                sb.Append(diff.Days);
                sb.Append("d");
                showSeconds = false;
            }
            if (diff.Hours > 0)
            {
                if (sb.Length > 0) sb.Append(" ");
                sb.Append(diff.Hours);
                sb.Append("h");
                showSeconds = false;
            }
            if (diff.Minutes > 0)
            {
                if (sb.Length > 0) sb.Append(" ");
                sb.Append(diff.Minutes);
                sb.Append("m");
            }
            if (showSeconds && diff.Seconds > 0)
            {
                if (sb.Length > 0) sb.Append(" ");
                sb.Append(diff.Seconds);
                sb.Append("s");
            }
            return sb.ToString();
        }
    }
}
