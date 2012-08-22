using System;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.Win32;

//
// Note: all comments must be in English in this file
// 
namespace SkivSoft.LanExchange.SDK
{
    #region ILanEXInitializable interface
    /// <summary>
    /// Init with service provider.
    /// </summary>
    public interface ILanEXInitializable
    {
        void Initialize(IServiceProvider serviceProvider);
    }
    #endregion

    #region ILanEXPlugin interface
    /// <summary>
    /// LanExchange plugin methods. 
    /// Plugin is initializable with serivice provider.
    /// </summary>
    public interface ILanEXPlugin : ILanEXInitializable
    {
        string Name { get; }
        string Version { get; }
        string Author { get; }
        string Description { get; }
    }
    #endregion

    #region ILanEXItem interface
    public struct TLanEXColumnInfo
    {
        string Name;
        int Width;

        public TLanEXColumnInfo(string NewName, int NewWidth)
        {
            this.Name = NewName;
            this.Width = NewWidth;
        }
    }

    public interface ILanEXItem
    {
        string Name { get; set; }
        string Comment { get; set; }
        int ImageIndex { get; }
        string ToolTipText { get; }

        //List<ILanEXItem> GetItems();

        string[] GetStrings();
        string[] GetSubItems();
        TLanEXColumnInfo[] GetColumns();
        void CopyExtraFrom(ILanEXItem Comp);
    }
    #endregion

    #region ILanEXSettings Interface
    public interface ILanEXSettings
    {


    }
    #endregion

    #region LoggerPrintEventHandler & LoggerPrintEventArgs
    /// <summary>
    /// LogPrint arguments.
    /// </summary>
    public class LoggerPrintEventArgs : EventArgs
    {
        private string text;

        public LoggerPrintEventArgs(string Text)
        {
            this.text = Text;
        }

        public string Text { get { return this.text; } }
    }
    
    public delegate void LoggerPrintEventHandler(object sender, LoggerPrintEventArgs e);
    #endregion

    #region ILanEXMainApp interface
    /// <summary>
    /// Main Application class LanExchange.
    /// </summary>
    public interface ILanEXMainApp
    {
        /// <summary>
        /// Print log event handler.
        /// </summary>
        event LoggerPrintEventHandler LoggerPrint;
        /// <summary>
        /// This event occurs after main form were loaded.
        /// </summary>
        event EventHandler Loaded;
        /// <summary>
        /// This event calls by main refresh timer.
        /// </summary>
        event EventHandler UpdateItemList;
        
        /// <summary>
        /// Returns current computer name.
        /// </summary>
        string ComputerName { get; }
        /// <summary>
        /// Returns current user name.
        /// </summary>
        string UserName { get; }
        ILanEXControl MainForm { get; }
        ILanEXTabControl Pages { get; }
        ILanEXStatusStrip StatusStrip { get; }
        ILanEXTabController TabController { get; }

        /// <summary>
        /// Print text string to log.
        /// </summary>
        void LogPrint(string format, params object[] args);
        /// <summary>
        /// Print Exception to log.
        /// </summary>
        /// <param name="exception">An Exception.</param>
        void LogPrint(Exception exception);
        void ListView_SetupTip(ILanEXListView LV);
        void ListView_Setup(ILanEXListView LV);
        void ListView_Update(ILanEXListView LV);

        int RegisterImageIndex(Bitmap pic16x16, Bitmap pic32x32);
        ILanEXComponent CreateComponent(Type type);
        ILanEXControl CreateControl(Type type);
        string InputBoxAsk(string caption, string prompt, string defText);
    }
    #endregion
}