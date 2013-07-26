using System;
using System.Reflection;
using System.Collections.Generic;
using LanExchange.SDK;
using LanExchange.Utils;

namespace LanExchange
{
    public class ConcreteStrategySelector : IBackgroundStrategySelector
    {
        private readonly IList<IBackgroundStrategy> m_Strategies;

        public ConcreteStrategySelector()
        {
            m_Strategies = new List<IBackgroundStrategy>();
            
        }

        public PanelStrategyBase FindFirstAcceptedStrategy(ISubject subject, Type baseType)
        {
            foreach (var strategy in m_Strategies)
                if (strategy.GetType().BaseType == baseType)
                {
                    var panelStrategy = strategy as PanelStrategyBase;
                    if (panelStrategy == null) continue;
                    if (panelStrategy.IsSubjectAccepted(subject))
                        return panelStrategy;
                }
            return null;
        }

        public void SearchStrategiesInAssembly(Assembly asm, Type baseType)
        {
            foreach (var T in asm.GetTypes())
                if (T.IsClass && !T.IsAbstract && T.BaseType == baseType)
                try
                {
                    var strategy = (PanelStrategyBase)Activator.CreateInstance(T);
                    m_Strategies.Add(strategy);
                }
                catch (Exception E)
                {
                    LogUtils.Error("SearchStrategies: {0} {1}\n{2}", T, E.Message, E.StackTrace);
                }
        }

        public void RegisterBackgroundStrategy(IBackgroundStrategy strategy)
        {
            m_Strategies.Add(strategy);
        }
    }
}
