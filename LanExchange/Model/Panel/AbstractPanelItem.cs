using System;
using LanExchange.Utils.Sorting;

namespace LanExchange.Model.Panel
{
    public abstract class AbstractPanelItem : IColumnComparable, IComparable<AbstractPanelItem>, ISubject
    {
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
            get { return this[0].ToString(); }
        }

        public override string ToString()
        {
            return this[0].ToString();
        }

        internal string[] GetStringsUpper()
        {
            var result = new string[CountColumns];
            for (int i = 0; i < result.Length; i++)
                result[i] = this[i].ToString().ToUpper();
            return result;
        }

    }
}