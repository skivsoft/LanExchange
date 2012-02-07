using System;
using System.Collections.Generic;
using System.Text;
using Tools;
using SkivSoft.LanExchange.SDK;

namespace LanExchange
{
    public class TPanelItemList 
    {
        private SortedDictionary<string, TPanelItem> Data = null;
        public List<string> Keys = null;
        private String Filter = "";

        public TPanelItemList()
        {
            Data = new SortedDictionary<string, TPanelItem>();
            Keys = new List<string>();
        }
   
        public void Add(TPanelItem Comp)
        {
            if (Comp != null)
              if (!String.IsNullOrEmpty(Comp.Name))
                if (!Data.ContainsKey(Comp.Name))
                    Data.Add(Comp.Name, Comp);
        }

        public void Delete(TPanelItem Comp)
        {
            Data.Remove(Comp.Name);
        }

        public TPanelItem Get(string key)
        {
            TPanelItem Result = null;
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
            Keys.Clear();
            string Filter1 = FilterText.ToUpper();
            string Filter2 = TPuntoSwitcher.Change(FilterText);
            if (Filter2 != null) Filter2 = Filter2.ToUpper();
            foreach (var Pair in Data)
            {
                string[] A = Pair.Value.getStrings();
                if (!bFiltered || Pair.Value.Name == ".." || GoodForFilter(A, Filter1, Filter2))
                    Keys.Add(Pair.Value.Name);
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
            get { return Keys.Count; }
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

        public List<string> ListView_GetSelected(ILanEXListView LV, bool bAll)
        {
            List<string> Result = new List<string>();
            if (LV.FocusedItem != null)
                Result.Add(LV.FocusedItem.Text);
            else
                Result.Add("");
            if (bAll)
                for (int index = 0; index < LV.ItemsCount; index++)
                    Result.Add(Keys[index]);
            else
                foreach (int index in LV.SelectedIndices)
                    Result.Add(Keys[index]);
            return Result;
        }
        
        public void ListView_SetSelected(ILanEXListView LV, List<string> SaveSelected)
        {
            LV.SelectedIndices.Clear();
            LV.FocusedItem = null;
            if (LV.VirtualListSize > 0)
            {
                for (int i = 0; i < SaveSelected.Count; i++)
                {
                    int index = Keys.IndexOf(SaveSelected[i]);
                    if (index == -1) continue;
                    if (i == 0)
                    {
                        LV.FocusedItem = (ILanEXListViewItem)LV.GetItem(index);
                        LV.EnsureVisible(index);
                    }
                    else
                        LV.SelectedIndices.Add(index);
                }
            }
        }

        // <summary>
        // Выбор компьютера по имени в списке.
        // </summary>
        public void ListView_SelectComputer(ILanEXListView LV, string CompName)
        {
            int index = -1;
            // пробуем найти запомненный элемент
            if (CompName != null)
            {
                index = this.Keys.IndexOf(CompName);
                if (index == -1) index = 0;
            }
            else
                index = 0;
            // установка текущего элемента
            if (LV.VirtualListSize > 0)
            {
                LV.SelectedIndices.Add(index);
                LV.FocusedItem = (ILanEXListViewItem)LV.GetItem(index);
                LV.EnsureVisible(index);
            }
        }

        #region Привязка своего обьекта к ILanEXListView

        public static void ListView_SetObject(ILanEXListView LV, TPanelItemList ItemList)
        {
            LV.Tag = ItemList;
        }

        public static TPanelItemList ListView_GetObject(ILanEXListView LV)
        {
            return LV.Tag as TPanelItemList;
        }

        public static TPanelItemList ListView_CreateObject(ILanEXListView LV)
        {
            TPanelItemList Result = new TPanelItemList();
            ListView_SetObject(LV, Result);
            return Result;
        }

        public static void ListView_DeleteObject(ILanEXListView LV)
        {
            TPanelItemList List = ListView_GetObject(LV);
            if (List != null)
                ListView_SetObject(LV, null);
        }
        #endregion

        public List<string> ToList()
        {
            List<string> Result = new List<string>();
            foreach (var Pair in Data)
                Result.Add(Pair.Value.Name);
            return Result;
        }
    }
}
