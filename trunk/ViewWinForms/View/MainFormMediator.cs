using System;
using System.Collections.Generic;
using System.Text;
using PurePatterns;
using PureInterfaces;
using System.Windows.Forms;
using LanExchange.SDK.SDKView;
using LanExchange.SDK;
using ViewWinForms.View.Forms;
using System.Reflection;
using System.Drawing;

namespace ViewWinForms.View
{
    public class MainFormMediator : Mediator, IFormMediator, IMediator
    {
        public new const string NAME = "MainFormMediator";
        private MainForm m_Form = null;

        public MainFormMediator(MainForm form) : base(NAME, form)
        {
            m_Form = form;
        }

        public override void OnRegister()
        {
            base.OnRegister();

            if (Globals.Resources != null)
            {
                m_Form.picLogo.Image = Globals.Resources.GetImage("logo_icon");
                Localization.ProcessControl(m_Form);
            }
        }

        public void ShowDialog()
        {
            // do nothing
        }
    }
}
