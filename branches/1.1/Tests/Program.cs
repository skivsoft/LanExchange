using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{

    class Program
    {

        static void Main()
        {
            TabControllerTest TestTabs = new TabControllerTest();
            //TestTabs.CreateFromEmpyTabControl();
            {
                SettingsTest obj = new SettingsTest();
                obj.SetListValue();
            }

        }
    }


}