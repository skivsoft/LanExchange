using System;

namespace LanExchange.Plugin.FileSystem
{
    internal class ColumnDrive : IComparable<ColumnDrive>, IComparable
    {
        private readonly string m_Name;
        private readonly string m_DisplayName;

        public ColumnDrive(string name, string displayName)
        {
            m_Name = name;
            m_DisplayName = displayName;
        }

        public string Name
        {
            get { return m_Name; }
        }

        public int CompareTo(object obj)
        {
            return CompareTo(obj as ColumnDrive);
        }

        public int CompareTo(ColumnDrive other)
        {
            return String.Compare(Name, other.Name, StringComparison.CurrentCulture);
        }

        public override string ToString()
        {
            return m_DisplayName;
        }
    }
}