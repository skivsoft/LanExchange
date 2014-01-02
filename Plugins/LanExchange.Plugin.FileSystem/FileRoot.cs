using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using LanExchange.Plugin.FileSystem.Properties;
using LanExchange.SDK;

namespace LanExchange.Plugin.FileSystem
{
    public class FileRoot : PanelItemRootBase
    {
        protected override string GetName()
        {
            return Resources.Computer;
        }

        public override string ImageName
        {
            get { return PanelImageNames.COMPUTER; }
        }

        public override object Clone()
        {
            return new FileRoot();
        }
    }
}
