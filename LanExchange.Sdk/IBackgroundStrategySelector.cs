using System;
using System.Reflection;

namespace LanExchange.Sdk
{
    /// <summary>
    /// Selector for suitable background strategy.
    /// </summary>
    public interface IBackgroundStrategySelector
    {
        /// <summary>
        /// Registers the background strategy.
        /// </summary>
        /// <param name="strategy">The strategy.</param>
        void RegisterBackgroundStrategy(IBackgroundStrategy strategy);

        /// <summary>
        /// Searches the strategies in assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="baseType">Type of the base.</param>
        void SearchStrategiesInAssembly(Assembly assembly, Type baseType);

        /// <summary>
        /// Finds the first accepted strategy.
        /// </summary>
        /// <param name="subject">The subject.</param>
        /// <param name="baseType">Type of the base.</param>
        /// <returns></returns>
        PanelStrategyBase FindFirstAcceptedStrategy(ISubject subject, Type baseType);
    }
}
