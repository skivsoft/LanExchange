using System.Windows.Forms;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Presentation.WinForms
{
    internal sealed class SystemInformationService : ISystemInformationService
    {
        public string ComputerName => SystemInformation.ComputerName;

        public string UserDomainName => SystemInformation.UserDomainName;

        public string UserName => SystemInformation.UserName;
    }
}
