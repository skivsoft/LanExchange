using System;
using System.Collections.Generic;

namespace LanExchange.SDK
{
    [Serializable]
    public sealed class PanelItemBaseHolder : List<PanelItemBase>
    {
        public PanelItemBaseHolder(string context, string dataType)
        {
            Context = context;
            DataType = dataType;
        }

        public string Context { get; private set; }
        public string DataType { get; private set; }
    }
}