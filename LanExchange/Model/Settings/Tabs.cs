using System.Collections.Generic;
using LanExchange.Intf;

namespace LanExchange.Model.Settings
{
    public class Tabs
    {
        public int SelectedIndex { get; set; }
        public Tab[] Items { get; set; }

        public Tabs()
        {
            Items = new Tab[0];
        }

        internal void GetFromModel(IPagesModel model)
        {
            SelectedIndex = SelectedIndex;
            var pages = new List<Tab>();
            for (int i = 0; i < model.Count; i++)
                pages.Add(model.GetItem(i).Settings);
            Items = pages.ToArray();
        }

        internal void SetToModel(IPagesModel model)
        {
            //App.PanelItemTypes.CreateDefaultRoots();
            //if (m_PagesSettings.Items.Length > 0)
            //    // add loaded tabs if present
            //    foreach(var page in m_PagesSettings.Items)
            //    {
            //        var info = App.Resolve<IPanelModel>();
            //        info.Settings = page;
            //        AddTab(info);
            //    }
            //else
            // create default tabs
            //foreach (var root in App.PanelItemTypes.DefaultRoots)
            //{
            //    var info = App.Resolve<IPanelModel>();
            //    info.TabName = root.Name;
            //    info.SetDefaultRoot(root);
            //    model.AddTab(info);
            //}
            //if (SelectedIndex != -1)
            //    model.SelectedIndex = SelectedIndex;
        }
    }
}