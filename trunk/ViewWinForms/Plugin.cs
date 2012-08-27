using System;
using System.Collections.Generic;
using System.Text;
using LanExchange;
using LanExchange.Model;
using PureMVC.PureInterfaces;
using PureMVC.PurePatterns;
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
