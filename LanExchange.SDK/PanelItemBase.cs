using System;
using System.Globalization;
using System.Text;

namespace LanExchange.SDK
{
    // TODO refactor this! what we compare Name or Subject?
    /// <summary>
    /// Base class for any LanExchange panel item.
    /// </summary>
    public abstract class PanelItemBase : IComparable<PanelItemBase>, IComparable, IColumnComparable
    {
        /// <summary>
        /// The ".." item
        /// </summary>
        public static readonly string s_DoubleDot = String.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="PanelItemBase"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        protected PanelItemBase(PanelItemBase parent)
        {
            Parent = parent;
        }

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
        /// Creates the column header.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public abstract PanelColumnHeader CreateColumnHeader(int index);
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
        /// Gets or sets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        public PanelItemBase Parent { get; set; }

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
        /// Gets the tool tip text.
        /// </summary>
        /// <value>
        /// The tool tip text.
        /// </value>
        public virtual string ToolTipText
        {
            get 
            { 
                var sb = new StringBuilder();
                for (int i = 1; i < CountColumns; i++)
                {
                    if (sb.Length > 0)
                        sb.AppendLine();
                    sb.Append(GetColumnValue(i));
                }
                return sb.ToString();
            }
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
            if (other == null) return 1;
            if ((Name == s_DoubleDot) && (other.Name != s_DoubleDot))
                return -1;
            if ((Name != s_DoubleDot) && (other.Name == s_DoubleDot))
                return 1;
            int result = String.Compare(Name, other.Name, StringComparison.OrdinalIgnoreCase);
            // TODO !!! CHEC ITEM SORT
            return result;
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
        /// Gets the full name of panel item.
        /// </summary>
        /// <returns></returns>
        public string FullItemName
        {
            get
            {
                var sb = new StringBuilder();
                var item = this;
                while (true)
                {
                    var s = item.ToString();
                    if (!String.IsNullOrEmpty(s))
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
                    result[i] = item.ToString().ToUpper(CultureInfo.InvariantCulture);
            }
            return result;
        }
    }
}