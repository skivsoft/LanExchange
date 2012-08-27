using System;
using System.Collections.Generic;
using System.Text;
using LanExchange;
using LanExchange.View;
using PureMVC.PureInterfaces;
using PureMVC.PurePatterns;
using ViewWinForms.View.Forms;

namespace ViewWinForms.View
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
                form.Text = String.Format(Globals.T("AboutForm.Text"), form.ProductName);
                if (Globals.Resources != null)
                {
                    form.picLogo.Image = Globals.Resources.GetImage("logo_icon");
                }
                form.ShowDialog();
            }
        }
    }
}
