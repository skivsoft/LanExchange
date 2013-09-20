using System;
using System.Collections.Generic;

namespace LanExchange.SDK
{
    /// <summary>
    /// Column header interface returns by <cref>PanelItemBase</cref>.
    /// </summary>
    public sealed class PanelColumnHeader
    {
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text;

        public bool Visible;

        public bool Refreshable;

        public int Width;

        public LazyCallback Callback;

        public int Index;

        private readonly IDictionary<PanelItemBase, IComparable> m_LazyDict;

        public PanelColumnHeader(string text, int width = 0)
        {
            Text = text;
            Width = width == 0 ? 130 : width;
            Visible = true;
            m_LazyDict = new Dictionary<PanelItemBase, IComparable>();
        }

        public IDictionary<PanelItemBase, IComparable> LazyDict
        {
            get { return m_LazyDict; }
        }
    }
}