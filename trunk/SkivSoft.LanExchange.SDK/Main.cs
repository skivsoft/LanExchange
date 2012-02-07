using System;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.Win32;

//
// Note: all comments must be in English in this file
// 
namespace SkivSoft.LanExchange.SDK
{
    /// <summary>
    /// Init with service provider.
    /// </summary>
    public interface ILanEXInitializable
    {
        void Initialize(IServiceProvider serviceProvider);
    }
    
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

    #region TPanelItem Model
    public abstract class TPanelItem : IComparable<TPanelItem>
    {
        protected abstract string GetComment();
        protected abstract void SetComment(string value);
        protected abstract string GetName();
        protected abstract void SetName(string value);

        protected virtual int GetImageIndex()
        {
            return 0;
        }

        protected virtual string GetToolTipText()
        {
            return Comment;
        }

        public virtual string[] getStrings()
        {
            return new string[2] { Name.ToUpper(), Comment.ToUpper() };
        }

        public virtual string[] GetSubItems()
        {
            return new string[1] { Comment };
        }

        public virtual void CopyExtraFrom(TPanelItem Comp)
        {
            // empty for base class
        }

        public string Name
        {
            get { return GetName(); }
            set { SetName(value); }
        }

        public string Comment
        {
            get { return GetComment(); }
            set { SetComment(value); }
        }

        public int ImageIndex
        {
            get { return GetImageIndex(); }
        }

        public string ToolTipText
        {
            get { return GetToolTipText(); }
        }

        public int CompareTo(TPanelItem p2)
        {
            return Name.CompareTo(p2.Name);
        }
    }

    public class TPanelItemComparer : IComparer<TPanelItem>
    {
        public int Compare(TPanelItem Item1, TPanelItem Item2)
        {
            if (Item1.Name == "..")
                if (Item2.Name == "..")
                    return 0;
                else
                    return -1;
            else
                return Item1.CompareTo(Item2);
        }
    }
    #endregion

    #region TSettings Interface
    public interface ILanEXSettings
    {


    }
    #endregion

    #region TTabController Interface
    public interface ILanEXTabController
    {

    }
    #endregion


    #region TMainApp Interface
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

    /// <summary>
    /// Main Application class LanExchange.
    /// </summary>
    public interface ILanEXMainApp
    {
        /// <summary>
        /// Print text string to log.
        /// </summary>
        void LogPrint(string format, params object[] args);
        void LogPrint(Exception exception);
        /// <summary>
        /// Add/Remove event handlers for LogPrint calls.
        /// </summary>
        /// <param name="handler"></param>
        void LoggerPrintEventHandlerAdd(LoggerPrintEventHandler handler);
        void LoggerPrintEventHandlerRemove(LoggerPrintEventHandler handler);
        /// <summary>
        /// Returns current computer name.
        /// </summary>
        string ComputerName { get; }
        /// <summary>
        /// Returns current user name.
        /// </summary>
        string UserName { get; }

        ILanEXForm MainForm { get; }

        ILanEXControl CreateControl(Type type);
        void ListView_SetupTip(ILanEXListView LV);
        void ListView_Setup(ILanEXListView LV);
        void ListView_Update(ILanEXListView LV);
        string InputBoxAsk(string caption, string prompt, string defText);
    }
    #endregion
}