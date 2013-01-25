using System;
using LanExchange.Utils.Sorting;
using System.Text;

namespace LanExchange.Model.Panel
{
    public abstract class AbstractPanelItem : IComparable<AbstractPanelItem>, IEquatable<ISubject>, IComparable, IColumnComparable, ISubject
    {
        public static readonly string BACK = String.Empty;

        private ISubject m_ParentSubject;

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
        
        public ISubject ParentSubject
        {
            get { return Parent ?? m_ParentSubject; }
            set
            {
                if (Parent == null)
                    m_ParentSubject = value;
            }
        }

        public virtual int ImageIndex
        {
            get { return Name == BACK ? LanExchangeIcons.FolderBack : -1; }
        }

        private IComparable GetColumnValue(int index)
        {
            if (index < 0 || index > CountColumns-1)
                throw new ArgumentOutOfRangeException("index");
            return this[index];
        }

        /// <summary>
        /// IColumnsComparable.CompareTo implementation.
        /// </summary>
        /// <param name="other"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public int CompareTo(object other, int column)
        {
            var otherItem = other as AbstractPanelItem;
            if (otherItem == null) return 1;
            var value1 = GetColumnValue(column);
            var value2 = otherItem.GetColumnValue(column);
            return value1.CompareTo(value2);
        }

        /// <summary>
        /// IComparable&lt;AbstractPanelItem&gt;.CompareTo implementation.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(AbstractPanelItem other)
        {
            if ((Name == BACK) && (other.Name != BACK))
                return -1;
            if ((Name != BACK) && (other.Name == BACK))
                return 1;
            return String.Compare(Name, other.Name, StringComparison.Ordinal);
            //return CompareTo(other, 0);
        }

        /// <summary>
        /// IComparable.CompareTo implementation
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            return CompareTo(obj as AbstractPanelItem);
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
            {
                object item = this[i];
                if (item != null)
                    result[i] = item.ToString().ToUpper();
            }
            return result;
        }

        public bool Equals(ISubject other)
        {
            return String.Compare(Subject, other.Subject, StringComparison.Ordinal) == 0;
        }
    }
}