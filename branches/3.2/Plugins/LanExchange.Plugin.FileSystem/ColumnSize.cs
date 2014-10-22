using System;
using System.ComponentModel;
using System.Globalization;

namespace LanExchange.Plugin.FileSystem
{
    public class ColumnSize : IComparable<ColumnSize>, IComparable
    {
        public static ColumnSize Zero = new ColumnSize(0, true);

        private readonly long m_Value;
        private readonly bool m_IsDirectory;

        public ColumnSize(long value, bool isDirectory)
        {
            m_Value = value;
            m_IsDirectory = isDirectory;
        }

        public long Value
        {
            get { return m_Value; }
        }

        public bool IsDirectory
        {
            get { return m_IsDirectory; }
        }

        public int CompareTo(object obj)
        {
            return CompareTo(obj as ColumnSize);
        }

        public int CompareTo(ColumnSize other)
        {
            if (IsDirectory)
            {
                if (!other.IsDirectory)
                    return -1;
                return 0;
            }
            if (other.IsDirectory) return 1;
            if (Value < other.Value) return -1;
            if (Value > other.Value) return 1;
            return 0;
        }

        [Localizable(false)]
        public override string ToString()
        {
            if (m_IsDirectory && m_Value == 0)
                return string.Empty;

            switch (PluginFileSystem.SizeFormat)
            {
                case ColumnSizeFormat.Byte:
                    return FormatSize(m_Value, long.MaxValue, null);
                case ColumnSizeFormat.Kilobyte:
                    return FormatSize(m_Value, 1000, new[] {string.Empty, "KB", "MB", "GB", "TB", "PB", "EB"});
                case ColumnSizeFormat.Kibibyte:
                    return FormatSize(m_Value, 1024, new[] {string.Empty, "KiB", "MiB", "GiB", "TiB", "PiB", "EiB"});
                default:
                    return string.Empty;
            }
        }

        [Localizable(false)]
        private static string FormatSize(long size, double divide, string[] names)
        {
            if (size < divide)
                return size.ToString("N0", CultureInfo.CurrentCulture);

            double value = size;
            var count = names.Length;
            var index = 0;
            while (index < count-1 && value > divide)
            {
                value /= divide;
                index++;
            }
            size = (long) Math.Round(value);
            return size.ToString("N0", CultureInfo.CurrentCulture) + " " + names[index];
        }
    }
}