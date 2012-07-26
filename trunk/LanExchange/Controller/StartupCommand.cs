using System;
using System.Collections.Generic;
using System.Text;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using System.Windows.Forms;
using LanExchange.Model;
using LanExchange.View;

namespace LanExchange.Controller
{
    public class StartupCommand : SimpleCommand, ICommand
    {

        public override void Execute(INotification notification)
        {
            // network browser
            //Facade.RegisterProxy(new DomainProxy());
            Facade.RegisterProxy(new ComputerProxy());
            //Facade.RegisterProxy(new ResourceProxy());
            //Facade.RegisterProxy(new FileProxy());
            // AD browser
            //Facade.RegisterProxy(new UserProxy());
            // Person browser
            //Facade.RegisterProxy(new PersonProxy());

            MainForm form = (MainForm)notification.Body;
            if (form != null)
            {
                form.Text = "LanExchange 1.5";
                Facade.RegisterMediator(new PanelViewMediator(form.panelView1));
            }
        }
    }
}
