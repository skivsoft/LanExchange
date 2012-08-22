using System;
using System.Collections.Generic;
using System.Text;
using PurePatterns;
using PureInterfaces;
using System.Windows.Forms;
using ViewWinForms.View.Forms;
using LanExchange.SDK.SDKView;

namespace ViewWinForms.View
{
    public class ApplicationMediator : Mediator, IApplicationMediator, IMediator
    {
        public new const string NAME = "ApplicationMediator";
        private MainForm m_MainForm;

        public ApplicationMediator()
            : base(NAME, null)
        {

        }

        public override void OnRegister()
        {
            base.OnRegister();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false); // must be called before first form created
            m_MainForm = new MainForm();
            if (m_MainForm != null)
            {
                Facade.RegisterMediator(new MainFormMediator(m_MainForm));
                Facade.RegisterMediator(new StatusViewMediator(m_MainForm.statusView1));
                Facade.RegisterMediator(new PanelViewMediator(m_MainForm.panelView1));
            }
        }

        public void Run()
        {
            Application.Run(m_MainForm);
        }

    }
}
