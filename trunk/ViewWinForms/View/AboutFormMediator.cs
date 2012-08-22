using System;
using System.Collections.Generic;
using System.Text;
using PurePatterns;
using LanExchange.SDK.SDKView;
using PureInterfaces;
using ViewWinForms.View.Forms;
using LanExchange.SDK;

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
                if (Globals.Resources != null)
                {
                    form.picLogo.Image = Globals.Resources.GetImage("logo_icon");
                }
                form.ShowDialog();
            }
        }
    }
}
