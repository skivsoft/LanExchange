using System;
using System.Collections.Generic;
using System.Text;

namespace LanExchange.Model.VO
{
    public class PanelItemVO : IComparable<PanelItemVO>
    {
        private string m_Name;
        private string[] m_Items;
        private bool m_Back = false;

        public PanelItemVO(string name, bool back, params string[] items)
        {
            m_Name = name;
            m_Back = back;
            m_Items = items;
        }


        public string Name
        {
            get { return m_Back ? ".." : m_Name; }
            set { m_Name = value; }
        }

        public string[] SubItems
        {
            get { return m_Items; }
            set { m_Items = value; }
        }

        public bool IsBackButton
        {
            get { return m_Back; }
            set { m_Back = value; }
        }

        public virtual int CompareTo(PanelItemVO p2)
        {
            int Result;
            if (this.Name == "..")
                if (p2.Name == "..")
                    Result = 0;
                else
                    Result = -1;
            else
                if (p2.Name == "..")
                    Result = +1;
                else
                    Result = this.Name.CompareTo(p2.Name);
            return Result;
        }
    }

    public class PanelItemComparer : IComparer<PanelItemVO>
    {
        public int Compare(PanelItemVO Item1, PanelItemVO Item2)
        {
            return Item1.CompareTo(Item2);
        }
    }

}
