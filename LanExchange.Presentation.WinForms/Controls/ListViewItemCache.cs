// using NLog;


using System.Windows.Forms;

namespace LanExchange.Presentation.WinForms.Controls
{
    internal sealed class ListViewItemCache
    {
        // private readonly static Logger logger = LogManager.GetCurrentClassLogger();


        // private ListViewItem[] TableCache;

        // private int TableCacheStartIndex;

        private readonly IListViewItemGetter getter;

        // TODO Cache is currently OFF. Need perfomance stress-tests.
        public ListViewItemCache(IListViewItemGetter getter)
        {
            this.getter = getter;
            // TableCache = new ListViewItem[0];

        }
        
        public void CacheVirtualItems(object sender, CacheVirtualItemsEventArgs e)
        {
            // logger.Info("CacheViertualItems({0}, {1})", e.StartIndex, e.EndIndex);

            /*
            int Count = e.EndIndex - e.StartIndex + 1;
            TableCacheStartIndex = e.StartIndex;
            TableCache = new ListViewItem[Count];
            if (m_Getter != null)
            for (int i = 0; i < Count; i++)
                TableCache[i] = m_Getter.GetListViewItemAt(TableCacheStartIndex + i);
            */
        }

        public void RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            // int TableCacheEndIndex = TableCacheStartIndex + TableCache.Length - 1;

            var lv = sender as ListView;
            if (lv == null) return;
            if (e.ItemIndex == 0)
                e.Item = null;
            if (e.ItemIndex >=  0 && e.ItemIndex <= lv.VirtualListSize - 1)
            {
                if (getter != null)
                    e.Item = getter.GetListViewItemAt(e.ItemIndex);
                // e.Item = TableCache[e.ItemIndex - TableCacheStartIndex];

            }
            if (e.Item == null)
                e.Item = new ListViewItem();
            // TODO !STRANGE SUBITEM COUNT
            for (int i = e.Item.SubItems.Count; i < lv.Columns.Count; i++)
                e.Item.SubItems.Add(string.Empty);
        }
    }
}
