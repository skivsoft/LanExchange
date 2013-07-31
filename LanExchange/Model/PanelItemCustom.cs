using System;
using LanExchange.SDK;

namespace LanExchange.Model
{
    class PanelItemCustom : PanelItemBase
    {
        public PanelItemCustom(PanelItemBase parent) : base(parent)
        {
        }

        public PanelItemCustom(PanelItemBase parent, string name) : base(parent)
        {
            Name = name;
        }

        public override sealed string Name { get; set; }

        public override int CountColumns
        {
            get { return 1; }
        }

        public override IComparable this[int index]
        {
            get { return Name; }
        }

        public override string ImageName
        {
            get { return String.Empty; }
        }

        public override PanelColumnHeader CreateColumnHeader(int index)
        {
            var header = new PanelColumnHeader();
            header.Text = "Name";
            header.Visible = true;
            return header;
        }
    }
}
