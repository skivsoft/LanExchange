using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using PureInterfaces;

namespace LanExchange.SDK.SDKModel
{
    public interface IResourceProxy : IProxy
    {
        Image GetImage(string name);
        string GetText(string name);
        
        string CurrentLanguage { get; set; }
    }
}
