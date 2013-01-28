using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.Sdk;

namespace LanExchange.Plugin
{
    public class Users : IPlugin
    {
        private IServiceProvider m_Provider;

        public void Initialize(IServiceProvider serviceProvider)
        {
            m_Provider = serviceProvider;
        }


        public void MainFormCreated()
        {
            if (m_Provider == null) return;

            var Info = m_Provider.GetService(typeof(IInfoView)) as IInfoView;
            if (Info == null) return;

            Info.Visible = false;
        }
    }
}
