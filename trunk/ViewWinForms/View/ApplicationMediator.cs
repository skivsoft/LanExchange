using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using LanExchange.View;
using PureMVC.PureInterfaces;
using PureMVC.PurePatterns;
using ViewWinForms.View.Forms;

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
                Facade.RegisterMediator(new AboutFormMediator());
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
