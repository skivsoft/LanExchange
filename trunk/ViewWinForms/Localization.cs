using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using LanExchange.SDK;

namespace ViewWinForms.View
{
    public class Localization
    {
        private static void InternalProcessControl(Control control, string path)
        {
            // localize control's string properties
            Type type = control.GetType();
            foreach (PropertyInfo Info in type.GetProperties())
            {
                if (Info.PropertyType.Equals(typeof(String)) && Info.CanWrite)
                {
                    string S = Globals.Resources.GetText(String.Format("{0}.{1}", path, Info.Name));
                    if (!String.IsNullOrEmpty(S))
                        type.GetProperty(Info.Name).SetValue(control, S, null);
                }
            }
            // localize child controls
            foreach (Control C in control.Controls)
                InternalProcessControl(C, path+"."+C.Name);
        }

        public static void ProcessControl(Control control)
        {
            if (Globals.Resources == null)
                return;
            InternalProcessControl(control, control.Name);
        }
    }
}
