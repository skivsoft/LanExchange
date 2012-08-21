using System;
using System.Collections.Generic;
using System.Text;
using PurePatterns;
using PureInterfaces;
using LanExchange.Model.VO;
using LanExchange.SDK.SDKModel;
using LanExchange.SDK.SDKModel.VO;

namespace LanExchange.Model
{
    public abstract class PanelItemProxy : Proxy, IPanelItemProxy, IProxy
    {
        private PanelItemSortOrder m_LastSortOrder = new PanelItemSortOrder(-1, PanelItemSortDirection.Ascending);

        public PanelItemProxy(string name)
            : base(name, new List<PanelItemVO>())
        {
        }

        public IList<PanelItemVO> Objects
        {
            get { return (IList<PanelItemVO>)Data; }
        }

        public virtual int NumObjects
        {
            get { return Objects.Count; }
        }

        public abstract void EnumObjects(string path);

        public void Sort(int ColIndex)
        {
            PanelItemSortOrder NewWorldOrder = m_LastSortOrder;
            PanelItemComparer comparer = new PanelItemComparer();
            if (ColIndex == m_LastSortOrder.Index)
            {
                if (m_LastSortOrder.Direction == PanelItemSortDirection.Ascending)
                    m_LastSortOrder.Direction = PanelItemSortDirection.Descending;
                else
                    m_LastSortOrder.Direction = PanelItemSortDirection.Ascending;
                comparer.SortOrders.Add(m_LastSortOrder);
            }
            else
            {
                PanelItemSortOrder NewSortOrder = new PanelItemSortOrder(ColIndex, PanelItemSortDirection.Ascending);
                comparer.SortOrders.Add(NewSortOrder);
                if (m_LastSortOrder.Index != -1)
                    comparer.SortOrders.Add(m_LastSortOrder);
                m_LastSortOrder = NewSortOrder;
            }
            ((List<PanelItemVO>)Data).Sort(comparer);
        }

        public virtual ColumnVO[] GetColumns()
        {
            return new ColumnVO[] { new ColumnVO("", 100) };
        }
    }
}
