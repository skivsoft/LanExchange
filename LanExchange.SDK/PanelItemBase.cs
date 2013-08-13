using System;
using System.Globalization;
using System.Text;
using System.Xml.Serialization;

namespace LanExchange.SDK
{
    /// <summary>
    /// Base class for any LanExchange panel item.
    /// </summary>
    public class PanelItemBase : IEquatable<PanelItemBase>, IComparable<PanelItemBase>, IComparable
    {
        protected bool m_IsReachable = true;

        public PanelItemBase()
        {
            m_IsReachable = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PanelItemBase"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public PanelItemBase(PanelItemBase parent) : this()
        {
            Parent = parent;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public virtual string Name { get; set; }
        /// <summary>
        /// Gets the count columns.
        /// </summary>
        /// <value>
        /// The count columns.
        /// </value>
        public virtual int CountColumns 
        {
            get { return 1; }
        }

        protected virtual void SetValue(int index, IComparable value)
        {
            
        }

        public virtual IComparable GetValue(int index)
        {
            return Name;
        }

        /// <summary>
        /// Gets or sets the <see cref="IComparable"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="IComparable"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public IComparable this[int index]
        {
            get
            {
                if (index < 0 || index >= CountColumns)    
                    throw new ArgumentOutOfRangeException("index");
                return GetValue(index);
            }
            set
            {
                SetValue(index, value);
            }
        }
        /// <summary>
        /// Gets the name of the image.
        /// </summary>
        /// <value>
        /// The name of the image.
        /// </value>
        public virtual string ImageName
        {
            get { return string.Empty; }
        }

        public virtual string ImageLegendText
        {
            get { return string.Empty; }
        }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        [XmlIgnore]
        public PanelItemBase Parent { get; set; }

        [XmlIgnore]
        public bool IsReachable
        {
            get { return m_IsReachable; }
            set { m_IsReachable = value; }
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
                    sb.Append(this[i]);
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
            if ((this is PanelItemDoubleDot) && !(other is PanelItemDoubleDot))
                return -1;
            if (!(this is PanelItemDoubleDot) && (other is PanelItemDoubleDot))
                return 1;
            if ((this is PanelItemDoubleDot) && (other is PanelItemDoubleDot))
                return 0;
            var value1 = this[column];
            var value2 = otherItem[column];
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
            if ((this is PanelItemDoubleDot) && !(other is PanelItemDoubleDot))
                return -1;
            if (!(this is PanelItemDoubleDot) && (other is PanelItemDoubleDot))
                return 1;
            if ((this is PanelItemDoubleDot) && (other is PanelItemDoubleDot))
                return 0;
            int result = String.Compare(Name, other.Name, StringComparison.OrdinalIgnoreCase);
            // TODO !!! CHECK ITEM SORT
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

        public bool Equals(PanelItemBase other)
        {
            return CompareTo(other) == 0;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}