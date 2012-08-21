using System;
using System.Collections.Generic;
using System.Text;

namespace LanExchange.SDK.SDKModel.VO
{
    public class PanelItemVO
    {
        private string m_Name;
        private bool m_Back = false;
        private object m_Data = null;

        public PanelItemVO(string name, object data)
        {
            m_Name = name;
            m_Data = data;
            m_Back = name == "..";
        }


        public string Name
        {
            get { return m_Back ? ".." : m_Name; }
            set { m_Name = value; }
        }

        public bool IsBackButton
        {
            get { return m_Back; }
            set { m_Back = value; }
        }

        public virtual string[] SubItems
        {
            get { return new string[0] {}; }
        }

        public string GetText(int ColIndex)
        {
            if (ColIndex == 0)
                return m_Name;
            else
                if (ColIndex - 1 < SubItems.Length)
                    return SubItems[ColIndex - 1];
                else
                    return String.Empty;
        }

        public virtual int CompareTo(PanelItemVO obj, int Index)
        {
            string S1 = this.GetText(Index);
            string S2 = obj.GetText(Index);
            return String.Compare(S1, S2, StringComparison.CurrentCultureIgnoreCase);
        }
    }

    public enum PanelItemSortDirection
    {
        Ascending,
        Descending
    }

    public struct PanelItemSortOrder
    {
        public int Index;
        public PanelItemSortDirection Direction;

        public PanelItemSortOrder(int index, PanelItemSortDirection direction)
        {
            Index = index;
            Direction = direction;
        }
    }


    public class PanelItemComparer : IComparer<PanelItemVO>
    {
        private List<PanelItemSortOrder> m_SortOrders = new List<PanelItemSortOrder>();

        public int Compare(PanelItemVO Item1, PanelItemVO Item2)
        {
            int Result;
            if (Item1.IsBackButton)
                if (Item2.IsBackButton)
                    Result = 0;
                else
                    Result = -1;
            else
                if (Item2.IsBackButton)
                    Result = +1;
                else
                {
                    Result = 0;
                    int index = 0;
                    while (index < m_SortOrders.Count && Result == 0)
                    {
                        PanelItemSortOrder order = m_SortOrders[index];
                        if (order.Direction == PanelItemSortDirection.Ascending)
                            Result = Item1.CompareTo(Item2, order.Index);
                        else
                            Result = Item2.CompareTo(Item1, order.Index);
                        index++;
                    }
                }
            return Result;
        }

        public List<PanelItemSortOrder> SortOrders
        {
            get { return m_SortOrders; }
        }
    }

}
