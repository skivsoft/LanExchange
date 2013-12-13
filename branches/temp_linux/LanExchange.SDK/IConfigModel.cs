using System;

namespace LanExchange.SDK
{
    public interface IConfigModel
    {
        event EventHandler<ConfigChangedArgs> Changed;

        bool ShowInfoPanel { get; set; }
        bool ShowGridLines { get; set; }
        bool RunMinimized { get; set; }
        bool AdvancedMode { get; set; }
        int NumInfoLines { get; set; }
        int MainFormX { get; set; }
        int MainFormWidth { get; set; }
        string Language { get; set; }

        void Load();
        void Save();
    }
}