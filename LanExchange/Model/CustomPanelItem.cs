using System;
using System.Collections;
using System.Collections.Generic;
using LanExchange.SDK;

namespace LanExchange.Model
{
    public class CustomPanelItem : PanelItemBase
    {
        private readonly IDictionary<int, PanelColumnHeader> m_Header;
        private readonly IDictionary<int, IComparable> m_Data;
        private int m_CountColumns;

        public CustomPanelItem(PanelItemBase parent, string name) : base(parent)
        {
            m_Header = new Dictionary<int, PanelColumnHeader>();
            m_Data = new Dictionary<int, IComparable>();
            Name = name;
            SetCountColumns(1);
        }

        public override sealed string Name 
        {
            get { return this[0].ToString(); }
            set { this[0] = value; }
        }

        public override int CountColumns
        {
            get { return m_CountColumns;  }
        }

        public void SetCountColumns(int value)
        {
            m_CountColumns = value;
        }

        protected override IComparable GetValue(int index)
        {
            IComparable result;
            if (m_Data.TryGetValue(index, out result))
                return result;
            return null;
        }

        protected override void SetValue(int index, IComparable value)
        {
            m_Data.Remove(index);
            m_Data.Add(index, value);
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
