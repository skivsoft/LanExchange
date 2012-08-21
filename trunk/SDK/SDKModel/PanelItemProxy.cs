using System;
using System.Collections.Generic;
using System.Text;
using PurePatterns;
using PureInterfaces;
using LanExchange.SDK.SDKModel;
using LanExchange.SDK.SDKModel.VO;

namespace LanExchange.SDK.SDKModel
{
    public abstract class PanelItemProxy : Proxy, IPanelItemProxy, IProxy
    {
        private const int MAX_TIME_FOR_MULTISORT = 1000;
        private int LastSortTick = 0;
        private PanelItemComparer m_Comparer;

        public PanelItemProxy(string name)
            : base(name, new List<PanelItemVO>())
        {
            m_Comparer = new PanelItemComparer();
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
            if (Math.Abs(LastSortTick - System.Environment.TickCount) > MAX_TIME_FOR_MULTISORT)
            {
                if (m_Comparer.SortOrders.Count == 0)
                    m_Comparer.SortOrders.Add(new PanelItemSortOrder(ColIndex, PanelItemSortDirection.Ascending));
                else
                {
                    PanelItemSortOrder order = m_Comparer.SortOrders[0];
                    if (order.Index == ColIndex)
                        order.SwitchDirection();
                    else
                    {
                        order.Index = ColIndex;
                        order.Direction = PanelItemSortDirection.Ascending;
                    }
                    m_Comparer.SortOrders.Clear();
                    m_Comparer.SortOrders.Add(order);
                }
            }
            else
            {
                PanelItemSortOrder order;
                bool bFound = false;
                for (int i = 0; i < m_Comparer.SortOrders.Count; i++)
                    if (m_Comparer.SortOrders[i].Index == ColIndex)
                    {
                        order = m_Comparer.SortOrders[i];
                        order.SwitchDirection();
                        m_Comparer.SortOrders.RemoveAt(i);
                        m_Comparer.SortOrders.Insert(i, order);
                        bFound = true;
                        break;
                    }
                if (!bFound)
                    m_Comparer.SortOrders.Add(new PanelItemSortOrder(ColIndex, PanelItemSortDirection.Ascending));
            }
            ((List<PanelItemVO>)Data).Sort(m_Comparer);
            LastSortTick = System.Environment.TickCount;
        }

        public virtual ColumnVO[] GetColumns()
        {
            return new ColumnVO[] { new ColumnVO("", 100) };
        }
    }
}
