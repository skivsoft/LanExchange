using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace LanExchange.Plugin.Users
{
    public sealed class PathDistinct
    {
        private readonly Dictionary<string, int> dict;

        public PathDistinct()
        {
            dict = new Dictionary<string, int>(new StringEqualityComparer());
        }

        public IEnumerable<string> Items
        {
            get
            {
                foreach (var pair in dict)
                    yield return pair.Key;
            }
        }

        public int Count
        {
            get { return dict.Count; }
        }

        public string Prefix
        {
            get { return GetPrefix(); }
        }

        public void Add(string path)
        {
            if (!dict.ContainsKey(path))
                dict.Add(path, 0);
        }

        [Localizable(false)]
        private string GetPrefix()
        {
            var result = new List<string>();

            int index = 0;
            while (true)
            {
                var enumDict = dict.GetEnumerator();
                if (!enumDict.MoveNext()) break;
                var str = enumDict.Current.Key.Split('\\');
                if (index + 1 > str.Length) break;
                string current = str[index];

                bool allEqual = true;
                while (enumDict.MoveNext())
                {
                    str = enumDict.Current.Key.Split('\\');
                    if (index + 1 > str.Length) break;
                    if (string.Compare(current, str[index], StringComparison.InvariantCultureIgnoreCase) != 0)
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

            return string.Join(@"\", result.ToArray());
        }
    }
}
