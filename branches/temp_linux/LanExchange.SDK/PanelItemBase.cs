using System;
using System.Xml.Serialization;

namespace LanExchange.SDK
{
    /// <summary>
    /// Base class for any LanExchange panel item.
    /// </summary>
    [Serializable]
    public abstract class PanelItemBase : IEquatable<PanelItemBase>, IComparable<PanelItemBase>, IComparable, ICloneable
    {
        private bool m_IsReachable;

        protected PanelItemBase()
        {
            m_IsReachable = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PanelItemBase"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        protected PanelItemBase(PanelItemBase parent)
            : this()
        {
            Parent = parent;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [XmlAttribute]
        public abstract string Name { get; set; }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        public virtual string FullName
        {
            get { return Name; }
        }

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
        public abstract string ImageName { get; }

        public abstract string ImageLegendText { get; }

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
            int compres;
            if (value1 == null && value2 == null)
                compres = 0;
            else
                if (value1 == null) compres = -1;
                else
                    if (value2 == null) compres = 1;
                    else
                        compres = value1.CompareTo(value2);
            return compres == 0 ? CompareTo(other) : compres;
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

        public bool Equals(PanelItemBase other)
        {
            return CompareTo(other) == 0;
        }

        public override int GetHashCode()
        {
            return Name != null ? Name.GetHashCode() : 0;
        }

        public abstract object Clone();

        public virtual bool IsRereadAccepted(string subject)
        {
            return false;
        }

        public string GetTabImageName()
        {
            var item = this;
            while (item.Parent != null)
            {
                if (item.Parent is PanelItemRoot)
                    return item.ImageName;
                item = Parent;
            }
            return item.GetType().Name + PanelImageNames.NORMAL_POSTFIX;
        }
    }
}