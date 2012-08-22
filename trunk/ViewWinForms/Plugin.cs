using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.SDK;
using PureInterfaces;
using LanExchange.SDK.SDKModel;
using PurePatterns;
using ViewWinForms.View;

namespace ViewWinForms
{
    [Plugin(Description="Default user interface based on WinForms", Version="1.0", Author="Skiv")]
    public class ViewWinFormsPlugin : IPlugin
    {
        public void Initialize(IFacade facade)
        {
            Globals.Facade = facade;

            if (facade != null)
            {
                facade.RegisterMediator(new ApplicationMediator());
            }
        }
    }
}
