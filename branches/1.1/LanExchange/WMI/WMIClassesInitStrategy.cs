using LanExchange.Model.Strategy;

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
