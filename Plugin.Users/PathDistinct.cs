using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LanExchange.Plugin
{
    public class PathDistinct
    {
        private Dictionary<string, int> m_Dict;

        public PathDistinct()
        {
            m_Dict = new Dictionary<string, int>(new StringEqualityComparer());

        }

        public IEnumerable<string> Items
        {
            get
            {
                foreach (var pair in m_Dict)
                    yield return pair.Key;
            }
        }

        public void Add(string path)
        {
            if (!m_Dict.ContainsKey(path))
                m_Dict.Add(path, 0);
        }

        public int Count 
        {
            get { return m_Dict.Count; }
        }

        private string GetPrefix()
        {
            var result = new List<string>();

            int index = 0;
            string current;
            while (true)
            {
                var enumDict = m_Dict.GetEnumerator();
                enumDict.MoveNext();
                var str = enumDict.Current.Key.Split('\\');
                if (index + 1 > str.Length) break;
                current = str[index];

                bool allEqual = true;
                while(enumDict.MoveNext())
                {
                    str = enumDict.Current.Key.Split('\\');
                    if (index + 1 > str.Length) break;
                    if (String.Compare(current, str[index], StringComparison.InvariantCultureIgnoreCase) != 0)
                    {
                        allEqual = false;
                        break;
                    }
                }
                if (allEqual)
                    result.Add(current);
                else
                    break;
                index++;
            }
            return String.Join(@"\", result.ToArray());
        }

        public string Prefix
        {
            get { return GetPrefix(); }
        }
    }
}
