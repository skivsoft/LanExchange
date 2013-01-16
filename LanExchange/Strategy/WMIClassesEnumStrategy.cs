using LanExchange.WMI;

namespace LanExchange.Strategy
{
    public class WMIClassesEnumStrategy : IBackgroundStrategy
    {
        public void Algorithm()
        {
            WMIClassList.Instance.EnumLocalMachineClasses();
        }
    }
}
