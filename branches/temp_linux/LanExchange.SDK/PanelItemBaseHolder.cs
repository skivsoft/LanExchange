using System;
using System.Collections.Generic;

namespace LanExchange.SDK
{
    [Serializable]
    public class PanelItemBaseHolder : List<PanelItemBase>
    {
        public string Context;
        public string DataType;
    }
}