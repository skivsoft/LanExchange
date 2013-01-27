using System;
using System.Text;

namespace LanExchange.Sdk
{
    /// <summary>
    /// Base class for any LanExchange panel item.
    /// </summary>
    public abstract class PanelItemBase : IComparable<PanelItemBase>, IEquatable<ISubject>, IComparable, IColumnComparable, ISubject
    {
        /// <summary>
        /// The BACK
        /// </summary>
        public static readonly string BACK = String.Empty;

        private ISubject m_ParentSubject;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public abstract string Name { get; set; }
        /// <summary>
        /// Gets the count columns.
        /// </summary>
        /// <value>
        /// The count columns.
        /// </value>
        public abstract int CountColumns { get; }
        /// <summary>
        /// Gets or sets the <see cref="IComparable"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="IComparable"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public abstract IComparable this[int index] { get; }
        /// <summary>
        /// Gets the name of the image.
        /// </summary>
        /// <value>
        /// The name of the image.
        /// </value>
        public abstract string ImageName { get; }
        /// <summary>
        /// Gets the tool tip text.
        /// </summary>
        /// <value>
        /// The tool tip text.
        /// </value>
        public abstract string ToolTipText { get; }
        /// <summary>
        /// Creates the column header.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public abstract IPanelColumnHeader CreateColumnHeader(int index);

        /// <summary>
        /// Initializes a new instance of the <see cref="PanelItemBase"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        protected PanelItemBase(PanelItemBase parent)
        {
            Parent = parent;
        }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        public PanelItemBase Parent { get; set; }

        /// <summary>
        /// Gets or sets the parent subject. This value is meanfull when <cref>Parent</cref> is null.
        /// </summary>
        /// <value>
        /// The parent subject.
        /// </value>
        public ISubject ParentSubject
        {
            get { return Parent ?? m_ParentSubject; }
            set
            {
                if (Parent == null)
                    m_ParentSubject = value;
            }
        }

        /// <summary>
        /// Gets the column value.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">index</exception>
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
            var otherItem = other as PanelItemBase;
            if (otherItem == null) return 1;
            var value1 = GetColumnValue(column);
            var value2 = otherItem.GetColumnValue(column);
            return value1.CompareTo(value2);
        }


        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public int CompareTo(PanelItemBase other)
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
            return CompareTo(obj as PanelItemBase);
        }

        /// <summary>
        /// Gets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        public string Subject
        {
            get { return Name; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is cacheable.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is cacheable; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsCacheable
        {
            get { return false; }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the strings upper.
        /// </summary>
        /// <returns></returns>
        public string[] GetStringsUpper()
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

        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public bool Equals(ISubject other)
        {
            return String.Compare(Subject, other.Subject, StringComparison.Ordinal) == 0;
        }
    }
}