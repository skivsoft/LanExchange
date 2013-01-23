using System;
using LanExchange.Utils;
using LanExchange.UI;

namespace LanExchange.Model.Panel
{
    public class DomainPanelItem : AbstractPanelItem
    {
        private readonly ServerInfo m_SI;

        public DomainPanelItem(AbstractPanelItem parent, ServerInfo si) : base(parent)
        {
            m_SI = si;
        }

        public DomainPanelItem(string domain) : base(null)
        {
            m_SI = new ServerInfo(domain, String.Empty);
        }

        public override bool IsCacheable
        {
            get { return true; }
        }

        public override string Name
        {
            get { return m_SI.Name; }
            set { m_SI.Name = value; }
        }

        public override int CountColumns
        {
            get { return 1; }
        }

        public override IComparable this[int index]
        {
            get
            {
                if (index == 0)
                    return Name;
                throw new ArgumentOutOfRangeException("index");
            }
        }

        public override string ToolTipText
        {
            get { return String.Empty; }
        }

        public override IPanelColumnHeader CreateColumnHeader(int index)
        {
            return new ColumnHeaderEx { Visible = true, Text = "Домен" };
        }

        public override string ToString()
        {
            return string.Empty;
        }

        //public int CompareTo(DomainPanelItem other)
        //{
        //    return String.Compare(Name, other.Name, StringComparison.Ordinal);
        //}
    }
}
