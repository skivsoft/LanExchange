using System.ComponentModel;
using System.Windows.Forms;
using LanExchange.SDK;

namespace LanExchange.Utils
{
    public static class TranslationUtils
    {
        [Localizable(false)]
        public static void ApplyResources(ComponentResourceManager resources, IContainer container)
        {
            foreach (Component component in container.Components)
            {
                if (component is ITranslationable)
                    (component as ITranslationable).ApplyResources();
                else
                {
                    var name = ReflectionUtils.GetObjectProperty<string>(component, "Name");
                    if (name != null)
                        resources.ApplyResources(component, name);
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
    }
}
