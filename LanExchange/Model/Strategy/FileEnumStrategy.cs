using System.Collections.Generic;
using LanExchange.Model.Panel;
using System.IO;
using LanExchange.Sdk;

namespace LanExchange.Model.Strategy
{
    public class FileEnumStrategy : PanelStrategyBase
    {
        public override void Algorithm()
        {
            if ((Subject as PanelItemBase) == null) return;
            Result = new List<PanelItemBase>();
            var path = (Subject as PanelItemBase).GetFullName();
            var files = Directory.GetFileSystemEntries(path, "*.*");
            foreach (var fname in files)
                Result.Add(new FilePanelItem(Subject as PanelItemBase, fname));
        }

        public override void AcceptSubject(ISubject subject, out bool accepted)
        {
            accepted = false;
            // TODO: add file navigator little bit later
            //if (subject is SharePanelItem)
            //    accepted = true;
            //if (subject is FilePanelItem)
            //    accepted = (subject as FilePanelItem).IsDirectory;
        }
    }
}
