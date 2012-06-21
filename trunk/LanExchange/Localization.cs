using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace LanExchange
{
    public class Localization
    {
        private string Language = "";
        private static Localization LocalizationInstance = null;

        public Localization(string Lang)
        {
            Language = Lang;
        }
        

        public static Localization GetLocalization()
        {
            if (LocalizationInstance == null)
                LocalizationInstance = new Localization("RU");
            return LocalizationInstance;
        }

        public string this[string Name]
        {
            get
            {
                return Name;
            }
        }


        public void ApplyLanguage(Control Group)
        {
            if (String.IsNullOrEmpty(Group.Text))
                Group.Text = this[Group.Name];
            foreach (Control C in Group.Controls)
            {
                ApplyLanguage(C);
            }

        }

        public void ApplyLanguageToApplication()
        {
            foreach (var F in Application.OpenForms)
            {
                ApplyLanguage((Control)F);
            }
        }

    }
}
