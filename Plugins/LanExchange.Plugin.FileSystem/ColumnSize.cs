using System;
using System.Globalization;

namespace LanExchange.Plugin.FileSystem
{
    internal class ColumnSize : IComparable<ColumnSize>, IComparable
    {
        public static ColumnSize Zero = new ColumnSize(0, true, string.Empty);

        private readonly long m_Value;
        private readonly bool m_IsDirectory;
        private readonly string m_Name;

        public ColumnSize(long value, bool isDirectory, string name )
        {
            m_Value = value;
            m_IsDirectory = isDirectory;
            m_Name = name;
        }

        public long Value
        {
            get { return m_Value; }
        }

        public bool IsDirectory
        {
            get { return m_IsDirectory; }
        }

        public string Name
        {
            get { return m_Name; }
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

        public override string ToString()
        {
            return m_IsDirectory && m_Value == 0 ? string.Empty : m_Value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
