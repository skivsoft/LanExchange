using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LanExchange.SDK
{
    /// <summary>
    /// Path to nested object.
    /// </summary>
    public sealed class ObjectPath<TPanelItemBase> where TPanelItemBase : PanelItemBase
    {
        private readonly Stack<TPanelItemBase> path;

        /// <summary>
        /// Occurs when [changed].
        /// </summary>
        public event EventHandler Changed;

        public ObjectPath()
        {
            path = new Stack<TPanelItemBase>();
        }

        /// <summary>
        /// Array for xml-serialization.
        /// </summary>
        public TPanelItemBase[] Item { get; set; }

        private void OnChanged()
        {
            Item = path.ToArray();
            if (Changed != null)
                Changed(this, EventArgs.Empty);
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            path.Clear();
            OnChanged();
        }

        /// <summary>
        /// Pushes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Push(TPanelItemBase item)
        {
            path.Push(item);
            OnChanged();
        }

        /// <summary>
        /// Pops this instance.
        /// </summary>
        public void Pop()
        {
            path.Pop();
            OnChanged();
        }

        /// <summary>
        /// Peeks this instance.
        /// </summary>
        /// <returns></returns>
        public TPanelItemBase Peek()
        {
            return path.Peek();
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        [Localizable(false)]
        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var item in path)
            {
                if (sb.Length > 0)
                    sb.Insert(0, @"\");
                sb.Insert(0, item);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Gets a value indicating whether path is empty.
        /// </summary>
        /// <value>
        ///   <c>true</c> if path is empty; otherwise, <c>false</c>.
        /// </value>
        public bool IsEmpty
        {
            get { return path.Count == 0; }
        }

        public bool IsEmptyOrRoot
        {
            get
            {
                var parent = path.Count == 0 ? null : Peek();
                return (parent == null) || parent is PanelItemRootBase;
            }
        }
    }
}
