using System.Windows.Forms;
using System;
//using NLog;

namespace LanExchange.UI
{
    // TODO need use cache
    public class ListViewItemCache
    {
        //private readonly static Logger logger = LogManager.GetCurrentClassLogger();

        //private ListViewItem[] TableCache;
        //private int TableCacheStartIndex;
        private readonly IListViewItemGetter m_Getter;

        // TODO Cache is currently OFF. Need perfomance stress-tests.
        public ListViewItemCache(IListViewItemGetter getter)
        {
            m_Getter = getter;
            //TableCache = new ListViewItem[0];
        }
        
        public void CacheVirtualItems(object sender, CacheVirtualItemsEventArgs e)
        {
            //logger.Info("CacheViertualItems({0}, {1})", e.StartIndex, e.EndIndex);
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
            //int TableCacheEndIndex = TableCacheStartIndex + TableCache.Length - 1;
            var lv = sender as ListView;
            if (lv == null) return;
            if (e.ItemIndex >=  0 && e.ItemIndex <= lv.VirtualListSize-1)
            {
                if (m_Getter != null)
                {
                    e.Item = m_Getter.GetListViewItemAt(e.ItemIndex);
                    for (int i = e.Item.SubItems.Count+1; i < lv.Columns.Count; i++)
                        e.Item.SubItems.Add(String.Empty);                
                }
                //e.Item = TableCache[e.ItemIndex - TableCacheStartIndex];
            }
            else
            {
                e.Item = new ListViewItem();
                for (int i = 0; i < (sender as ListView).Columns.Count - 1; i++)
                    e.Item.SubItems.Add(String.Empty);
            }
        }
    }
}
