using System;
using LanExchange.Utils;
using LanExchange.UI;

namespace LanExchange.Model.Panel
{
    public class DomainPanelItem : AbstractPanelItem
    {
        public DomainPanelItem(AbstractPanelItem parent, ServerInfo si) : base(parent)
        {
            
        }

        public string Name { get; set; }

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
    }
}
