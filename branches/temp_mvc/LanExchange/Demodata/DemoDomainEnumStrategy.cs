using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.Model;
using LanExchange.Model.VO;

namespace LanExchange.Demodata
{
    public class DemoDomainEnumStrategy : IEnumObjectsStrategy
    {
        public IList<LanExchange.Model.VO.PanelItemVO> EnumObjects(string path)
        {
            List<PanelItemVO> Result = new List<PanelItemVO>();
            Result.Add(new DomainVO("MONDAY", "MON"));
            Result.Add(new DomainVO("TUESDAY", "TUE"));
            Result.Add(new DomainVO("WEDNESDAY", "WED"));
            Result.Add(new DomainVO("THURSDAY", "THU"));
            Result.Add(new DomainVO("FRIDAY", "FRI"));
            Result.Add(new DomainVO("SATURDAY", "SAT"));
            Result.Add(new DomainVO("SUNDAY", "SUN"));
            return Result;
        }
    }
}
