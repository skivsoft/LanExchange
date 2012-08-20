using System;
using System.Collections.Generic;
using System.Text;
using PurePatterns;
using PureInterfaces;
using System.Windows.Forms;
using ViewWinForms.View.Forms;

namespace ViewWinForms.View
{
    public class ApplicationMediator : Mediator, IMediator
    {
        public new const string NAME = "ApplicationMediator";

        public ApplicationMediator()
            : base(NAME, null)
        {

        }

        public override void OnRegister()
        {
            base.OnRegister();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false); // must be called before first form created
            MainForm form = new MainForm();
            if (form != null)
            {
                Facade.RegisterMediator(new MainFormMediator(form));
                Facade.RegisterMediator(new StatusViewMediator(form.statusView1));
                Facade.RegisterMediator(new PanelViewMediator(form.panelView1));
            }
            Application.Run(form);
        }

    }
}
