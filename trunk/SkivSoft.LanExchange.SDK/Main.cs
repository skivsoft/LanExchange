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
        /// <summary>
        /// This method calls when main form were loaded.
        /// </summary>
    }

    #region IPanelItem Interface
    public interface IPanelItem
    {
        string Name { get; set; }
        string Comment { get; set; }
        int ImageIndex { get; }
        string ToolTipText { get; }

        string[] GetStrings();
        string[] GetSubItems();
        void CopyExtraFrom(IPanelItem Comp);
    }
    #endregion

    #region ILanEXSettings Interface
    public interface ILanEXSettings
    {


    }
    #endregion

    #region ILanEXTabController Interface
    public interface ILanEXTabController
    {

    }
    #endregion


    #region ILanEXMainApp Interface
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
        /// Print log event handler.
        /// </summary>
        event LoggerPrintEventHandler LoggerPrint;
        /// <summary>
        /// This event occurs after main form were loaded.
        /// </summary>
        event EventHandler Loaded;
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

        ILanEXComponent CreateComponent(Type type);
        ILanEXControl CreateControl(Type type);
        void ListView_SetupTip(ILanEXListView LV);
        void ListView_Setup(ILanEXListView LV);
        void ListView_Update(ILanEXListView LV);
        string InputBoxAsk(string caption, string prompt, string defText);
    }
    #endregion
}