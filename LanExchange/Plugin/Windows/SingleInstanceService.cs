using LanExchange.Plugin.Windows.Utils;
using LanExchange.SDK;

namespace LanExchange.Plugin.Windows
{
    internal class SingleInstanceService : ISingleInstanceService
    {
        public bool CheckExists(string unicalName)
        {
            return SingleInstanceCheck.CheckExists(unicalName);
        }
    }
}
