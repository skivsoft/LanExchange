using System;
using System.Collections.Generic;
using System.Text;
using Tools;
using SkivSoft.LanExchange.SDK;

namespace LanExchange
{
    public class TPanelItemList : ILanEXItemList
    {
        private SortedDictionary<string, IPanelItem> Data = null;
        private List<string> keys = null;
        private String Filter = "";

        public TPanelItemList()
        {
            Data = new SortedDictionary<string, IPanelItem>();
            keys = new List<string>();
        }
   
        public void Add(IPanelItem Comp)
        {
            if (Comp != null)
              if (!String.IsNullOrEmpty(Comp.Name))
                if (!Data.ContainsKey(Comp.Name))
                    Data.Add(Comp.Name, Comp);
        }

        public void Delete(IPanelItem Comp)
        {
            Data.Remove(Comp.Name);
        }

        public IPanelItem Get(string key)
        {
            IPanelItem Result = null;
            if (Data.TryGetValue(key, out Result))
            {
                Result.Name = key;
                return Result;
            }
            else
                return null;
        }

        public void Clear()
        {
            Data.Clear();
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
            foreach (var Pair in Data)
            {
                string[] A = Pair.Value.GetStrings();
                if (!bFiltered || Pair.Value.Name == ".." || GoodForFilter(A, Filter1, Filter2))
                    keys.Add(Pair.Value.Name);
            }
        }

        public bool IsFiltered
        {
            get { return !String.IsNullOrEmpty(Filter); }
        }

        // Возвращает количество компов в списке
        public int Count
        {
            get { return Data.Count; }
        }

        // Возвращает число записей в фильтре
        public int FilterCount
        {
            get { return keys.Count; }
        }

        public String FilterText
        {
            get { return Filter; }
            set
            {
                Filter = value;
                ApplyFilter();
            }
        }

        public IList<string> Keys
        {
            get
            {
                return this.keys as IList<string>;
            }
        }
    }
}
