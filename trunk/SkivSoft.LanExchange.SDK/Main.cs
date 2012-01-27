using System;
using System.IO;

//
// Note: all comments must be in English in this file
// 
namespace SkivSoft.LanExchange.SDK
{
    public interface IMLanEXInitializable
    {
        void Initialize(IServiceProvider serviceProvider);
    }
    
    public interface IMLanEXPlugin: IMLanEXInitializable
    {
        string Name { get; }
        string Version { get; }
        string Author { get; }
        string Description { get; }
        void DoIt();
    }

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
    public interface IMLanEXMainApp
    {
        /// <summary>
        /// Test method.
        /// </summary>
        void PrintKarrramba();

        /// <summary>
        /// Print text string to log.
        /// </summary>
        void LogPrint(string format, params object[] args);
        void LogPrint(Exception exception);
        void LoggerPrintEventHandlerAdd(LoggerPrintEventHandler handler);
        void LoggerPrintEventHandlerRemove(LoggerPrintEventHandler handler);
    }
}