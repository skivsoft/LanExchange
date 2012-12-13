#define __NET_3_5
// <copyright file="Extensions.cs" company="Brown University">
// Copyright (c) 2009 by John Mertus
// </copyright>
// <author>John Mertus</author>
// <date>10/31/2009 9:30:22 AM</date>
// <summary></summary>

using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
#if NET_3_5
    using System.Linq;
#endif

namespace LanExchange
{

    /// <summary>
    /// This is a set of extensions for accessing the Event Handlers as well as cloning menu items
    /// </summary>
    public static class MenuUtils
    {
        //////////////////////////////////////////////////
        // Private static fields
        //////////////////////////////////////////////////
        #region Public static methods

        /// <summary>
        /// This contains a counter to help make names unique
        /// </summary>
        private static int menuNameCounter;

        #endregion

        //////////////////////////////////////////////////
        // Public static methods
        //////////////////////////////////////////////////
        #region Public static methods

        /// <summary>
        /// Clones the specified source tool strip menu item. 
        /// </summary>
        /// <param name="sourceToolStripMenuItem">The source tool strip menu item.</param>
        /// <returns>A cloned version of the toolstrip menu item</returns>
        public static ToolStripMenuItem Clone(
#if NET_3_5
            this 
#endif
            ToolStripMenuItem sourceToolStripMenuItem)
        {
            var menuItem = new ToolStripMenuItem();
#if NET_3_5
            var propInfoList = from p in typeof(ToolStripMenuItem).GetProperties()
                               let attributes = p.GetCustomAttributes(true)
                               let notBrowseable = (from a in attributes
                                                    where a.GetType() == typeof(BrowsableAttribute)
                                                    select !(a as BrowsableAttribute).Browsable).FirstOrDefault()
                               where !notBrowseable && p.CanRead && p.CanWrite && p.Name != "DropDown"
                               //orderby p.Name
                               select p;
            // Copy over using reflections
            foreach (var p in propInfoList)
            {
                object propertyInfoValue = p.GetValue(sourceToolStripMenuItem, null);
                p.SetValue(menuItem, propertyInfoValue, null);
            }
#else
            foreach (var p in typeof(ToolStripMenuItem).GetProperties())
            {
                bool Browsable = true;
                foreach (var a in p.GetCustomAttributes(true))
                    if (a.GetType() == typeof(BrowsableAttribute) && !(a as BrowsableAttribute).Browsable)
                    {
                        Browsable = false;
                        break;
                    }
                if (Browsable && p.CanRead && p.CanWrite && !p.Name.Equals("DropDown"))
                {
                    object propertyInfoValue = p.GetValue(sourceToolStripMenuItem, null);
                    p.SetValue(menuItem, propertyInfoValue, null);
                }
            }
#endif            
            // Create a new menu name
            menuItem.Name = String.Format("{0}-{1}", sourceToolStripMenuItem.Name, menuNameCounter++);

            // Process any other properties
            if (sourceToolStripMenuItem.ImageIndex != -1)
            {
                menuItem.ImageIndex = sourceToolStripMenuItem.ImageIndex;
            }

            if (!string.IsNullOrEmpty(sourceToolStripMenuItem.ImageKey))
            {
                menuItem.ImageKey = sourceToolStripMenuItem.ImageKey;
            }

            // We need to make this visible 
            menuItem.Visible = true;

            // Recursively clone the drop down list
            foreach (var item in sourceToolStripMenuItem.DropDownItems)
            {
                ToolStripItem newItem;
                if (item is ToolStripMenuItem)
                {
#if NET_3_5
                    newItem = ((ToolStripMenuItem)item).Clone();
#else
                    newItem = Clone((ToolStripMenuItem)item);
#endif
                }
                else if (item is ToolStripSeparator)
                {
                    newItem = new ToolStripSeparator();
                }
                else
                {
                    throw new NotImplementedException("Menu item is not a ToolStripMenuItem or a ToolStripSeparatorr");
                }

                menuItem.DropDownItems.Add(newItem);
            }

            // The handler list starts empty because we created its parent via a new
            // So this is equivalen to a copy.
#if NET_3_5
            menuItem.AddHandlers(sourceToolStripMenuItem);
#else
            AddHandlers(menuItem, sourceToolStripMenuItem);
#endif

            return menuItem;
        }

        /// <summary>
        /// Adds the handlers from the source component to the destination component
        /// </summary>
        /// <typeparam name="T">An IComponent type</typeparam>
        /// <param name="destinationComponent">The destination component.</param>
        /// <param name="sourceComponent">The source component.</param>
#if NET_3_5
        public static void AddHandlers<T>(this T destinationComponent, T sourceComponent) where T : IComponent
        {
            // If there are other handlers, they will not be erased
            var destEventHandlerList = destinationComponent.GetEventHandlerList();
            var sourceEventHandlerList = sourceComponent.GetEventHandlerList();

            destEventHandlerList.AddHandlers(sourceEventHandlerList);
        }
#else
        public static void AddHandlers<T>(T destinationComponent, T sourceComponent) where T : IComponent
        {
            // If there are other handlers, they will not be erased
            var destEventHandlerList = GetEventHandlerList(destinationComponent);
            var sourceEventHandlerList = GetEventHandlerList(sourceComponent);

            destEventHandlerList.AddHandlers(sourceEventHandlerList);
        }
#endif

        /// <summary>
        /// Gets the event handler list from a component
        /// </summary>
        /// <param name="component">The source component.</param>
        /// <returns>The EventHanderList or null if none</returns>
        public static EventHandlerList GetEventHandlerList(
#if NET_3_5
            this 
#endif
            IComponent component)
        {
            var eventsInfo = component.GetType().GetProperty("Events", BindingFlags.Instance | BindingFlags.NonPublic);
            return (EventHandlerList)eventsInfo.GetValue(component, null);
        }

        #endregion

        //////////////////////////////////////////////////
        // Private static methods
        //////////////////////////////////////////////////
    }
}
