using System;
using System.ComponentModel;
using System.Globalization;

namespace LanExchange.Plugin.FileSystem
{
    internal class ColumnSize : IComparable<ColumnSize>, IComparable
    {
        public static ColumnSize Zero = new ColumnSize(0, true, string.Empty);

        private readonly long value;
        private readonly bool isDirectory;
        private readonly string name;

        public ColumnSize(long value, bool isDirectory = true, string name = null )
        {
            this.value = value;
            this.isDirectory = isDirectory;
            this.name = name;
        }

        public long Value
        {
            get { return value; }
        }

        public bool IsDirectory
        {
            get { return isDirectory; }
        }

        public string Name
        {
            get { return name; }
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
                return string.Compare(Name, other.Name, StringComparison.Ordinal);
            }
            if (other.IsDirectory) return 1;
            if (Value < other.Value) return -1;
            if (Value > other.Value) return 1;
            return 0;
        }

        [Localizable(false)]
        public override string ToString()
        {
            return isDirectory && value == 0 ? string.Empty : value.ToString("N0", CultureInfo.CurrentCulture);
        }
    }
}
