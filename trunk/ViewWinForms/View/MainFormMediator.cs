using System;
using System.Collections.Generic;
using System.Text;
using PurePatterns;
using PureInterfaces;
using System.Windows.Forms;
using LanExchange.SDK.SDKView;

namespace ViewWinForms.View
{
    public class MainFormMediator : Mediator, IFormMediator, IMediator
    {
        public new const string NAME = "MainFormMediator";
        private Form m_Component = null;

        public MainFormMediator(Form form) : base(NAME, form)
        {
            m_Component = form;
        }

        public void ShowDialog()
        {
            // do nothing
        }
    }
}
