using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.Model;
using LanExchange.OSLayer;
using LanExchange.Model.VO;

namespace LanExchange.Demodata
{
    public class DemoComputerEnumStrategy : IEnumObjectsStrategy
    {
        public IList<LanExchange.Model.VO.PanelItemVO> EnumObjects(string path)
        {
            List<PanelItemVO> Result = new List<PanelItemVO>();
            NetApi32.SERVER_INFO_101 info = new NetApi32.SERVER_INFO_101();
            info.sv101_name = "TEST";
            info.sv101_comment = "qqq w w";
            Result.Add(new ComputerVO("TEST", new ServerInfoVO(info)));
            return Result;
        }
    }
}
