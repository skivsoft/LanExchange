using System;
using System.Collections.Generic;
using LanExchange.Model.Panel;
using LanExchange.Model;
using System.IO;

namespace LanExchange.Strategy.Panel
{
    public class FileEnumStrategy : AbstractPanelStrategy
    {
        public override void Algorithm()
        {
            if ((Subject as AbstractPanelItem) == null) return;
            //if (!(Subject is SharePanelItem)) return;
            /// if (!(Subject is FilePanelItem)) return;
            m_Result = new List<AbstractPanelItem>();
            var path = (Subject as AbstractPanelItem).GetFullName();
            var files = Directory.GetFileSystemEntries(path, "*.*");
            foreach (var fname in files)
                m_Result.Add(new FilePanelItem(Subject as AbstractPanelItem, fname));
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
