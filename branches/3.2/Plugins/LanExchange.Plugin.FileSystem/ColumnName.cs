using System;

namespace LanExchange.Plugin.FileSystem
{
    internal class ColumnName : IComparable, IComparable<ColumnName>
    {
        public ColumnName(string name, bool isDirectory)
        {
            Name = name;
            IsDirectory = isDirectory;
        }

        public string Name { get; private set; }
        
        public bool IsDirectory { get; private set; }


        public int CompareTo(object obj)
        {
            return CompareTo(obj as ColumnName);
        }

        public int CompareTo(ColumnName other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            if (IsDirectory)
            {
                if (!other.IsDirectory)
                    return -1;
            }
            else if (other.IsDirectory)
                return 1;
            return string.Compare(Name, other.Name, StringComparison.CurrentCultureIgnoreCase);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
