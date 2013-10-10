namespace LanTabs
{
    public static class ContainerBuilder
    {
        /// <summary>
        /// Maps interfaces to concrete implementations.
        /// </summary>
        public static IIoCContainer Build()
        {
            var container = new SimpleIocContainer();
            container.Register<IPagesView, PagesView>(LifeCycle.Transient);
            container.Register<IPageListView, PageListView>(LifeCycle.Transient);
            container.Register<IPageTreeView, PageTreeView>(LifeCycle.Transient);
            container.Register<IPageHomeView, PageHomeView>(LifeCycle.Transient);
            container.Register<IPagesPresenter, PagesPresenter>(LifeCycle.Singleton);
            return container;
        }
    }
}
