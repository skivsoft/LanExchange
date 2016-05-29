namespace LanExchange.Presentation.Interfaces
{
    public interface ISystemInformationService
    {
        string ComputerName { get; }
        string UserDomainName { get; }
        string UserName { get; }
    }
}