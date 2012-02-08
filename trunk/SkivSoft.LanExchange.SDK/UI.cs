using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections;

namespace SkivSoft.LanExchange.SDK
{
    /// <summary>
    /// Simple and empty base class for non-controls.
    /// </summary>
    public interface ILanEXComponent
    {
    }

    public interface ILanEXItemList : ILanEXComponent
    {
        bool IsFiltered { get; }
        int Count { get; }
        int FilterCount { get; }
        String FilterText { get; set; }

        void Add(ILanEXItem Comp);
        void Delete(ILanEXItem Comp);
        ILanEXItem Get(string key);
        void Clear();
        void ApplyFilter();

        IList<string> Keys { get; }
    }

    public interface ILanEXListViewItem : ILanEXComponent
    {
        string Text { get; set; }
    }

    public interface ILanEXMenuItem : ILanEXComponent
    {
        event EventHandler Click;
        object Tag { get; set; }
        bool Checked { get; set; }
        string Text { get; set; }
        IList DropDownItems { get; }
    }

    public interface ILanEXControl : IDisposable
    {
        string Name { get; set; }
        Rectangle Bounds { get; set; }
        
        void Focus();
        void Add(ILanEXControl child);
    }
    
    public interface ILanEXListView : ILanEXControl
    {
        ILanEXListViewItem FocusedItem { get; set; }
        IList<int> SelectedIndices { get; }
        int VirtualListSize { get; set; }
        int View { get; set; }
        int ItemsCount { get; }
        ILanEXItemList ItemList { get; set; }

        ILanEXListViewItem GetItem(int Index);
        void EnsureVisible(int Index);

        List<string> GetSelected(bool bAll);
        void SetSelected(List<string> SaveSelected);
        void SelectComputer(string CompName);
    }

    public interface ILanEXTabPage : ILanEXControl
    {
        string Text { get; set; }
        bool IsListViewPresent { get; }
        ILanEXListView ListView { get; set; }
    }

    public interface ILanEXTabControl : ILanEXControl
    {
        int SelectedIndex { get; set; }
        ILanEXTabPage SelectedTab { get; }
        int TabCount { get; }
        ILanEXTabPage GetPage(int Index);
        void RemoveAt(int Index);
    }

    public interface ILanEXStatusStrip : ILanEXControl
    {
        void SetText(int Index, string Text);
    }

}
