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
        private int m_SortIndex = 0;
        private PanelItemSortDirection m_SortDirection = PanelItemSortDirection.Ascending;

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

        public void Sort()
        {
            PanelItemComparer comparer = new PanelItemComparer();
            comparer.SortIndex = m_SortIndex;
            comparer.SortDirection = m_SortDirection;
            ((List<PanelItemVO>)Data).Sort(comparer);
        }

        public void ChangeSort(int ColIndex)
        {
            if (m_SortIndex == ColIndex)
                if (m_SortDirection == PanelItemSortDirection.Ascending)
                    m_SortDirection = PanelItemSortDirection.Descending;
                else
                    m_SortDirection = PanelItemSortDirection.Ascending;
            else
            {
                m_SortIndex = ColIndex;
                m_SortDirection = PanelItemSortDirection.Ascending;
            }
        }

        public virtual ColumnVO[] GetColumns()
        {
            return new ColumnVO[] { new ColumnVO("", 100) };
        }
    }
}
