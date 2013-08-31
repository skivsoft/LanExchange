using System;

namespace LanExchange.SDK
{
    /// <summary>
    /// The single class for any root item of plugin.
    /// </summary>
    [Serializable]
    public sealed class PanelItemRoot : PanelItemBase
    {
        public static PanelItemBase ROOT_OF_USERITEMS = new PanelItemRoot();
    }
}