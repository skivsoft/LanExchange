using System.ComponentModel;
using System.Resources;
using System.Windows.Forms;
using LanExchange.Intf;
using LanExchange.SDK;

namespace LanExchange.Utils
{
    public static class TranslationUtils
    {
        [Localizable(false)]
        public static void ApplyResources(ResourceManager resources, IContainer container)
        {
            foreach (Component component in container.Components)
            {
                if (component is ITranslationable)
                    (component as ITranslationable).ApplyResources();
                else
                {
                    if (component is ContextMenuStrip)
                        ApplyResourcesToContextMenuStrip(resources, component as ContextMenuStrip);
                    //var name = ReflectionUtils.GetObjectProperty<string>(component, "Name");
                    //if (name != null)
                    //    resources.ApplyResources(component, name);
                }
            }
        }

        public static void ApplyResources(ComponentResourceManager resources, Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is ITranslationable)
                    (control as ITranslationable).ApplyResources();
                else
                {
                    resources.ApplyResources(control, control.Name);
                    ApplyResources(resources, control.Controls);
                }
            }
        }

        private static void ApplyResourcesToContextMenuStrip(ResourceManager resources, ContextMenuStrip menu)
        {
            foreach(var item in menu.Items)
            {
                var menuItem = item as ToolStripMenuItem;
                if (menuItem != null)
                    menuItem.Text = resources.GetString(menuItem.Name + "_Text");
            }
            
        }
    }
}
