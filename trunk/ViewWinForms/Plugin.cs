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
    [Plugin(Description="Default user interface", Version="1.0", Author="Skiv")]
    public class ViewWinFormsPlugin : IPlugin
    {
        public IFacade AppFacade = null;

        public void Initialize(IFacade facade)
        {
            AppFacade = facade;

            if (AppFacade != null)
            {
                AppFacade.RegisterMediator(new ApplicationMediator());
            }
        }
    }
}
