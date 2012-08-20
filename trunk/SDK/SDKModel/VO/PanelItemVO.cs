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
                return SubItems[ColIndex - 1];
        }
    }

    public enum PanelItemSortDirection
    {
        Ascending,
        Descending
    }

    public class PanelItemComparer : IComparer<PanelItemVO>
    {
        private int m_SortIndex = 0;
        private PanelItemSortDirection m_SortDirection = PanelItemSortDirection.Ascending;

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
                    string S1, S2;
                    if (SortDirection == PanelItemSortDirection.Ascending)
                    {
                        S1 = Item1.GetText(SortIndex);
                        S2 = Item2.GetText(SortIndex);
                    }
                    else
                    {
                        S1 = Item2.GetText(SortIndex);
                        S2 = Item1.GetText(SortIndex);
                    }
                    Result = S1.ToUpper().CompareTo(S2.ToUpper());
                }
            return Result;
        }

        public int SortIndex
        {
            get { return m_SortIndex; }
            set { m_SortIndex = value; }
        }

        public PanelItemSortDirection SortDirection
        {
            get { return m_SortDirection; }
            set { m_SortDirection = value; }
        }
    }

}
