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
    }
    
    public interface ILanEXForm : ILanEXControl
    {
        Rectangle Bounds { get; set; }
    }

    public interface ILanEXTabControl : ILanEXControl
    {
        int SelectedIndex { get; set; }
        IList<ILanEXTabPage> TabPages { get; }
        ILanEXTabPage SelectedTab { get; }
        int TabCount { get; }
    }

    public interface ILanEXTabPage : ILanEXControl
    {
        string Text { get; set; }
        bool IsListViewPresent { get; }
        ILanEXListView ListView { get; set;  }
    }

    public interface ILanEXListViewItem
    {
        string Text { get; }
    }

    public interface ILanEXListView : ILanEXControl
    {
        ILanEXListViewItem FocusedItem { get; set; }
        IList<ILanEXListViewItem> Items { get; }
        IList SelectedIndices { get; }
        int VirtualListSize { get; set; }
        int View { get; set; }
        
        void EnsureVisible(int Index);
        void Focus();
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
