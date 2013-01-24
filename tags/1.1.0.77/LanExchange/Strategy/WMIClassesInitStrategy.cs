using LanExchange.WMI;

namespace LanExchange.Strategy
{
    public class WMIClassesInitStrategy : IBackgroundStrategy
    {
        public void Algorithm()
        {
            WMIClassList.Instance.EnumLocalMachineClasses();
        }
    }
}
