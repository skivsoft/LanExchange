using System;
using System.Data;
using System.DirectoryServices;

namespace Plugins.Plugin
{
    internal class ConcreteADExecutor// : IADExecutor
    {
        private DirectoryEntry m_Root;

        public void Connect(string path)
        {
            m_Root = new DirectoryEntry(path);
        }

        public void Dispose()
        {
            if (m_Root != null)
                m_Root.Dispose();
        }

        //string Filter
        public DataTable Query(string filter, string[] fields)
        {
            //result.Columns.Add("qqq");
            //result.Columns.Add("www");

            //var row = result.NewRow();
            //row["qqq"] = "QQQ";
            //row["www"] = "WWW";
            //result.Rows.Add(row);

            //return result;

            var result = new DataTable();

            using (var searcher = new DirectorySearcher(m_Root))
            {
                searcher.Filter = filter;

                // add requested field to searcher
                foreach(var str in fields)
                    searcher.PropertiesToLoad.Add(str);

                SearchResultCollection src = searcher.FindAll();
                int index = 0;
                foreach (SearchResult sr in src)
                {
                    // if no properties, leave this loop
                    if (sr.Properties == null) break;
                    if (sr.Properties.PropertyNames == null) break;

                    if (index == 0)
                    {
                        foreach (string propName in sr.Properties.PropertyNames)
                            result.Columns.Add(propName);
                    }

                    DataRow row = result.NewRow();
                    // add column to result DataTable
                    foreach (string propName in sr.Properties.PropertyNames)
                    {
                        var prop = sr.Properties[propName];

                        if (prop.Count == 0)
                            row[propName] = String.Empty;
                        else
                            row[propName] = prop[0];
                    }
                    result.Rows.Add(row);
                    index++;
                }
            }
            return result;
        }
    }
}
