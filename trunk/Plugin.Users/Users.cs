using System;
using LanExchange.Plugin.Properties;
using LanExchange.Sdk;

namespace LanExchange.Plugin
{
    public class Users : IPlugin
    {
        private static IServiceProvider m_Provider;
        private IBackgroundStrategySelector m_StrategySelector;

        public void Initialize(IServiceProvider serviceProvider)
        {
            m_Provider = serviceProvider;

            // get strategy selector
            m_StrategySelector = m_Provider.GetService(typeof(IBackgroundStrategySelector)) as IBackgroundStrategySelector;
            if (m_StrategySelector == null) return;

            // create and register our enum strategy in it
            m_StrategySelector.RegisterBackgroundStrategy(new UserEnumStrategy());
        }

        //public static IServiceProvider Provider
        //{
        //    get { return m_Provider; }
        //}

        public void MainFormCreated()
        {
            if (m_Provider == null) return;
            // test hiding top panel
            //var Info = m_Provider.GetService(typeof(IInfoView)) as IInfoView;
            //if (Info == null) return;
            //Info.Visible = false;
        }
    }
}
