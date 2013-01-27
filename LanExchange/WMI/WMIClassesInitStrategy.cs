using LanExchange.Model.Strategy;
using LanExchange.Sdk;

namespace LanExchange.WMI
{
    public class WMIClassesInitStrategy : IBackgroundStrategy
    {
        public void Algorithm()
        {
            WMIClassList.Instance.EnumLocalMachineClasses();
        }
    }
}
