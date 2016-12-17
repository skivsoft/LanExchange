using System;

namespace LanExchange.Plugin.FileSystem
{
    internal class ColumnDrive : IComparable<ColumnDrive>, IComparable
    {
        private readonly string name;
        private readonly string displayName;

        public ColumnDrive(string name, string displayName)
        {
            this.name = name;
            this.displayName = displayName;
        }

        public string Name
        {
            get { return name; }
        }

        public int CompareTo(object obj)
        {
            return CompareTo(obj as ColumnDrive);
        }

        public int CompareTo(ColumnDrive other)
        {
            return string.Compare(Name, other.Name, StringComparison.CurrentCulture);
        }

        public override string ToString()
        {
            return displayName;
        }
    }
}