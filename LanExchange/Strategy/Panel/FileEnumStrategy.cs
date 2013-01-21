using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.Model;

namespace LanExchange.Strategy.Panel
{
    public class FileEnumStrategy : AbstractPanelStrategy
    {
        public FileEnumStrategy(string subject)
            : base(subject)
        {

        }
        public override void Algorithm()
        {
            //
        }

        public override bool AcceptParent(AbstractPanelItem parent)
        {
            if ((parent as SharePanelItem) != null)
                return true;
            if ((parent as FilePanelItem) != null)
                return true;
            return false;
        }
    }
}
