using System;
using LanExchange.Utils.Sorting;
using System.Text;

namespace LanExchange.Model.Panel
{
    public abstract class AbstractPanelItem : IComparable<AbstractPanelItem>, IEquatable<ISubject>, IColumnComparable, ISubject
    {
        public const string BACK = "..";
        public abstract string Name { get; set; }
        public abstract int CountColumns { get; }
        public abstract IComparable this[int index] { get; }
        public abstract string ToolTipText { get; }
        public abstract IPanelColumnHeader CreateColumnHeader(int index);

        protected AbstractPanelItem(AbstractPanelItem parent)
        {
            Parent = parent;
        }

        public AbstractPanelItem Parent { get; set; }

        public virtual int ImageIndex
        {
            get { return -1; }
        }

        private IComparable GetColumnValue(int index)
        {
            if (index < 0 || index > CountColumns-1)
                throw new ArgumentOutOfRangeException("index");
            return this[index];
        }

        public int CompareTo(object other, int column)
        {
            var otherItem = other as AbstractPanelItem;
            if (otherItem == null) return 1;
            var value1 = GetColumnValue(column);
            var value2 = otherItem.GetColumnValue(column);
            return value1.CompareTo(value2);
        }

        public int CompareTo(AbstractPanelItem other)
        {
            return CompareTo(other, 0);
        }

        public string Subject
        {
            get { return Name; }
        }

        public virtual bool IsCacheable
        {
            get { return false; }
        }

        public override string ToString()
        {
            return Name;
        }


        public string GetFullName()
        {
            var sb = new StringBuilder();
            var item = this;
            while (true)
            {
                var s = item.ToString();
                if (s != string.Empty)
                {
                    if (sb.Length > 0)
                        sb.Insert(0, @"\");
                    sb.Insert(0, s);
                }
                if (item.Parent == null) break;
                item = item.Parent;
            }
            return sb.ToString();
        }

        internal string[] GetStringsUpper()
        {
            var result = new string[CountColumns];
            for (int i = 0; i < result.Length; i++)
                result[i] = this[i].ToString().ToUpper();
            return result;
        }

        public bool Equals(ISubject other)
        {
            return String.Compare(Subject, other.Subject, StringComparison.Ordinal) == 0;
        }

    }
}