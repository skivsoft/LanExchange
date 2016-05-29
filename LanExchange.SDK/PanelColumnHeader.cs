using System;
using System.Collections.Generic;
using System.ComponentModel;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.SDK
{
    /// <summary>
    /// Column header interface returns by <cref>PanelItemBase</cref>.
    /// </summary>
    public sealed class PanelColumnHeader : IColumnHeader
    {
        public PanelColumnHeader(string text, int width = 120)
        {
            Text = text;
            Width = width;
            Visible = true;
            LazyDict = new Dictionary<PanelItemBase, IComparable>();
        }

        public IDictionary<PanelItemBase, IComparable> LazyDict { get; }

        public string Text { get; private set; }

        public int Width { get; private set; }

        public bool Visible { get; set; }

        public bool Refreshable { get; set; }

        public Func<PanelItemBase, IComparable> Callback { get; set; }

        public int Index { get; set; }

        [DefaultValue(HorizontalAlignment.Left)]
        public HorizontalAlignment TextAlign { get; set; }
    }
}