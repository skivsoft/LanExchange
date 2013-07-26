using LanExchange.SDK;

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
