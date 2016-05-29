namespace LanExchange.Presentation.Interfaces
{
    public interface IEditView : IWindow
    {
        //TODO: hide model
        //void SetColumns(IList<PanelColumnHeader> columns);

        bool ShowModal();
    }
}
