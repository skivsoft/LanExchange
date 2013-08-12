using System;
using System.Collections.Generic;
using System.Threading;

namespace LanExchange.SDK
{
    public abstract class LazyPanelColumn : PanelColumnHeader
    {
        private readonly Dictionary<PanelItemBase, IComparable> m_Dict;

        protected LazyPanelColumn(string text, int width = 0)
            : base(text, width)
        {
            m_Dict = new Dictionary<PanelItemBase, IComparable>();
        }

        public abstract IComparable SyncGetData(PanelItemBase panelItem);

        public IDictionary<PanelItemBase, IComparable>  Dict
        {
            get { return m_Dict; }
        }
    }
}
