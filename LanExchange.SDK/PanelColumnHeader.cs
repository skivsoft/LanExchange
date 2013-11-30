using System;
using System.Collections.Generic;

namespace LanExchange.SDK
{
    /// <summary>
    /// Column header interface returns by <cref>PanelItemBase</cref>.
    /// </summary>
    public sealed class PanelColumnHeader
    {
        private readonly IDictionary<PanelItemBase, IComparable> m_LazyDict;

        public PanelColumnHeader(string text, int width = 120)
        {
            Text = text;
            Width = width;
            Visible = true;
            m_LazyDict = new Dictionary<PanelItemBase, IComparable>();
        }

        public IDictionary<PanelItemBase, IComparable> LazyDict
        {
            get { return m_LazyDict; }
        }

        public string Text { get; private set; }

        public int Width { get; private set; }

        public bool Visible { get; set; }

        public bool Refreshable { get; set; }

        public LazyCallback Callback { get; set; }

        public int Index { get; set; }
    }
}