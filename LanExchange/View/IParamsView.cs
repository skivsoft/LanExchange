namespace LanExchange.View
{
    public interface IParamsView
    {
        bool IsAutorun { get; set; }
        bool RunMinimized { get; set; }
        bool AdvancedMode { get; set; }
        decimal RefreshTimeInMin { get; set; }
    }
}
