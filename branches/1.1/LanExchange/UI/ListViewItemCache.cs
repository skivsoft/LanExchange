using System;
using System.Windows.Forms;
using NLog;

namespace LanExchange.Utils
{
    public interface IListViewItemGetter
    {
        ListViewItem GetListViewItemAt(int Index);
    }
    
    public class ListViewItemCache
    {
        private readonly static Logger logger = LogManager.GetCurrentClassLogger();

        private ListViewItem[] TableCache;
        //private int TableCacheStartIndex;
        private readonly IListViewItemGetter m_Getter;

        // TODO Cache is currently OFF. Need perfomance stress-tests.
        public ListViewItemCache(IListViewItemGetter getter)
        {
            m_Getter = getter;
            TableCache = new ListViewItem[0];
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
            //if (e.ItemIndex >= TableCacheStartIndex && e.ItemIndex <= TableCacheEndIndex)
            {
                if (m_Getter != null)
                    e.Item = m_Getter.GetListViewItemAt(e.ItemIndex);
                //e.Item = TableCache[e.ItemIndex - TableCacheStartIndex];
            }
            //else
            //{
            //    e.Item = new ListViewItem();
            //    for (int i = 0; i < (sender as ListView).Columns.Count - 1; i++)
            //        e.Item.SubItems.Add(String.Empty);
            //}
        }
    }
}
