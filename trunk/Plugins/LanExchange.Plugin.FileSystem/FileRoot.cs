using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.SDK;

namespace LanExchange.Plugin.FileSystem
{
    internal class FileRoot : PanelItemRootBase
    {
        protected override string GetName()
        {
            return "Computer";
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
