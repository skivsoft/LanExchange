using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using LanExchange.SDK;
using System.Diagnostics.Contracts;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Plugin.WinForms.Utils
{
    internal static class TranslationUtils
    {
        private static readonly IDictionary<Component, string> fieldsMap = new Dictionary<Component, string>();

        /// <summary>
        /// Recursive translation every control.
        /// </summary>
        /// <param name="controls"></param>
        internal static void TranslateControls(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is ITranslationable)
                    (control as ITranslationable).TranslateUI();
                else
                    TranslateControls(control.Controls);
            }
        }

        /// <summary>
        /// Translation of components. Menus mostly.
        /// </summary>
        /// <param name="resources"></param>
        /// <param name="instance"></param>
        /// <param name="components"></param>
        internal static void TranslateComponents(ResourceManager resources, ContainerControl instance, IContainer components)
        {
            Contract.Requires<ArgumentNullException>(resources != null);
            Contract.Requires<ArgumentNullException>(instance != null);
            Contract.Requires<ArgumentNullException>(components != null);

            fieldsMap.Clear();
            var fields = instance.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var field in fields)
            if (field.FieldType.IsSubclassOf(typeof(Component)))
            {
                var component = (Component) field.GetValue(instance);
                fieldsMap.Add(component, field.Name);
            }

            foreach (Component component in components.Components)
            {
                if (component is ITranslationable)
                    (component as ITranslationable).TranslateUI();
                else
                {
                    if (component is ContextMenuStrip)
                        ProcessContextMenuStrip(resources, component as ContextMenuStrip);
                    if (component is MainMenu)
                        ProcessMenuItems(resources, (component as MainMenu).MenuItems);
                }
            }
        }

        private static string GetComponentName(Component component)
        {
            string result;
            if (fieldsMap.TryGetValue(component, out result))
                return result;
            return null;
        }

        [Localizable(false)]
        private static void ProcessMenuItems(ResourceManager resources, Menu.MenuItemCollection menuItems)
        {
            foreach (MenuItem menuItem in menuItems)
                if (menuItem.Text != "-")
                {
                    var name = GetComponentName(menuItem);
                    if (name != null)
                        menuItem.Text = resources.GetString(name + "_Text");
                    if (menuItem.MenuItems.Count > 0)
                        ProcessMenuItems(resources, menuItem.MenuItems);
                }
        }

        private static void ProcessContextMenuStrip(ResourceManager resources, ContextMenuStrip menu)
        {
            foreach(var item in menu.Items)
            {
                var menuItem = item as ToolStripMenuItem;
                if (menuItem != null && !string.IsNullOrEmpty(menuItem.Name))
                    menuItem.Text = resources.GetString(menuItem.Name + "_Text");
            }
        }
    }
}