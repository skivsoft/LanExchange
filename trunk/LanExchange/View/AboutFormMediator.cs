using System;
using System.Collections.Generic;
using System.Text;
using LanExchange;
using LanExchange.View;
using LanExchange.View.Forms;
using PureMVC.PureInterfaces;
using PureMVC.PurePatterns;

namespace LanExchange.View
{
    public class AboutFormMediator : Mediator, IFormMediator, IMediator
    {
        public new const string NAME = "AboutFormMediator";

        public AboutFormMediator() : base(NAME, null)
        {

        }

        public void ShowDialog()
        {
            using (AboutForm form = new AboutForm())
            {
                form.Text = String.Format(AppFacade.T("AboutForm.Text"), form.ProductName);
                if (AppFacade.Resources != null)
                {
                    form.picLogo.Image = AppFacade.Resources.GetImage("logo_icon");
                }
                form.ShowDialog();
            }
        }
    }
}
