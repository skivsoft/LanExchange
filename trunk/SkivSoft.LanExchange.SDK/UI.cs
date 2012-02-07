using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections;

namespace SkivSoft.LanExchange.SDK
{
    public interface ILanEXControl : IDisposable
    {
        object Tag { get; set; }
        string Name { get; set; }
        event EventHandler Click;
        void Add(ILanEXControl childControl);
        void Focus();
        void BringToFront();
        void SendToBack();
    }
    
    public interface ILanEXForm : ILanEXControl
    {
        Rectangle Bounds { get; set; }
    }

    public interface ILanEXListViewItem
    {
        string Text { get; }
    }

    public interface ILanEXListView : ILanEXControl
    {
        ILanEXListViewItem FocusedItem { get; set; }
        IList<int> SelectedIndices { get; }
        int VirtualListSize { get; set; }
        int View { get; set; }

        int ItemsCount { get; }
        ILanEXListViewItem GetItem(int Index);
        void EnsureVisible(int Index);
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

    public interface ILanEXFilterPanel : ILanEXControl
    {

    }

    public interface ILanEXMenuItem : ILanEXControl
    {
        bool Checked { get; set; }
        string Text { get; set; }
        IList DropDownItems { get; }
    }

    public interface ILanEXSaveFileDialog : ILanEXControl
    {

    }
}
