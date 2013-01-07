using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using LanExchange;
using LanExchange.View;
using PureMVC.PureInterfaces;
using PureMVC.PurePatterns;
using LanExchange.View.Forms;

namespace LanExchange.View
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

            if (AppFacade.Resources != null)
            {
                AppFacade.LocalizeControl(m_Form);
            }
        }

        public void ShowDialog()
        {
            // do nothing
        }
    }
}
