using LanExchange.SDK;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace LanExchange.Plugin.Win7
{
    public class Win7 : IPlugin
    {
        public void Initialize(System.IServiceProvider serviceProvider)
        {
            if (TaskbarManager.IsPlatformSupported)
            {
                
                
            }
        }
    }
}
