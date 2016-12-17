using System;
using System.Collections.Generic;
using System.Diagnostics;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Implementation
{
    internal sealed class PanelItemFactoryManager : IPanelItemFactoryManager
    {
        private readonly IPanelColumnManager columnManager;
        private readonly IDictionary<Type, IPanelItemFactory> types;
        private readonly List<PanelItemBase> defaultRoots;

        public PanelItemFactoryManager(IPanelColumnManager columnManager)
        {
            if (columnManager == null) throw new ArgumentNullException(nameof(columnManager));

            this.columnManager = columnManager;

            types = new Dictionary<Type, IPanelItemFactory>();
            defaultRoots = new List<PanelItemBase>();
        }

        public void RegisterFactory<TPanelItem>(IPanelItemFactory factory) where TPanelItem : PanelItemBase
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));

            types.Add(typeof(TPanelItem), factory);
            factory.RegisterColumns(columnManager);
        }

        public Func<PanelItemBase, bool> GetAvailabilityChecker(Type type)
        {
            IPanelItemFactory foundFactory;
            if (types.TryGetValue(type, out foundFactory))
                return foundFactory.GetAvailabilityChecker();
            return null;
        }

        public IDictionary<Type, IPanelItemFactory> Types
        {
            get { return types; }
        }

        public PanelItemBase CreateDefaultRoot(string typeName)
        {
            foreach (var pair in types)
                if (pair.Key.Name.Equals(typeName))
                    return pair.Value.CreateDefaultRoot();
            return null;
        }

        public void CreateDefaultRoots()
        {
            defaultRoots.Clear();
            foreach (var pair in types)
            try
            {
                var root = pair.Value.CreateDefaultRoot();
                if (root != null)
                    defaultRoots.Add(root);
                defaultRoots.Sort();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }

        public IList<PanelItemBase> DefaultRoots
        {
            get { return defaultRoots; }
        }

        public bool Exists(Type type)
        {
            IPanelItemFactory factory;
            return types.TryGetValue(type, out factory);
        }

        public Type[] ToArray()
        {
            var result = new Type[types.Count + 2];
            int i = 0;
            foreach (var key in types.Keys)
                result[i++] = key;
            result[types.Count] = typeof(PanelItemRootBase);
            result[types.Count + 1] = typeof(PanelItemDoubleDot);
            return result;
        }

        public bool IsEmpty
        {
            get { return types.Count == 0; }
        }
    }
}