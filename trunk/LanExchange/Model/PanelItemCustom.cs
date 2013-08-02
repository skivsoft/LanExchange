using System;
using LanExchange.SDK;

namespace LanExchange.Model
{
    public class PanelItemCustom : PanelItemBase
    {
        public PanelItemCustom(PanelItemBase parent, string name) : base(parent)
        {
            Name = name;
        }

        public override sealed string Name { get; set; }

        public override int CountColumns
        {
            get { return 3; }
        }

        public override IComparable this[int index]
        {
            get
            {
                switch(index)
                {
                    case 0:
                        return Name;
                    case 1:
                    case 2:
                        return " ";
                    default:
                        throw new ArgumentOutOfRangeException("index");
                }
            }
        }

        public override string ImageName
        {
            get { return string.Empty; }
        }

        public override PanelColumnHeader CreateColumnHeader(int index)
        {
            PanelColumnHeader result = null;
            switch(index)
            {
                case 0:
                    result = new PanelColumnHeader("Column0");
                    break;
                case 1:
                    result = new PanelColumnHeader("Column1");
                    break;
                case 2:
                    result = new PanelColumnHeader("Column2");
                    break;
            }
            return result;
        }
    }
}
