using System;
using System.Collections.Generic;
using System.Text;

namespace LanExchange.Model
{
    public class FilePanelItem : AbstractPanelItem
    {

        protected FilePanelItem(AbstractPanelItem parent) : base(parent)
        {
            
        }

        public override int CountColumns
        {
            get { throw new NotImplementedException(); }
        }

        public override IComparable this[int index]
        {
            get { throw new NotImplementedException(); }
        }

        public override string ToolTipText
        {
            get { throw new NotImplementedException(); }
        }

        public override IPanelColumnHeader CreateColumnHeader(int index)
        {
            throw new NotImplementedException();
        }
    }
}
