using System;
using System.Text;
using Tools;
using SkivSoft.LanExchange.SDK;
using System.Collections;
using System.Collections.Generic;

namespace LanExchange
{
    public class TLanEXItemList : ILanEXItemList
    {
        private SortedDictionary<string, ILanEXItem> data = null;
        private List<string> keys = null;
        private String filter = "";

        public TLanEXItemList()
        {
            data = new SortedDictionary<string, ILanEXItem>();
            keys = new List<string>();
        }
   
        public void Add(ILanEXItem Comp)
        {
            if (Comp != null)
              if (!String.IsNullOrEmpty(Comp.Name))
                if (!data.ContainsKey(Comp.Name))
                    data.Add(Comp.Name, Comp);
        }

        public void Delete(ILanEXItem Comp)
        {
            data.Remove(Comp.Name);
        }

        public ILanEXItem Get(string key)
        {
            ILanEXItem Result = null;
            if (data.TryGetValue(key, out Result))
            {
                Result.Name = key;
                return Result;
            }
            else
                return null;
        }

        public void Clear()
        {
            data.Clear();
        }

        private bool GoodForFilter(string[] A, string Filter1, string Filter2)
        {
            for (int i = 0; i < A.Length; i++)
            {
                if (i == 0)
                {
                    if (TPuntoSwitcher.RussianContains(A[i], Filter1) || (TPuntoSwitcher.RussianContains(A[i], Filter2)))
                        return true;
                } else
                if (Filter1 != null && A[i].Contains(Filter1) || Filter2 != null && A[i].Contains(Filter2))
                    return true;
            }
            return false;
        }

        public void ApplyFilter()
        {
            bool bFiltered = IsFiltered;
            keys.Clear();
            string Filter1 = FilterText.ToUpper();
            string Filter2 = TPuntoSwitcher.Change(FilterText);
            if (Filter2 != null) Filter2 = Filter2.ToUpper();
            foreach (var Pair in data)
            {
                string[] A = Pair.Value.GetStrings();
                if (!bFiltered || Pair.Value.Name == ".." || GoodForFilter(A, Filter1, Filter2))
                    keys.Add(Pair.Value.Name);
            }
        }

        public bool IsFiltered
        {
            get { return !String.IsNullOrEmpty(filter); }
        }

        // Возвращает количество компов в списке
        public int Count
        {
            get { return data.Count; }
        }

        // Возвращает число записей в фильтре
        public int FilterCount
        {
            get { return keys.Count; }
        }

        public String FilterText
        {
            get { return filter; }
            set
            {
                filter = value;
                ApplyFilter();
            }
        }

        public IList<string> Keys
        {
            get { return this.keys as IList<string>; }
        }
    }
}
