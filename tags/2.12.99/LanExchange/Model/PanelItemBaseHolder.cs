using System;
using System.Collections.Generic;
using LanExchange.SDK;

namespace LanExchange.Model
{
    [Serializable]
    public class PanelItemBaseHolder : List<PanelItemBase>
    {
        public string Context;
        public string DataType;
    }
}